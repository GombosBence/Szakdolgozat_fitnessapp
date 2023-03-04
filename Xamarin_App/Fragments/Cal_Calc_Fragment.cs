using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.API;
using Szakdolgozat.Model;

namespace Szakdolgozat
{
    [Obsolete]
    public class Cal_Calc_Fragment : Android.Support.V4.App.Fragment
    {
        RadioButton maleRb;
        RadioButton femaleRb;
        EditText ageEt;
        EditText weightEt;
        EditText heightEt;
        Spinner activitySpinner;
        Spinner goalSpinner;
        Button saveBtn;
        Interface1 api;
        int userid;
        ConnectionString connectionString = new ConnectionString();

      [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.calorie_calculator_fragment, container, false);
            maleRb = view.FindViewById<RadioButton>(Resource.Id.maleRadio);
            femaleRb = view.FindViewById<RadioButton>(Resource.Id.femaleRadio);
            ageEt = view.FindViewById<EditText>(Resource.Id.ageEt);
            heightEt = view.FindViewById<EditText>(Resource.Id.heightEt);
            weightEt = view.FindViewById<EditText>(Resource.Id.weightEt);
            activitySpinner = view.FindViewById<Spinner>(Resource.Id.acitivtySpinner);
            goalSpinner = view.FindViewById<Spinner>(Resource.Id.goalSpinner);
            saveBtn = view.FindViewById<Button>(Resource.Id.saveBtn);
            // api = RestService.For<Interface1>("http://10.0.2.2:5016/");
            api = RestService.For<Interface1>(connectionString.getConnection());
            userid = Arguments.GetInt("uid");

            activitySpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_Itemselceted);
            var adapter = ArrayAdapter.CreateFromResource
                (Activity,Resource.Array.activityLevelRes, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            activitySpinner.Adapter = adapter;

            goalSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_Itemselceted);
            var adapter2 = ArrayAdapter.CreateFromResource
                (Activity, Resource.Array.goalArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            goalSpinner.Adapter = adapter2;


            maleRb.Click += MaleRb_Click;
            femaleRb.Click += FemaleRb_Click;
            saveBtn.Click += SaveBtn_Click;
            


            return view;
        }

       
        private async void SaveBtn_Click(object sender, EventArgs e)
        {

           
            
            int age = Int32.Parse(ageEt.Text);
            int height = Int32.Parse(heightEt.Text);
            int weight = Int32.Parse(weightEt.Text);
            string gender;
            if (maleRb.Checked)
            {
                 gender = "Male";
            }
            else {
                 gender = "Female";
            }
            string activityLevel = activitySpinner.SelectedItem.ToString();
            string goal = goalSpinner.SelectedItem.ToString();

            var result = await api.PutUserData(userid, age, weight, height, gender, goal, activityLevel);
            Toast.MakeText(Activity, result, ToastLength.Short).Show();

        }

        private void spinner_Itemselceted(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            
        }

        private void FemaleRb_Click(object sender, EventArgs e)
        {
            if (femaleRb.Checked)
            {
                maleRb.Checked = false;
                femaleRb.Checked = true;
            }
            else
            {
                maleRb.Checked = true;
                femaleRb.Checked = false;
            }
        }

        private void MaleRb_Click(object sender, EventArgs e)
        {
            if (maleRb.Checked)
            {
                maleRb.Checked = true;
                femaleRb.Checked = false;
            }
            else 
            {
                maleRb.Checked = false;
                femaleRb.Checked = true;
            }
        }
    }
}
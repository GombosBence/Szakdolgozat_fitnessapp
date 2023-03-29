using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.API;
using Szakdolgozat.Model;
using Szakdolgozat.Resources.adapter;

namespace Szakdolgozat.Fragments
{
    [Obsolete]
    public class mealListFragment : Android.Support.V4.App.Fragment, refreshInterface
    {

        TextView currentCalTv;
        TextView maximumCalTv;
        ProgressBar calProgressBar;
        TextView proValTv;
        TextView carboValTv;
        TextView fatsValTv;
        ImageView prevDayIv;
        ImageView nextDayIv;
        TextView dateTv;
        ListView myMealsList;
        int userid;
        Interface1 api;
        DateTime currentDate;
        myMealsListAdapter adapter;
        List<MyMealsModel> mealList;
        ConnectionString connectionString = new ConnectionString();


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment2, container, false);
            userid = Arguments.GetInt("uid");
            //api = RestService.For<Interface1>("http://10.0.2.2:5016/");
            api = RestService.For<Interface1>(connectionString.getConnection());
            currentCalTv = (TextView)view.FindViewById(Resource.Id.currentCalTv);
            maximumCalTv = (TextView)view.FindViewById(Resource.Id.maximumCalTv);
            calProgressBar = (ProgressBar)view.FindViewById(Resource.Id.calProgressBar);
            proValTv = (TextView)view.FindViewById(Resource.Id.proValTv);
            carboValTv = (TextView)view.FindViewById(Resource.Id.carboValTv);
            fatsValTv = (TextView)view.FindViewById(Resource.Id.fatsValTv);
            prevDayIv = (ImageView)view.FindViewById(Resource.Id.prevDayIv);
            nextDayIv = (ImageView)view.FindViewById(Resource.Id.nextDayIv);
            dateTv = (TextView)view.FindViewById(Resource.Id.dateTv);
            myMealsList = (ListView)view.FindViewById(Resource.Id.myMealsList);
            
            calculateMaxCalories();
            currentDate = DateTime.Now;
            dateTv.Text = DateTime.Now.ToString("yyyy - MM - dd");
            initMealsByDate(DateTime.Now);
            prevDayIv.Click += PrevDayIv_Click;
            nextDayIv.Click += NextDayIv_Click;
            
            

            return view;
        }


        private void NextDayIv_Click(object sender, EventArgs e)
        {
            if (currentDate == DateTime.Now)
            {
                return;
            }
           currentDate = currentDate.AddDays(1);
           initMealsByDate(currentDate);
           dateTv.Text = currentDate.ToString("yyyy - MM - dd");
        }

        private void PrevDayIv_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            initMealsByDate(currentDate);
            dateTv.Text = currentDate.ToString("yyyy - MM - dd");
        }

        public async void calculateMaxCalories()
        {
            var result = await api.calculateMaxCalories(userid);
            if (result.Equals("\"User is null\""))
            {
                return;
            }
            maximumCalTv.Text = result;
            calProgressBar.Max = Int32.Parse(result);
        }
        public async void initMealsByDate(DateTime date)
        {
            mealList = new List<MyMealsModel>();
            var result = await api.myMealsByDate(userid, date);
            mealList = JsonConvert.DeserializeObject<List<MyMealsModel>>(result);
            if (mealList == null)
            {
                return;
            }


            adapter = new myMealsListAdapter(Activity, mealList, userid, this);
            myMealsList.Adapter = adapter;
            myMealsList.Focusable = true;
            nutrientSummary();      
        }

        void nutrientSummary()
        {
            int cCalories = 0;
            int cProtein = 0;
            int cCarb = 0;
            int cFat = 0;
            mealList.ForEach(delegate (MyMealsModel meal)
            {
                cCalories += meal.calories;
                cProtein += meal.protein;
                cCarb += meal.carbohydrate;
                cFat += meal.fat;
            });
            currentCalTv.Text = cCalories.ToString();
            proValTv.Text = cProtein.ToString() + " g";
            carboValTv.Text = cCarb.ToString() + " g";
            fatsValTv.Text = cFat.ToString() + " g";
            calProgressBar.Progress = Int32.Parse(currentCalTv.Text);
            if ( Int32.Parse(currentCalTv.Text) > calProgressBar.Max)
            {
                calProgressBar.ProgressDrawable.SetColorFilter(Android.Graphics.Color.Red, Android.Graphics.PorterDuff.Mode.Multiply);
            }
            if (Int32.Parse(currentCalTv.Text) < calProgressBar.Max)
            {
                calProgressBar.ProgressDrawable.SetColorFilter(Android.Graphics.Color.Aquamarine, Android.Graphics.PorterDuff.Mode.Multiply);
            }

        }

        void refreshInterface.onDeleteClick(int id)
        {
            MyMealsModel meal = mealList.Find(x => x.id == id);
            mealList.Remove(meal);
            adapter.NotifyDataSetChanged();
            nutrientSummary();
        }
    }
}
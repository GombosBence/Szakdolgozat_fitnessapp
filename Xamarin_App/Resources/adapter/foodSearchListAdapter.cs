using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Telecom;
using Android.Views;
using Android.Widget;
using DotLiquid.Util;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.API;
using Szakdolgozat.Model;

namespace Szakdolgozat.Resources.adapter
{

    internal class foodSearchListAdapter : RecyclerView.Adapter
    {


        private Activity activity;
        private List<Hint> hints;
        private int loggedInUserId;
        ConnectionString connectionString = new ConnectionString();


        public foodSearchListAdapter(Activity activity, List<Hint> hints, int loggedInUserId)
        {
            this.loggedInUserId = loggedInUserId;
            this.activity = activity;
            this.hints = hints;
        }

        public class MyView : RecyclerView.ViewHolder
        {

            public View mMainView { get; set; }
            public TextView mfoodnName { get; set; }
            public TextView mcaloriesValue { get; set; }
            public TextView mproteinValueTv { get; set; }
            public TextView mcarbValueTv { get; set; }
            public TextView mfatValueTv { get; set; }
            public EditText mquantityEt { get; set; }
            public Button maddBtn { get; set; }

            public MyView(View mMainView) : base(mMainView)
            {
                this.mMainView = mMainView;
            }
        }

        public override int ItemCount
        {
            get { return hints.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            //Interface1 api = RestService.For<Interface1>("http://10.0.2.2:5016/");
            Interface1 api = RestService.For<Interface1>(connectionString.getConnection());
            float calories = (float)hints[position].food.nutrients.ENERC_KCAL;
            float protein = (float)hints[position].food.nutrients.PROCNT;
            float carb = (float)hints[position].food.nutrients.CHOCDF;
            float fat = (float)hints[position].food.nutrients.FAT;
            MyView myHolder = holder as MyView;

            float quantity = float.Parse(myHolder.mquantityEt.Text);

            myHolder.mfoodnName.Text = hints[position].food.label;
            myHolder.mcaloriesValue.Text = ((int)(calories / 100 * quantity)).ToString();
            myHolder.mproteinValueTv.Text = ((int)(protein / 100 * quantity)).ToString();
            myHolder.mcarbValueTv.Text = ((int)(carb / 100 * quantity)).ToString();
            myHolder.mfatValueTv.Text = ((int)(fat / 100 * quantity)).ToString();


            if (!myHolder.maddBtn.HasOnClickListeners)
            {
                myHolder.maddBtn.Click += async (sender, args) =>
                {

                    string _foodName = myHolder.mfoodnName.Text;
                    int _calories = Int32.Parse(myHolder.mcaloriesValue.Text);
                    int _protein = Int32.Parse(myHolder.mproteinValueTv.Text);
                    int _carbohydrate = Int32.Parse(myHolder.mcarbValueTv.Text);
                    int _fat = Int32.Parse(myHolder.mfatValueTv.Text);
                    double _quantityInGrams = double.Parse(myHolder.mquantityEt.Text);


                    var result = await api.AddMeal(loggedInUserId, _foodName, _calories, _protein, _carbohydrate, _fat, _quantityInGrams);
                    Toast.MakeText(activity, result, ToastLength.Short).Show();
                    myHolder.maddBtn.Enabled = false;
                    myHolder.mquantityEt.Text = 100.ToString();
                    await Task.Delay(2000);
                    myHolder.maddBtn.Enabled = true;


                };
            }

            myHolder.mquantityEt.Click += (sender, args) =>
            {
                myHolder.mquantityEt.RequestFocus();
            };

            myHolder.mquantityEt.TextChanged += (sender, args) =>
            {
                if (myHolder.mquantityEt.Text == "")
                {
                    myHolder.mquantityEt.Text = "1";
                }
                float quantity = float.Parse(myHolder.mquantityEt.Text);

                myHolder.mcaloriesValue.Text = ((int)(calories / 100 * quantity)).ToString();
                myHolder.mproteinValueTv.Text = ((int)(protein / 100 * quantity)).ToString();
                myHolder.mcarbValueTv.Text = ((int)(carb / 100 * quantity)).ToString();
                myHolder.mfatValueTv.Text = ((int)(fat / 100 * quantity)).ToString();
            };

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.searchResultListTemplate, parent, false);
            TextView foodnName = (TextView)row.FindViewById(Resource.Id.foodNameTv);
            TextView caloriesValue = (TextView)row.FindViewById(Resource.Id.calorieValueTv);
            TextView proteinValueTv = (TextView)row.FindViewById(Resource.Id.proteinValueTv);
            TextView carbValueTv = (TextView)row.FindViewById(Resource.Id.carbValueTv);
            TextView fatValueTv = (TextView)row.FindViewById(Resource.Id.fatValueTv);
            EditText quantityEt = (EditText)row.FindViewById(Resource.Id.quantityEt);
            Button addBtn = (Button)row.FindViewById(Resource.Id.addBtn);

            MyView view = new MyView(row)
            {
                mfoodnName = foodnName,
                mcaloriesValue = caloriesValue,
                mproteinValueTv = proteinValueTv,
                mcarbValueTv = carbValueTv,
                mfatValueTv = fatValueTv,
                mquantityEt = quantityEt,
                maddBtn = addBtn
            };
            return view;
        }


    }
}
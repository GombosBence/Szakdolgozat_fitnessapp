using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.API;
using Szakdolgozat.Model;

namespace Szakdolgozat.Resources.adapter
{
    internal class myMealsListAdapter : BaseAdapter<MyMealsModel>
    {

        private Activity activity;
        private List<MyMealsModel> list;
        private int loggedInUser;
        refreshInterface refreshI;
        ConnectionString connectionString = new ConnectionString();
        public myMealsListAdapter(Activity activity, List<MyMealsModel> list, int loggedInUser, refreshInterface refreshI)
        {
            this.activity = activity;
            this.list = list;
            this.loggedInUser = loggedInUser;
            this.refreshI = refreshI;
        }

        public override MyMealsModel this[int position]
        {
            get { return list[position]; }
        }

        public override int Count
        {
            get
            {
                return list.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public class ViewHolder : Java.Lang.Object
        {
            public TextView foodName { get; set; }
            public TextView caloriesValue { get; set; }
            public TextView proteinValueTv { get; set; }
            public TextView carbValueTv { get; set; }
            public TextView fatValueTv { get; set; }
            public TextView quantityEt { get; set; }
            public Button deleteBtn { get; set; }
        }

            public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //Interface1 api = RestService.For<Interface1>("http://10.0.2.2:5016/");
            Interface1 api = RestService.For<Interface1>(connectionString.getConnection());
            ViewHolder holder = null;
            if (holder == null)
            {
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.myMealsListTemplate, parent, false);
                holder = new ViewHolder
                {
                    foodName = (TextView)convertView.FindViewById(Resource.Id.foodNameTv),
                    caloriesValue = (TextView)convertView.FindViewById(Resource.Id.calorieValueTv),
                    proteinValueTv = (TextView)convertView.FindViewById(Resource.Id.proteinValueTv),
                    carbValueTv = (TextView)convertView.FindViewById(Resource.Id.carbValueTv),
                    fatValueTv = (TextView)convertView.FindViewById(Resource.Id.fatValueTv),
                    quantityEt = (TextView)convertView.FindViewById(Resource.Id.quantityEt),
                    deleteBtn = (Button)convertView.FindViewById(Resource.Id.deleteBtn)
                };

                holder.foodName.Text = list[position].foodName;
                holder.caloriesValue.Text = list[position].calories.ToString();
                holder.proteinValueTv.Text = list[position].protein.ToString();
                holder.carbValueTv.Text = list[position].carbohydrate.ToString();
                holder.fatValueTv.Text = list[position].fat.ToString();
                holder.quantityEt.Text = list[position].quantity.ToString();

                holder.deleteBtn.Click += async (sender, args) =>
                {
                    var result = await api.DeleteMeal(loggedInUser, list[position].id);
                    Toast.MakeText(activity, result, ToastLength.Short).Show();
                    refreshI.onDeleteClick(list[position].id);
                };
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

                return convertView;

            }
        }
 }

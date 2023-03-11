using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DotLiquid.Util;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.API;
using Szakdolgozat.Model;

namespace Szakdolgozat.Resources.adapter
{



    internal class userMilestoneAdapter : RecyclerView.Adapter
    {


        private Activity activity;
        private List<Milestone> milestones;
        private int loggedInUserId;
        ConnectionString connectionString = new ConnectionString();

        public userMilestoneAdapter(Activity activity, List<Milestone> milestones, int loggedInUserId)
        {
            this.activity = activity;
            this.milestones = milestones;
            this.loggedInUserId = loggedInUserId;
        }

        public class MyView : RecyclerView.ViewHolder
        {
            public View mMainView { get; set; }
            
            public TextView mileStoneName { get; set; }

            public TextView milestoneScore { get; set; }


            public MyView(View mMainView) : base(mMainView)
            {
                this.mMainView = mMainView;
            }
            
        }

        public override int ItemCount
        {
            get { return milestones.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            //Interface1 api = RestService.For<Interface1>(connectionString.getConnection());
            MyView myHolder = holder as MyView;

            myHolder.mileStoneName.Text = milestones[position].MilestoneName;
            myHolder.milestoneScore.Text = (milestones[position].Score).ToString();

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.milestoneListTemplate, parent, false);
            TextView mName = (TextView)row.FindViewById(Resource.Id.milestoneNameTv);
            TextView mScore = (TextView)row.FindViewById(Resource.Id.scoreTv);

            MyView view = new MyView(row)
            {
                mileStoneName = mName,
                milestoneScore = mScore


            };

            return view;
        }
    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DotLiquid.Util;
using Java.Lang;
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

            public TextView currentProgress { get; set; }

            public TextView goalProgress { get; set; }

            public ProgressBar ProgressBar { get; set; }


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
            myHolder.currentProgress.Text = (milestones[position].progress).ToString();
            myHolder.goalProgress.Text = (milestones[position].Goal).ToString();
            myHolder.ProgressBar.Max = milestones[position].progress;
            myHolder.ProgressBar.Progress = milestones[position].progress;

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.milestoneListTemplate, parent, false);
            TextView mName = (TextView)row.FindViewById(Resource.Id.milestoneNameTv);
            TextView mScore = (TextView)row.FindViewById(Resource.Id.scoreTv);
            TextView goalProg = (TextView)row.FindViewById(Resource.Id.goalProgressTv);
            TextView currentProg = (TextView)row.FindViewById(Resource.Id.currentProgressTv);
            ProgressBar progBar = (ProgressBar)row.FindViewById(Resource.Id.milestoneProgressBar);


            MyView view = new MyView(row)
            {
                mileStoneName = mName,
                milestoneScore = mScore,
                goalProgress = goalProg,
                currentProgress = currentProg,
                ProgressBar = progBar


            };

            return view;
        }
    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.API;
using Szakdolgozat.Model;
using Szakdolgozat.Resources.adapter;

namespace Szakdolgozat.Fragments
{
    public class milestoneFragment : Android.Support.V4.App.Fragment
    {

        Button myMilestones;
        Button inProgress;
        Interface1 api;
        TextView scoreTv;
        int userid;
        RecyclerView achivementList;
        userMilestoneAdapter adapter;
        ConnectionString connectionString = new ConnectionString();
        List<Milestone> userMilestoneList = new List<Milestone>();
        List<Milestone> inProgressList = new List<Milestone>();
        RecyclerView.LayoutManager layoutManager;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.milestones, container, false);
            api = RestService.For<Interface1>(connectionString.getConnection());
            userid = Arguments.GetInt("uid");
            myMilestones = (Button)view.FindViewById(Resource.Id.myMilestoneBtn);
            inProgress = (Button)view.FindViewById(Resource.Id.inProgressBtn);
            achivementList = (RecyclerView)view.FindViewById(Resource.Id.achievement_list);
            scoreTv = (TextView)view.FindViewById(Resource.Id.scoreTv);

            layoutManager = new LinearLayoutManager(Activity);
            achivementList.SetLayoutManager(layoutManager);
            getScore();
            myMilestones.Click += MyMilestones_Click;
            inProgress.Click += InProgress_ClickAsync;




            return view;
        }


        public async void getScore()
        {
            var result = await api.getUserMilestoneScore(userid);
            scoreTv.Text = JsonConvert.DeserializeObject<string>(result);

        }

        private async void InProgress_ClickAsync(object sender, EventArgs e)
        {
            var result = await api.getUserMilestones(userid, 1);
            userMilestoneList = JsonConvert.DeserializeObject<List<Milestone>>(result);



            achivementList.SetAdapter(new userMilestoneAdapter(Activity, userMilestoneList, userid));
        }

        private async void MyMilestones_Click(object sender, EventArgs e)
        {
            var result = await api.getUserMilestones(userid, 0);
            userMilestoneList = JsonConvert.DeserializeObject<List<Milestone>>(result);
                
            
            
                achivementList.SetAdapter(new userMilestoneAdapter(Activity, userMilestoneList, userid));
            
        }
    }
}
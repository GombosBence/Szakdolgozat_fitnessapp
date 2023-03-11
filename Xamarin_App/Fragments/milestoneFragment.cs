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
            achivementList = (RecyclerView)view.FindViewById(Resource.Id.achievement_list);

            layoutManager = new LinearLayoutManager(Activity);
            achivementList.SetLayoutManager(layoutManager);
            myMilestones.Click += MyMilestones_Click;




            return view;
        }

        private async void MyMilestones_Click(object sender, EventArgs e)
        {
            var result = await api.getUserMilestones(userid);
            userMilestoneList = JsonConvert.DeserializeObject<List<Milestone>>(result);
            
                Toast.MakeText(Activity, userMilestoneList[0].MilestoneName, ToastLength.Short).Show();
            
           
                
            
            
                achivementList.SetAdapter(new userMilestoneAdapter(Activity, userMilestoneList, userid));
            
        }
    }
}
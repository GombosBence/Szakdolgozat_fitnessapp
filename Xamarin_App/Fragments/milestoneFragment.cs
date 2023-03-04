using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szakdolgozat.Fragments
{
    public class milestoneFragment : Android.Support.V4.App.Fragment
    {

        Button myMilestones;
        Button inProgress;
        RecyclerView achivementList;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.milestones, container, false);




            return view;
        }
    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DocumentFormat.OpenXml.Drawing;
using Java.Nio;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.API;
using Szakdolgozat.Fragments;
using Szakdolgozat.Model;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using SupportToolBar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Java.Lang;

namespace Szakdolgozat.Fragments
{
    [Obsolete]
    public class MainMealFragment : Android.Support.V4.App.Fragment, ViewPager.IOnPageChangeListener
    {
        mealTrackerFragment fragment1;
        Fragment2 fragment2;
        Cal_Calc_Fragment calFragment;
        int loggedInUserId;
        Interface1 api;
        DrawerLayout mDrawerLayout;
        ViewPager viewPager;
        TabAdapter adapter;
        ConnectionString connectionString = new ConnectionString();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.designlayout, container, false);
            //api = RestService.For<Interface1>("http://10.0.2.2:5016/");
            api = RestService.For<Interface1>(connectionString.getConnection());
            loggedInUserId = Arguments.GetInt("uid");

            Bundle bundle = new Bundle();
            bundle.PutInt("uid", loggedInUserId);
            fragment1 = new mealTrackerFragment();
            fragment1.Arguments = bundle;
            fragment2 = new Fragment2();
            fragment2.Arguments = bundle;
            calFragment = new Cal_Calc_Fragment();
            calFragment.Arguments = bundle;


            mDrawerLayout = mDrawerLayout = view.FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            NavigationView navigagtonView = view.FindViewById<NavigationView>(Resource.Id.nav_view);
            if (navigagtonView != null)
            {
                SetUpDrawerContent(navigagtonView);
            }
            TabLayout tabs = view.FindViewById<TabLayout>(Resource.Id.tabs);
            viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            SetUpViewPage(viewPager);
            tabs.SetupWithViewPager(viewPager);
            viewPager.AddOnPageChangeListener(this);





            return view;
        }

        private void SetUpViewPage(ViewPager viewPager)
        {

            adapter = new TabAdapter(ChildFragmentManager);
            adapter.AddFragment(fragment2, "My Meals");
            adapter.AddFragment(fragment1, "Meal Tracker");
            adapter.AddFragment(calFragment, "Calorie Calculator");

            viewPager.Adapter = adapter;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }

        }

        private void SetUpDrawerContent(NavigationView navigagtonView)
        {
            navigagtonView.NavigationItemSelected += (object sneder, NavigationView.NavigationItemSelectedEventArgs e) =>
            {
                e.MenuItem.SetChecked(true);
                mDrawerLayout.CloseDrawers();
            };
        }

        public void OnPageScrollStateChanged(int state)
        {
            return;
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            return;
        }

        public void OnPageSelected(int position)
        {

            if (position == 0)
                viewPager.Adapter.NotifyDataSetChanged();
            else
                return;
        }

        public class TabAdapter : FragmentStatePagerAdapter
        {
            public List<SupportFragment> Fragments { get; set; }
            public List<string> FragmentNames { get; set; }

            public TabAdapter(SupportFragmentManager sfm) : base(sfm)
            {
                Fragments = new List<SupportFragment>();
                FragmentNames = new List<string>();
            }

            public override int GetItemPosition(Java.Lang.Object @object)
            {
                return PositionNone;
            }

            public void AddFragment(SupportFragment fragment, string name)
            {
                Fragments.Add(fragment);
                FragmentNames.Add(name);
            }
            public override int Count
            {
                get { return Fragments.Count; }
            }


            public override SupportFragment GetItem(int position)
            {

                return Fragments[position];
            }
            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(FragmentNames[position]);
            }
        }
    }
}
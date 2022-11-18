using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using Szakdolgozat.Fragments;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using SupportToolBar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Support.Design.Widget;
using static Szakdolgozat.Fragments.MainMealFragment;
using Xamarin.Forms;
using Szakdolgozat.Model;
using Szakdolgozat.API;
using Android.Content.PM;
using Android.Runtime;
using Android.Support.V4.Content;
using Android;
using Xamarin.Essentials;

namespace Szakdolgozat
{
    [Obsolete]
    [Activity(Label = "main_page", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Main_Page : AppCompatActivity// , Android.Support.V4.View.ViewPager.IOnPageChangeListener
    {
       
        public int loggedInUserId;
        DrawerLayout mDrawerLayout;
        FrameLayout fragmentContainer;
        NavigationView navigagtonView;
        MainMealFragment _mainMealFragment;
        StepCounterFragment _stepCounterFragment;
        SupportFragment mCurrentFragment;
        Stack<SupportFragment> mStackFragment;
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_page);
            loggedInUserId = Intent.GetIntExtra("userID", 0);
            Forms.Init(this, savedInstanceState);
            //DependencyService.Resolve<IStepCounter>().InitSensorService();

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ActivityRecognition) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.ActivityRecognition }, 0);

            }
            else
            {
                DependencyService.Resolve<IStepCounter>().InitSensorService();
            }

            fragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);
            mStackFragment = new Stack<SupportFragment>();
            Bundle bundle = new Bundle();
            bundle.PutInt("uid", loggedInUserId);
            _mainMealFragment = new MainMealFragment();
            _stepCounterFragment = new StepCounterFragment();
            _mainMealFragment.Arguments = bundle;
            _stepCounterFragment.Arguments = bundle;

            mCurrentFragment = _mainMealFragment;
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(fragmentContainer.Id, _stepCounterFragment, "Stepcounter");
            trans.Hide(_stepCounterFragment);
            trans.Add(fragmentContainer.Id, _mainMealFragment, "MainMealFragment");
            trans.Commit();


            SupportToolBar toolBar = FindViewById<SupportToolBar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Drawable.abc_ic_menu_copy_mtrl_am_alpha);
            ab.SetDisplayHomeAsUpEnabled(true);

           mDrawerLayout = mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
           navigagtonView = FindViewById<NavigationView>(Resource.Id.nav_view);
            if (navigagtonView != null)
            {
                SetUpDrawerContent(navigagtonView);
            }
            
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (grantResults.Length != 1)
            {
                return;
            }
            var ispermissionGranted = grantResults[0] == Permission.Granted;
            if (ispermissionGranted)
            {
                DependencyService.Resolve<IStepCounter>().InitSensorService();
            }
        }

        public void ShowFragment(SupportFragment supportFragment)
        {
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Hide(mCurrentFragment);
            trans.Show(supportFragment);
            trans.AddToBackStack(null);
            trans.Commit();
            mStackFragment.Push(mCurrentFragment);
            mCurrentFragment = supportFragment;

        }

        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                //SupportFragmentManager.PopBackStack();
                //mCurrentFragment = mStackFragment.Pop();
                return;
            }
            else
            {
                base.OnBackPressed();
            }
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

        public void LogOut()
        {
            Intent i = new Intent(this, typeof(Login_Page));
            StartActivity(i);
            Finish();

        }
        
        private void SetUpDrawerContent(NavigationView navigagtonView)
        {
            navigagtonView.NavigationItemSelected += (object sneder, NavigationView.NavigationItemSelectedEventArgs e) =>
            {
                e.MenuItem.SetChecked(true);
                switch (e.MenuItem.ToString())
                {
                    case "Food":
                        ShowFragment(_mainMealFragment);
                        break;
                    case "Dashboard":
                        break;
                    case "Step Counter":
                        ShowFragment(_stepCounterFragment);
                        break;
                    case "Milestones":
                        break;
                    case "Profile settings":
                        break;
                    case "Log Out":
                        DependencyService.Resolve<IStepCounter>().StopSensorService();
                        Preferences.Clear();
                        LogOut();
                        break;
                }
                mDrawerLayout.CloseDrawers();
            };
        }

        
        
    }
}
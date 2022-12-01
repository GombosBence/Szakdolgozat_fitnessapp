using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Szakdolgozat.API;
using Refit;
using Szakdolgozat.Model;
using Newtonsoft.Json;
using System;
using Android.Support.Design.Widget;
using System.Net;
using Java.Util.Prefs;
using Xamarin.Essentials;
using Preferences = Xamarin.Essentials.Preferences;

namespace Szakdolgozat
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Login_Page : AppCompatActivity
    {
        Button _registrationBtn;
        Button forgotPwBtn;
        Button loginBtn;
        EditText usernameEt;
        EditText passwordEt;
        Interface1 api;
        ConnectionString connectionString = new ConnectionString();

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnCreate(Bundle savedInstanceState)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login_page);



            _registrationBtn = FindViewById<Button>(Resource.Id.registerBtn);
            usernameEt = FindViewById<EditText>(Resource.Id.usernameEt);
            passwordEt = FindViewById<EditText>(Resource.Id.passwordEt);
            forgotPwBtn = FindViewById<Button>(Resource.Id.forgotPwBtn);
            loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
            //api = RestService.For<Interface1>("http://10.0.2.2:5016/");
            string ip = "http://" + getLocalIp() + ":45455/";
            api = RestService.For<Interface1>(connectionString.getConnection());

            
            if (Preferences.Get("LoggedIn", false))
            {
                usernameEt.Text = Preferences.Get("UserName", "");
                passwordEt.Text = Preferences.Get("Password", "");

                Intent i = new Intent(this, typeof(Main_Page));
                i.PutExtra("userID", Preferences.Get("LoggedInId", -1));
                StartActivity(i);
                Finish();


            }


            if (_registrationBtn != null)
                _registrationBtn.Click += (sender, e) =>
                {
                    Intent i = new Intent(this, typeof(Reg_Page));
                    StartActivity(i);
                };

            loginBtn.Click += LoginBtn_Click; 
            forgotPwBtn.Click += ForgotPwBtn_Click;
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            UserInformation userinfo = new UserInformation
            {
                Username = usernameEt.Text,
                Password = passwordEt.Text,
                Email = " "
            };
                
            
            string badPw = JsonConvert.SerializeObject("Incorrect password");
            string badUs = JsonConvert.SerializeObject("User doesn't exists");
            var result = await api.LoginUser(userinfo);
            if (result.Equals(badPw) || result.Equals(badUs))
            {
                Toast.MakeText(this, result, ToastLength.Short).Show();
            }
            else
            {
                int loggedInUserID = Int32.Parse(result);
                Preferences.Set("LoggedIn", true);
                Preferences.Set("LoggedInId", loggedInUserID);
                Preferences.Set("UserName", userinfo.Username);
                Preferences.Set("Password", userinfo.Password);

                Intent i = new Intent(this, typeof(Main_Page));
                i.PutExtra("userID", loggedInUserID);
                StartActivity(i);
                Finish();
            }
        }

        public string getLocalIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                return ip.ToString();
            }
            throw new Exception("Not network adapters with Ipv4");
        }

        public override void OnBackPressed()
        {
            Finish();
        }


        [Obsolete]
        private void ForgotPwBtn_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(Main_Page));
            i.PutExtra("userID", 6);
            StartActivity(i);
            Finish();
        }
    }
}
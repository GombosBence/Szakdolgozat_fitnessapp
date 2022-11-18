using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Szakdolgozat.API;
using Refit;
using System.Runtime.Remoting.Messaging;
using Szakdolgozat.Model;
using Android.Content;
using Newtonsoft.Json;

namespace Szakdolgozat
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Reg_Page : AppCompatActivity
    {
        EditText usernameEt;
        EditText passwordEt;
        EditText passwordAgainEt;
        EditText emailEt;
        Button nextBtn;
        Button backBtn;
        Interface1 api;
        ConnectionString connectionString = new ConnectionString();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.reg_page);

           // api = RestService.For<Interface1>("http://10.0.2.2:5016/");
            api = RestService.For<Interface1>(connectionString.getConnection());
            usernameEt = FindViewById<EditText>(Resource.Id.nameEt);
            passwordEt = FindViewById<EditText>(Resource.Id.passwordEt);
            passwordAgainEt = FindViewById<EditText>(Resource.Id.passwordAgainEt);
            emailEt = FindViewById<EditText>(Resource.Id.emailEt);
            nextBtn = FindViewById<Button>(Resource.Id.nextBtn);
            backBtn = FindViewById<Button>(Resource.Id.backBtn);

            nextBtn.Click += async delegate
            {
                if (!passwordEt.Text.Equals(passwordAgainEt.Text))
                {
                    Toast.MakeText(this, "A két jelszó nem egyezik meg", ToastLength.Short).Show();
                    return;
                }

                UserInformation userinfo = new UserInformation
                {
                    Username = usernameEt.Text.ToString(),
                    Password = passwordEt.Text.ToString(),
                    Email = emailEt.Text.ToString()
                };
                

                var result = await api.RegisterUser(userinfo);
                Toast.MakeText(this, result, ToastLength.Short).Show();
            };

            backBtn.Click += BackBtn_Click;        
        }

        private void BackBtn_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(Login_Page));
            Finish();
            StartActivity(i);
        }
    }
}
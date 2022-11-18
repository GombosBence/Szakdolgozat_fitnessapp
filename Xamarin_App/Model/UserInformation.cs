using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szakdolgozat.Model
{
    public class UserInformation
    {

        public UserInformation()
        {
            MealsHistories = new HashSet<MealsHistory>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Salt { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Gender { get; set; }
        public int Goal { get; set; }
        public int ActivityLevel { get; set; }

        public virtual ICollection<MealsHistory> MealsHistories { get; set; }
    }
            
}
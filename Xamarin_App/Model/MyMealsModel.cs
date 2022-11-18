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
    public class MyMealsModel
    {

        
        public int id { get; set; }
        public string foodName { get; set; }
        public int calories { get; set; }
        public int protein { get; set; }
        public int fat { get; set; }
        public int carbohydrate { get; set; }

        public double quantity { get; set; }

        public DateTime mealDate { get; set; }


    }
}
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
    public class MealsHistory
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string FoodName { get; set; } = null!;
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbohydrate { get; set; }
        public int Fat { get; set; }
        public DateTime Date { get; set; }
        public double QuantityInGrams { get; set; }

        public virtual UserInformation User { get; set; } = null!;

    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model;

namespace Szakdolgozat.API
{
    internal interface foodInterface
    {
        
        [Get("/api/food-database/v2/parser")]
        Task<string> FoodSearch
            ([Query]string app_id, string app_key, string ingr);

    }
}
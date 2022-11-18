using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.API;
using Szakdolgozat.Model;
using Szakdolgozat.Resources.adapter;

namespace Szakdolgozat.Fragments
{
    [Obsolete]
    public class mealTrackerFragment : Android.Support.V4.App.Fragment
    {
        int userid;
        foodInterface api;
        EditText searchBarEt;
        RecyclerView recyclerSearchResultList;
        RecyclerView.LayoutManager layoutManager;
        Button searchBtn;
        string appId = "4e02afaf";
        string appKey = "2a84c303e28d514143a725b86dc087a5";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.mealsFragment, container, false);
            api = RestService.For<foodInterface>("https://api.edamam.com/");
            searchBarEt = (EditText)view.FindViewById<EditText>(Resource.Id.searchBarEt);
            searchBtn = (Button)view.FindViewById<Button>(Resource.Id.searchBtn);
            recyclerSearchResultList = view.FindViewById<RecyclerView>(Resource.Id.recyclerResultList);
            userid = Arguments.GetInt("uid");


            layoutManager = new LinearLayoutManager(Activity);
            recyclerSearchResultList.SetLayoutManager(layoutManager);




            searchBtn.Click += SearchBtn_Click;
            searchBarEt.FocusChange += SearchBarEt_FocusChange;
            
            return view;
        }

        private void SearchBarEt_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (searchBarEt.HasFocus)
            {
                recyclerSearchResultList.Enabled = false;   
            }
        }

        private async void SearchBtn_Click(object sender, EventArgs e)
        {
            recyclerSearchResultList.Enabled = true;
            searchBarEt.ClearFocus();
            string query = searchBarEt.Text;
            var result = await api.FoodSearch(appId, appKey, query);
            Root searchResult = JsonConvert.DeserializeObject<Root>(result);

            recyclerSearchResultList.SetAdapter(new foodSearchListAdapter(Activity, searchResult.hints, userid));
           
        }

    }
}
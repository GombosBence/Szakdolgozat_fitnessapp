﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Javax.Security.Auth.Login;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model;
using System.Net.Http;

namespace Szakdolgozat.API
{
    internal interface Interface1
    {


        [Put("/api/UserInformation")]
        Task<string> PutUserData([Body] int userId, int age, int weight, int height, string gender, string goal, string actvity);

        [Post("/api/UserInformation")]
        Task<string> GetUsername([Body] int userId);

        [Post("/api/CalorieCalculator")]
        Task<string> calculateMaxCalories([Body] int userId);

        [Post("/api/Register")]
        Task<string> RegisterUser([Body] UserInformation userinfo);

        [Post("/api/Login")]
        Task<string> LoginUser([Body] UserInformation userinfo);

        [Post("/api/MealHistory")]
        Task<string> myMealsByDate([Body] int userId, DateTime date);

        [Delete("/api/Meals")]
        Task<string> DeleteMeal([Body] int userId, int mealId);

        [Post("/api/Meals")]
        Task<string> AddMeal([Body] int userId, string foodName, int calories, int protein, int carb, int fat, double quantity);

        [Post("/api/Steps")]
        Task<int> saveSteps(int userId, int steps, DateTime date);

        [Post("/api/SetGoal")]
        Task<string> setGoal(int userId, int goal, DateTime date);

        [Post("/api/GetSteps")]
        Task<int[]> getLastWeekSteps(int userid, List<DateTime> dates);

        [Post("/api/GetStepGoal")]
        Task<int> getStepGoal(int userid);

        [Post("/api/UserMilestone")]
        Task<string> getUserMilestones([Body] int userId, int qVersion);

        [Post("/api/MilestoneScore")]
        Task<string> getUserMilestoneScore([Body] int userId);

        
    }
}
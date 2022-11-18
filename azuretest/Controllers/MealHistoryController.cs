using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealHistoryController : ControllerBase
    {

        AspWebApiDbContext dbContext = new AspWebApiDbContext();



        [HttpPost]
        public string myMealsByDate([FromBody] int postedUserId, DateTime date)
        {
            List<MealsHistory> mealList = dbContext.MealsHistories.ToList();
            
            List<MealsHistory> reducedList = mealList.FindAll(x => x.UserId == postedUserId && x.Date.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd"));

            List<MyMealsModel> myMealsList = new List<MyMealsModel>();
            reducedList.ForEach(x => myMealsList.Add(new MyMealsModel(x.Id , x.FoodName, x.Calories, x.Protein, x.Carbohydrate,  x.Fat, x.QuantityInGrams, x.Date)));
            string result = JsonConvert.SerializeObject(myMealsList);
            return JsonConvert.SerializeObject(myMealsList);

        }
    }
}

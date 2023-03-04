using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        AspWebApiDbContext dbContext = new AspWebApiDbContext();


        [HttpDelete]
        public string DeleteMeal([FromBody] int userId, int mealId)
        {
            List<MealsHistory> meals = dbContext.MealsHistories.ToList();
            MealsHistory? delMeal = meals.Find(x => x.UserId == userId && x.Id == mealId);
            if (delMeal != null)
            {
                dbContext.Remove(delMeal);
                dbContext.SaveChanges();
                return JsonConvert.SerializeObject("Deleted");
            }
            return JsonConvert.SerializeObject("Not Found");
           
        }

        [HttpGet]
        public int Getnumber()
        {
            List<MealsHistory> dataList = dbContext.MealsHistories.ToList();
            return dataList.Count;
        }

        [HttpPost]
        public string Post([FromBody] int userId, string foodName, int calories, int protein, int carb, int fat, double quantity)
        {

            UserInformation user = new UserInformation();
            List<UserInformation> userList = dbContext.UserInformations.ToList();
            List<UserMilestone> userMilestones = dbContext.UserMilestones.ToList();
            user = userList.Find(x => x.UserId == userId);
            if (user == null)
            {
                return JsonConvert.SerializeObject("Can't find user");
            }
            MealsHistory value = new MealsHistory();
            Random rnd = new Random();
            int num = rnd.Next();
            value.Id = num;
            value.UserId = userId;
            value.FoodName = foodName;
            value.Calories = calories;
            value.Protein = protein;
            value.Carbohydrate = carb;
            value.Fat = fat;
            var date = DateTime.Now;
            value.Date = date;
            value.QuantityInGrams = quantity;
            value.User = user;

            try
            {

                if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 8) == null)
                {
                    UserMilestone newMilestone = new UserMilestone();
                    newMilestone.UserId = user.UserId;
                    newMilestone.MilestoneId = 8;
                    dbContext.UserMilestones.Add(newMilestone);
                    dbContext.SaveChanges();
                }

                dbContext.MealsHistories.Add(value);
                dbContext.SaveChanges();
                return JsonConvert.SerializeObject("Successfully added to your meals");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }

        }


    }
}

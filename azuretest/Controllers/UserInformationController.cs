using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationController : ControllerBase
    {

        AspWebApiDbContext dbContext = new AspWebApiDbContext(); 

        [HttpPost]
        public string UsernameFromId([FromBody] int postedUserId)
        {
            List<UserInformation> datalist = dbContext.UserInformations.ToList();
            UserInformation? user = datalist.Find(x => x.UserId == postedUserId);

            if (user != null)
                return user.Username;
            else
                return JsonConvert.SerializeObject("Not found");
        }

        [HttpPut]
        public string PutUserData([FromBody] int userId, int age, int weight, int height,string gender, string goal, string actvity)
        {
            List<UserInformation> datalist = dbContext.UserInformations.ToList();
            UserInformation? user = datalist.Find(x => x.UserId == userId);
            if (user != null)
            {
                user.Age = age;
                user.Weight = weight;
                user.Height = height;
                user.Gender = gender;

                switch (goal)
                {
                    case "Lose fat":
                        user.Goal = 1;
                        break;
                    case "Maintain weight":
                        user.Goal = 2;
                        break;
                    case "Gain muscle":
                        user.Goal = 3;
                        break;
                }
                switch (actvity)
                {
                    case "Not active (desk job, no exercise)":
                        user.ActivityLevel = 1;
                        break;
                    case "Lightly active (1-2 days of exercise a week)":
                        user.ActivityLevel = 2;
                        break;
                    case "Active (3-5 days of exercise a week)":
                        user.ActivityLevel = 3;
                        break;
                    case "Very Active (Intense physical work, 5-7 days of exercise)":
                        user.ActivityLevel = 4;
                        break;
                }
                dbContext.Update(user);
                dbContext.SaveChanges();
                return JsonConvert.SerializeObject("Data saved");


            }
            else
            {
                return JsonConvert.SerializeObject("Can't find user");
            }
        }

    }
}

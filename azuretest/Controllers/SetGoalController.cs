using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetGoalController : ControllerBase
    {


        AspWebApiDbContext dbContext = new AspWebApiDbContext();

        [HttpPost]
        public string setGoal(int userId, int goal, DateTime date)
        {
            List<UserInformation> stepHistoryList = dbContext.UserInformations.ToList();

            if (dbContext.StepsHistories.Where(x => x.UserId == userId).Any())
            {
                UserInformation userHistory = stepHistoryList.Where(x => x.UserId == userId).First();
                userHistory.StepGoal= goal;
                dbContext.Update(userHistory);
                dbContext.SaveChanges();

                return JsonConvert.SerializeObject("Successful");
            }
            return JsonConvert.SerializeObject("Not successful");


        }
    }
}

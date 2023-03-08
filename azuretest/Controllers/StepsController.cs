using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Linq;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepsController : ControllerBase
    {

        AspWebApiDbContext dbContext = new AspWebApiDbContext();



        


        [HttpPost]
        public int saveSteps( int userId, int steps, DateTime date)
        {

            List<StepsHistory> stepsList = dbContext.StepsHistories.ToList();
            List<UserMilestone> userMilestones = dbContext.UserMilestones.ToList();
            UserInformation user = dbContext.UserInformations.Where(x => x.UserId == userId).First();
            if (user == null)
            {
                return -1;
            }
            if (dbContext.StepsHistories.Count() != 0)
            {
                
                if (stepsList.Where(x => x.UserId == userId && x.Date.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).Any())
                {
                    var userStepHistory = stepsList.Where(x => x.UserId == userId && x.Date.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).First();
                    userStepHistory.Steps = steps;
                    userStepHistory.CaloriesBurnt = steps * 0.04;
                    userStepHistory.Distance = steps / 1200.0;
                    dbContext.Update(userStepHistory);
                    dbContext.SaveChanges();
                    if (steps >= 5000)
                    {
                        if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 1) == null)
                        {
                            UserMilestone newMilestone = new UserMilestone();
                            newMilestone.UserId = user.UserId;
                            newMilestone.MilestoneId = 1;
                            dbContext.UserMilestones.Add(newMilestone);
                            dbContext.SaveChanges();
                        }
                    }
                    if (steps >= 10000)
                    {
                        if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 2) == null)
                        {
                            UserMilestone newMilestone = new UserMilestone();
                            newMilestone.UserId = user.UserId;
                            newMilestone.MilestoneId = 2;
                            dbContext.UserMilestones.Add(newMilestone);
                            dbContext.SaveChanges();
                        }
                    }
                    if (steps >= 20000)
                    {
                        if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 3) == null)
                        {
                            UserMilestone newMilestone = new UserMilestone();
                            newMilestone.UserId = user.UserId;
                            newMilestone.MilestoneId = 3;
                            dbContext.UserMilestones.Add(newMilestone);
                            dbContext.SaveChanges();
                        }
                    }


                    return (int)userStepHistory.Steps;
                }

            }
           
            StepsHistory newStepRecord = new StepsHistory
               {
                    Steps = steps,
                    Date = date,
                    User = user
               };
             dbContext.StepsHistories.Add(newStepRecord);
             dbContext.SaveChanges();
            return -2;
            

            
        }



    }
}

using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetStepsController : ControllerBase
    {

        AspWebApiDbContext dbContext = new AspWebApiDbContext();

        [HttpPost]
        public int[] getLastWeekSteps(int userid, List<DateTime> dates)
        {
            int[] stepValues = new int[7];
            List<StepsHistory> stepHistoryList = dbContext.StepsHistories.ToList();

            if (dbContext.StepsHistories.Where(x => x.UserId == userid).Any())
            {
                
                for (int i = 0; i < 7; i++)
                {
                    if (stepHistoryList.Where(x => x.UserId == userid && x.Date.ToString("yyyy-MM-dd") == dates.ElementAt(i).ToString("yyyy-MM-dd")).Any())
                    {
                        StepsHistory userHistory = stepHistoryList.Where(x => x.UserId == userid && x.Date.ToString("yyyy-MM-dd") == dates.ElementAt(i).ToString("yyyy-MM-dd")).First();
                        stepValues[i] = (int)userHistory.Steps;
                    }
                    else 
                    {
                        stepValues[i] = 0;
                    }
                }
            }
            
            return stepValues;
        }


    }
}

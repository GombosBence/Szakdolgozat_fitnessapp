using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetStepsController : ControllerBase
    {

        AspWebApiDbContext dbContext = new AspWebApiDbContext();

        [HttpPost]
        public int getSteps(int userId, DateTime date)
        {
            List<StepsHistory> stepsList = dbContext.StepsHistories.ToList();
            UserInformation user = dbContext.UserInformations.Where(x => x.UserId == userId).First();
            
            if (stepsList.Where(x => x.UserId == userId && x.Date.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).Any())
            {
                var userStepHistory = stepsList.Where(x => x.UserId == userId && x.Date.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).First();
                return (int)userStepHistory.Steps;
            }
            return 0;
        }
    }
}

using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetStepGoalController : ControllerBase
    {
        AspWebApiDbContext dbContext = new AspWebApiDbContext();

        [HttpPost]
        public int getStepGoal(int userId)
        {
           
            if (dbContext.UserInformations.Where(x => x.UserId == userId).Any())
            {
                UserInformation user = dbContext.UserInformations.Where(x => x.UserId == userId).First();

                if(user.StepGoal != null)
                return (int)user.StepGoal;
            }

            return 5000;
        }
    }
}

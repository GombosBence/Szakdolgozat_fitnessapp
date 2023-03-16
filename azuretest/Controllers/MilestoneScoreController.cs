using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestoneScoreController : ControllerBase
    {
        AspWebApiDbContext dbContext = new AspWebApiDbContext();

        [HttpPost]
        public string milestoneScore([FromBody] int userId)
        {
            var user = dbContext.UserInformations.Where(x => x.UserId == userId).First();

            return JsonConvert.SerializeObject(user.MilestoneScore.ToString());

        }


    }
}

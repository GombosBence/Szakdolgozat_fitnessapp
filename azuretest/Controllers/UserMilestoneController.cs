using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMilestoneController : ControllerBase
    {

        AspWebApiDbContext dbContext = new AspWebApiDbContext();

        [HttpPost]

        public string myMileStones([FromBody] int userId)
        {
            List<UserMilestone> userMileStone = dbContext.UserMilestones.ToList();
            if (userMileStone.Count > 0)
            {
                List<UserMilestone> reducedList = userMileStone.FindAll(x => x.UserId == userId);
                List<Milestone> milestoneList = dbContext.Milestones.ToList();
                List<MyMilestoneModel> myList = new List<MyMilestoneModel>();

                for (int i = 0; i < reducedList.Count; i++)
                {
                    Milestone mStone = milestoneList.First(x => x.Id == reducedList[i].MilestoneId);
                    myList.Add(new MyMilestoneModel(mStone.MilestoneName, mStone.Goal, mStone.Score));
                }


                return JsonConvert.SerializeObject(myList);
            }
            return "Error";
        }


    }
}

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

        public string myMileStones([FromBody] int userId, int qVersion)
        {


            if (qVersion == 1)
            {

                List<UserMilestone> userMilestones = dbContext.UserMilestones.Where(x => x.UserId == userId).ToList();
                List<Milestone> milestones = dbContext.Milestones.ToList();
                var user = dbContext.UserInformations.First(x => x.UserId == userId);
                List<MyMilestoneModel> myList = new List<MyMilestoneModel>();
                int progress = 0;

                foreach (Milestone m in milestones)
                {
                    if (userMilestones.Any(o => o.MilestoneId == m.Id))
                    {
                        continue;
                    }
                    switch (m.Id)
                    {
                        case 1:
                            if (user.MaximumSteps == null)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (int)user.MaximumSteps;
                            }
                            break;
                        case 2:
                            if (user.MaximumSteps == null)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (int)user.MaximumSteps;
                            }
                            break;
                        case 3:
                            if (user.MaximumSteps == null)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (int)user.MaximumSteps;
                            }
                            break;
                        case 4:
                            if (user.MaximumCalStreak == null)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (int)user.MaximumCalStreak;
                            }
                            break;
                        case 5:
                            if (user.MaximumCalStreak == null)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (int)user.MaximumCalStreak;
                            }
                            break;
                        case 6:
                            if (user.MaximumCalStreak == null)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (int)user.MaximumCalStreak;
                            }
                            break;
                        case 7:
                            if (user.MaximumCalStreak == null)
                            {
                                progress = 0;
                            }
                            else
                            {
                                progress = (int)user.MaximumCalStreak;
                            }
                            break;
                        default:
                            progress = 1;
                            break;
                    }
                    myList.Add(new MyMilestoneModel(m.MilestoneName, m.Goal, m.Score, progress));

                }
                return JsonConvert.SerializeObject(myList);

            }
            else
            {
                List<UserMilestone> userMileStone = dbContext.UserMilestones.ToList();
                var user = dbContext.UserInformations.First(x => x.UserId == userId);
                if (userMileStone.Count > 0)
                {
                    List<UserMilestone> reducedList = userMileStone.FindAll(x => x.UserId == userId);
                    List<Milestone> milestoneList = dbContext.Milestones.ToList();
                    List<MyMilestoneModel> myList = new List<MyMilestoneModel>();
                    int progress = 0;

                    for (int i = 0; i < reducedList.Count; i++)
                    {
                        Milestone mStone = milestoneList.First(x => x.Id == reducedList[i].MilestoneId);
                        switch (mStone.Id)
                        {
                            case 1:
                                if (user.MaximumSteps == null)
                                {
                                    progress = 0;
                                }
                                else
                                {
                                    progress = (int)user.MaximumSteps;
                                }
                                break;
                            case 2:
                                if (user.MaximumSteps == null)
                                {
                                    progress = 0;
                                }
                                else
                                {
                                    progress = (int)user.MaximumSteps;
                                }
                                break;
                            case 3:
                                if (user.MaximumSteps == null)
                                {
                                    progress = 0;
                                }
                                else
                                {
                                    progress = (int)user.MaximumSteps;
                                }
                                break;
                            case 4:
                                if (user.MaximumCalStreak == null)
                                {
                                    progress = 0;
                                }
                                else
                                {
                                    progress = (int)user.MaximumCalStreak;
                                }
                                break;
                            case 5:
                                if (user.MaximumCalStreak == null)
                                {
                                    progress = 0;
                                }
                                else
                                {
                                    progress = (int)user.MaximumCalStreak;
                                }
                                break;
                            case 6:
                                if (user.MaximumCalStreak == null)
                                {
                                    progress = 0;
                                }
                                else
                                {
                                    progress = (int)user.MaximumCalStreak;
                                }
                                break;
                            case 7:
                                if (user.MaximumCalStreak == null)
                                {
                                    progress = 0;
                                }
                                else
                                {
                                    progress = (int)user.MaximumCalStreak;
                                }
                                break;
                            default:
                                progress = 1;
                                break;
                        }
                        myList.Add(new MyMilestoneModel(mStone.MilestoneName, mStone.Goal, mStone.Score, progress));
                    }


                    return JsonConvert.SerializeObject(myList);
                }
                return "Error";
            }
        }


    }
}

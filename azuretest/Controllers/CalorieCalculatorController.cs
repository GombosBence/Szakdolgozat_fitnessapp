using azuretest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace azuretest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalorieCalculatorController : ControllerBase
    {


        AspWebApiDbContext dbContext = new AspWebApiDbContext();


        [HttpPost]
        public string calculateMaxCalories([FromBody] int postedUserId)
        {
            List<UserInformation> datalist = dbContext.UserInformations.ToList();
            UserInformation? user = datalist.Find(x => x.UserId == postedUserId);

            if (user == null || user.Weight == null || user.Height == null || user.Age == null)
            {
                return JsonConvert.SerializeObject("User is null");
            }
            else
            {

                double brm = 0;
                if (user.Gender == "Male")
                {
                    brm = (double)(66.5 + (13.75 * user.Weight) + (5.003 * user.Height) - (6.75 * user.Age));
                }
                if (user.Gender == "Female")
                {
                    brm = (double)(655.1 + (9.563 * user.Weight) + (1.850 * user.Height) - (4.676 * user.Age));
                }

                switch (user.ActivityLevel)
                {
                    case 1:
                        brm = brm * 1.2;
                        break;
                    case 2:
                        brm = brm * 1.375;
                        break;
                    case 3:
                        brm = brm * 1.55;
                        break;
                    case 4:
                        brm = brm * 1.725;
                        break;
                }
                switch (user.Goal)
                {
                    case 1:
                        brm = brm - 200;
                        break;
                    case 2:
                        break;
                    case 3:
                        brm = brm + 200;
                        break;
                }

                int harrisBenedict = (int)Math.Round(brm);
                return JsonConvert.SerializeObject(harrisBenedict, Formatting.Indented);
            }
        }
        }
}

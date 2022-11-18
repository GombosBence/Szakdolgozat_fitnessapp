using azuretest.Models;
using azuretest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace azuretest.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
       
       AspWebApiDbContext dbContext = new AspWebApiDbContext();

        
        
        public int Getnumber() 
        {
           List<UserInformation> dataList =  dbContext.UserInformations.ToList();
           return dataList.Count;
        }

        [HttpPost]
        [Obsolete]
        public string Post([FromBody] UserInformation uInfo)
        {

            

            if (!dbContext.UserInformations.Any(user => user.Username.Equals(uInfo.Username)))
            {
                UserInformation data = new UserInformation();
                data.UserId = Getnumber() + 1;
                data.Username = uInfo.Username;
                data.Salt = Convert.ToBase64String(Common.GetRandomSalt(16));
                data.Password = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(uInfo.Password), Convert.FromBase64String(data.Salt)));
                data.Email = uInfo.Email;

                try 
                {
                    dbContext.UserInformations.Add(data);
                    dbContext.SaveChanges();
                    return JsonConvert.SerializeObject("Registration Successfull");
                }
                catch(Exception ex) 
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }

            }
            else
            {
                return JsonConvert.SerializeObject("User already exists");
            }
        }
    }
}

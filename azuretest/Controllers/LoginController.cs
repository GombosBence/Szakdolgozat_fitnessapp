using azuretest.Models;
using azuretest.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace azuretest.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        AspWebApiDbContext dbContext = new AspWebApiDbContext();

        // POST api/<LoginController>
        [HttpPost]
        [Obsolete]
        public string Post([FromBody] UserInformation userinfo)
        {
            if (dbContext.UserInformations.Any(user => user.Username.Equals(userinfo.Username)))
            {
                UserInformation user = dbContext.UserInformations.Where(u => u.Username.Equals(userinfo.Username)).First();
                var client_post_password = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(userinfo.Password), Convert.FromBase64String(user.Salt)));

                if (client_post_password.Equals(user.Password))
                {
                    
                    return JsonConvert.SerializeObject(user.UserId);
                }
                else
                {
                    return JsonConvert.SerializeObject("Incorrect password");
                }

            }
            else
            {
                return JsonConvert.SerializeObject("User doesn't exists");
            }
        }
        
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestAPI.Models;
using RestAPI.Models.Data;
using RestAPI.Models.Services;
using System.IdentityModel.Tokens.Jwt;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController:ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly PersonService personService;
        private readonly int workTimeMinutes = 10;

        public AccountController(ApplicationContext _db)
        {
            this.db = _db;
            personService = new PersonService(db);
        }
        [Authorize]
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            string userName = HttpContext.User.Identity.Name;
            var user=db.Persons.FirstOrDefault(u=>u.Email== userName);
            if (user != null) return Ok(user);
            return NotFound();
        }
        [Authorize]
        [HttpGet("workTime")]
        public int GetWorkTimeInfo()
        {
            return workTimeMinutes;
        }
        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var userData = personService.GetUserLoginFromBasicAuth(Request);
            var login = userData.Item1;
            var pass = userData.Item2;
            var indentity = personService.GetIdentity(login, pass);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE, claims: indentity.Claims, notBefore: now, expires: now.Add(TimeSpan.FromMinutes(workTimeMinutes)),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(AuthOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256)
              );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token=encodedJwt,
                username=indentity.Name
            };
            return Ok(response);
        }
    }
}

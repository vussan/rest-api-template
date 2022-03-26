using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Repositories.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities;

namespace Rest_API_Template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            //Authentication Logic
            var user = await _unitOfWork.Users.Get(u => u.Username == "admin");
            return Ok(BuildToken(user));
        }

        private static string BuildToken(User user)
        {
            var bytes = Encoding.UTF8.GetBytes(Config.TokenSecretKey);
            var key = new SymmetricSecurityKey(bytes);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateJwtSecurityToken(
                  issuer: Config.TokenIssuer,
                  audience: Config.TokenAudience,
                  subject: new ClaimsIdentity(new List<Claim>
                  {
                      new Claim(ClaimTypes.Role,user.RoleId.ToString()??""),
                      new Claim("Id",user.Id.ToString()),
                      new Claim("Username",user.Username)
                  }),
                  notBefore: DateUtility.DateTime,
                  issuedAt: DateUtility.DateTime,
                  expires: DateUtility.DateTime.AddMinutes(Config.TokenExpiryTime),
                  signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

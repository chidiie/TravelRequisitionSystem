using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelRequisitionSystem.Models;
using TravelRequisitionSystem.Services.Interfaces;

namespace TravelRequisitionSystem.Services.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response Authenticate()
        {
            var response = new Response(false);

            try
            {
                var token = GenerateToken();

                response.Data = token;
                response.IsSuccessful = true;

                return response;
            }
            catch (Exception ex)
            {

                response.IsSuccessful = false;
                response.Error = new ErrorResponse
                {
                    ResponseCode = "99",
                    ResponseDescription = ex.Message,
                };
                return response;
            }

            

        }

        public string GenerateToken()
        {
            var authClaims = new List<Claim>
            {
                //new Claim(ClaimTypes.Name, user.Username),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(

                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

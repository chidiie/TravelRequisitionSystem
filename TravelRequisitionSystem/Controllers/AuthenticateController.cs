using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelRequisitionSystem.Services.Interfaces;

namespace TravelRequisitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJWTService _iJWTService;

        public AuthenticateController(IJWTService iJWTService)
        {
            _iJWTService = iJWTService;
        }


        [HttpGet("Authenticate")]
        public IActionResult Authenticate()
        {
            var response = _iJWTService.Authenticate();
            return Ok(response);
        }
    }
}

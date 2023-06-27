using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public string Register(RegisterModel model)
        {
            return authenticationService.Register(model);
        }

        [HttpPost("login")]
        public string Login(LoginModel model)
        {
            return authenticationService.Login(model);
        }

        [HttpPost("refresh-token")]
        public string RefreshToken(string reftoken)
        {
            return authenticationService.RefreshToken(reftoken);
        }
    }
}

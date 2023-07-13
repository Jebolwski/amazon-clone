using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public ResponseViewModel Register(RegisterModel model)
        {
            return authenticationService.Register(model);
        }

        [HttpPost("login")]
        public ResponseViewModel Login(LoginModel model)
        {
            return authenticationService.Login(model);
        }

        [HttpPost("refresh-token")]
        public ResponseViewModel RefreshToken(RefreshTokenModel model)
        {
            return authenticationService.RefreshToken(model);
        }

        [HttpPost("search-by-username")]
        public ResponseViewModel SearchByUsername(string name)
        {
            return authenticationService.SearchByUsername(name);
        }
    }
}

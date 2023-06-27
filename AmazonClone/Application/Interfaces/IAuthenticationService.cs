using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface IAuthenticationService
    {
        public string Register(RegisterModel request);

        public string Login(LoginModel request);

        public string RefreshToken(string reftoken);
    }
}
    
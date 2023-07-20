using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface IAuthenticationService
    {
        public ResponseViewModel Register(RegisterModel model);
        public ResponseViewModel Login(LoginModel request);
        public ResponseViewModel RefreshToken(RefreshTokenModel model);
        public ResponseViewModel SearchByUsername(string name);
        public ResponseViewModel getUser(Guid id);
    }
}

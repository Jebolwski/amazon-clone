﻿using AmazonClone.Application.ViewModels.Auth;

namespace AmazonClone.Application.Interfaces
{
    public interface IUserService
    {
        public bool Register(RegisterModel model);
        public bool Login(LoginModel model);
    }
}

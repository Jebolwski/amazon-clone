﻿using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface IUserService
    {
        public User update(User user);
        public User getUserByToken(string token);
        public User getUserByUsername(string username);
        public User add(User user);
    }
}

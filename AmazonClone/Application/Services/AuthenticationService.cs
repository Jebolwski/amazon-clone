using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Net;
using System.Web.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Migrations;
using System.Web.Http.Description;
using System.Net.Http;

namespace AmazonClone.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserRepository userRepository;

        public AuthenticationService(IConfiguration configuration, IUserService userService, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userService = userService;
            this.userRepository = userRepository;
        }

        public string Login(LoginModel request)
        {
            User user = userRepository.getUserByUsername(request.username);
            if (user == null)
            {
                return "User not found";
            }

            if (!VerifyPasswordHash(request.password, user.passwordHash, user.passwordSalt))
            {
                return "Wrong password.";
            }
                
            string token = CreateToken(user);

            user.RefreshToken = token;
            user.TokenExpires = DateTime.UtcNow.AddDays(1);
            userRepository.update(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken,user);

            return token;
        }

        public User Register(RegisterModel model)
        {
            CreatePasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User()
            {
                username = model.username,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                TokenCreated = DateTime.UtcNow,
                TokenExpires = DateTime.UtcNow.AddMinutes(4),
            };

            user = userRepository.add(user);

            Console.WriteLine(user.passwordHash);

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, "Normal User")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken,User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            userRepository.update(user);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            return refreshToken;
        }
        
        public string RefreshToken(string reftoken)
        {
            if (reftoken != null) { 
                User user = userRepository.getUserByToken(reftoken);
                if (user == null) {
                    return null;
                }
                if (!user.RefreshToken.Equals(reftoken))
                {
                    return "Invalid Refresh Token.";
                }
                else if (user.TokenExpires < DateTime.UtcNow)
                {
                    return "Token expired.";
                }

                string token = CreateToken(user);
                var newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(newRefreshToken,user);

                
                return token;
            }
            return null;
        }

        
    }


        
}

    


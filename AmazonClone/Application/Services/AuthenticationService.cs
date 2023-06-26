using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace AmazonClone.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly ICartService cartService;
        private readonly IRoleService roleService;

        public AuthenticationService(IConfiguration configuration, IUserService userService, ICartService cartService, IRoleService roleService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.cartService = cartService;
            this.roleService = roleService;
        }

        public string Login(LoginModel request)
        {
            User user = userService.getUserByUsername(request.username);
            if (user == null)
            {
                return "User not found";
            }

            if (!VerifyPasswordHash(request.password, user.passwordHash, user.passwordSalt))
            {
                return "Wrong password.";
            }
                
            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken,user);

            var obj = new
            {
                accessToken = token,
                refreshToken = refreshToken,
            };
            return JsonConvert.SerializeObject(obj);
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
                roleId = roleService.getRole("Normal User").id
            };

            user = userService.add(user);

            cartService.addCartToUser(user.id);


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
                new Claim(ClaimTypes.Role, roleService.get(user.roleId).name),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtSettings:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(4),
                signingCredentials: creds
                
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken,User user)
        {
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            userService.update(user);
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
                User user = userService.getUserByToken(reftoken);
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

                var obj = new
                {
                    accessToken = token,
                    refreshToken = newRefreshToken,
                };
                return JsonConvert.SerializeObject(obj);
            }
            return null;
        }

        
    }


        
}

    


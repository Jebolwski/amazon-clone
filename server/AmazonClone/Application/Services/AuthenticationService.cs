using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ResponseM;

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

        public ResponseViewModel Login(LoginModel request)
        {
            User user = userService.getUserByUsername(request.username);
            if (user == null)
            {
                return new ResponseViewModel()
                {
                    message = "User not found",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }

            if (!VerifyPasswordHash(request.password, user.passwordHash, user.passwordSalt))
            {
                return new ResponseViewModel()
                {
                    message = "Wrong username or password",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            var obj = new
            {
                accessToken = token,
                refreshToken = refreshToken,
            };
            return new ResponseViewModel()
            {
                message = "Başarıyla giriş yapıldı. 🚀",
                responseModel = obj,
                statusCode = 200
            };
        }

        public ResponseViewModel Register(RegisterModel model)
        {
            CreatePasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = userService.getUserByUsername(model.username);
            if (user != null)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı adı kullanımda. 😒",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }

            user = new User()
            {
                username = model.username,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                TokenCreated = DateTime.UtcNow,
                TokenExpires = DateTime.UtcNow.AddMinutes(4),
                roleId = roleService.getRole("Normal User").id
            };

            user = userService.add(user);

            CartResponseModel cartResponseModel = (CartResponseModel)cartService.addCartToUser(user.id).responseModel;
            user.cartId = cartResponseModel.id;
            userService.update(user);

            return new ResponseViewModel()
            {
                message = "Başarıyla kayıt olundu. 😍",
                responseModel = user,
                statusCode = 200
            };
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
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds

            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, User user)
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

        public ResponseViewModel RefreshToken(string reftoken)
        {
            if (reftoken != null)
            {
                User user = userService.getUserByRefreshToken(reftoken);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Uygun olmayan yenileme tokeni. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                if (!user.RefreshToken.Equals(reftoken))
                {
                    return new ResponseViewModel()
                    {
                        message = "Uygun olmayan yenileme tokeni. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                else if (user.TokenExpires < DateTime.UtcNow)
                {
                    return new ResponseViewModel()
                    {
                        message = "Tokenin süresi geçmiş. 😐",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }

                string token = CreateToken(user);
                var newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(newRefreshToken, user);

                var obj = new
                {
                    accessToken = token,
                    refreshToken = newRefreshToken,
                };
                return new ResponseViewModel()
                {
                    message = "Token başarıyla yenilendi. 🥰",
                    responseModel = obj,
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri verilmedi. 😥",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel SearchByUsername(string username)
        {
            Console.WriteLine(username);
            if (username != null)
            {
                User user = userService.getUserByUsername(username);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı bulunmadı. 😶",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }

                UserResponseModel userResponseModel = new UserResponseModel()
                {
                    TokenCreated = user.TokenCreated,
                    cartId = user.cartId,
                    username = user.username,
                    roleId = user.roleId,
                    TokenExpires = user.TokenExpires
                };
                return new ResponseViewModel()
                {
                    statusCode = 200,
                    message = "Kullanıcı getirildi. 🥰",
                    responseModel = userResponseModel
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri girilmedi.",
                responseModel = new Object(),
                statusCode = 400
            };
        }


    }



}




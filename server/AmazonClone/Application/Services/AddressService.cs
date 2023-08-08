using System.IdentityModel.Tokens.Jwt;
using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AddressM;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Azure;

namespace AmazonClone.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUserService userService;

        public AddressService(IAddressRepository addressRepository, IUserService userService)
        {
            this.addresRepository = addressRepository;
            this.userService = userService;
        }

        private readonly IAddressRepository addresRepository;

        public ResponseViewModel addAddress(string authToken, AddressAddModel model)
        {
            if (authToken != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                Address address = new Address()
                {
                    apartmentName = model.apartmentName,
                    apartmentNo = model.apartmentNo,
                    city = model.city,
                    floor = model.floor,
                    hood = model.hood,
                    userId = user.id,
                    addressComplete = model.hood + " mah. " + model.apartmentName + " apt. d : " + model.apartmentNo + " kat : " + model.floor + " " + model.city.ToUpper()
                };
                Address addressCreated = addresRepository.add(address);
                return new ResponseViewModel()
                {
                    message = "Adres başarıyla eklendi. 😍",
                    responseModel = new AddressResponseModel()
                    {
                        addressComplete = addressCreated.addressComplete,
                        apartmentName = addressCreated.apartmentName,
                        city = addressCreated.city,
                        apartmentNo = addressCreated.apartmentNo,
                        floor = addressCreated.floor,
                        hood = addressCreated.hood,
                        user = user,
                    },
                    statusCode = 200
                };

            }
            else
            {
                return new ResponseViewModel()
                {
                    statusCode = 400,
                    message = "Token girilmedi. 😒",
                    responseModel = new Object(),
                };
            }

        }

        public ResponseViewModel deleteAddress(string authToken, Guid id)
        {
            if (authToken != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                Address address = addresRepository.get(id);
                if (address == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Adres bulunamadı. 😥",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                if (address.userId == user.id)
                {
                    addresRepository.delete(id);
                    return new ResponseViewModel()
                    {
                        message = "Başarıyla silindi. 🥰",
                        responseModel = new Object(),
                        statusCode = 200
                    };
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
            }
            return new ResponseViewModel()
            {
                message = "Kullanıcı doğrulanamadı. 😞",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel getAddresses(string authToken, Guid id)
        {
            if (authToken != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                if (user.id != id)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                List<Address> addresses = addresRepository.GetAddressesByUserId(id);
                if (addresses != null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Adresler başarıyla getirildi. 🌝",
                        responseModel = addresses,
                        statusCode = 200
                    };
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Adresler getirilirken bir hata oluştu. 😞",
                        responseModel = addresses,
                        statusCode = 400
                    };
                }
            }
            else
            {
                return new ResponseViewModel()
                {
                    statusCode = 400,
                    message = "Token girilmedi. 😒",
                    responseModel = new Object(),
                };
            }

        }

        public ResponseViewModel getAddressById(string authToken, Guid id)
        {
            if (authToken != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }

                Address address = addresRepository.get(id);
                if (user.id != address.userId)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                if (address != null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Adres başarıyla getirildi. 🌝",
                        responseModel = address,
                        statusCode = 200
                    };
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Adresler getirilirken bir hata oluştu. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
            }
            else
            {
                return new ResponseViewModel()
                {
                    statusCode = 400,
                    message = "Token girilmedi. 😒",
                    responseModel = new Object(),
                };
            }

        }

        public ResponseViewModel updateAddress(string authToken, AddressUpdateModel model)
        {
            if (model != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                Address address = this.addresRepository.get(model.id);
                if (address != null)
                {

                    if (user.id != address.userId)
                    {
                        return new ResponseViewModel()
                        {
                            message = "Kullanıcı doğrulanamadı. 😞",
                            responseModel = new Object(),
                            statusCode = 400
                        };
                    }
                    address.hood = model.hood;
                    address.city = model.city;
                    address.apartmentNo = model.apartmentNo;
                    address.apartmentName = model.apartmentName;
                    address.floor = model.floor;
                    address.addressComplete = model.hood + " mah. " + model.apartmentName + " apt. d : " + model.apartmentNo + " kat : " + model.floor + " " + model.city.ToUpper();
                    Address addressUpdated = addresRepository.update(address);
                    return new ResponseViewModel()
                    {
                        message = "Adres başarıyla güncellendi. 🥰",
                        responseModel = new AddressResponseModel()
                        {
                            addressComplete = addressUpdated.addressComplete,
                            apartmentName = addressUpdated.apartmentName,
                            city = addressUpdated.city,
                            apartmentNo = addressUpdated.apartmentNo,
                            floor = addressUpdated.floor,
                            hood = addressUpdated.hood,
                            user = user,
                        },
                        statusCode = 200
                    };
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Adres bulunamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
            }
            else
            {

                return new ResponseViewModel()
                {
                    message = "Veri girilmedli. 😶",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
        }


    }
}




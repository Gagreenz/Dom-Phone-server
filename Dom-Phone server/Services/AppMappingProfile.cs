using AutoMapper;
using Dom_Phone_server.Dtos.Payment;
using Dom_Phone_server.Dtos.User;
using Dom_Phone_server.Models;
using System.Security.Cryptography;

namespace Dom_Phone_server.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<UserUpdateDto, User>()
                .BeforeMap((src,dest) =>
                {
                    if(src.Password != null && src.Password.Length != 0)
                    {
                        CreatePasswordHash(src.Password, out byte[] passwordHash, out byte[] passwordSalt);
                        dest.PasswordSalt = passwordSalt;
                        dest.PasswordHash = passwordHash;
                    }
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null && !string.IsNullOrEmpty(srcMember.ToString())));
            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>()
                .ForAllMembers(opt => 
                    opt.Condition((src, dest, srcMember) => 
                        srcMember != null && !string.IsNullOrEmpty(srcMember.ToString())));
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

using System.Threading.Tasks;
using Microservicedemo.Common.Exceptions;
using Microservicedemo.Services.Identity.Domain.Repositories;
using Microservicedemo.Services.Identity.Domain.Services;
using Microservicedemo.Services.Identity.Domain.Models;
using System;
using Microservicedemo.Common.Authentication;

namespace Microservicedemo.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IEncryptor _encryptor;

        private readonly IJWTHandler _jwtHandler;

        public UserService(IUserRepository userRepo, IEncryptor encryptor, IJWTHandler jwtHandler)
        {
            _userRepository = userRepo;
            _encryptor =encryptor;
            _jwtHandler = jwtHandler;
        }
        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if(user == null){
                throw new LearnException($"email_not_found", $"Registered Email: {email}, not found");
            }
            if (!user.ValidatePassword(password, _encryptor))
            {
                throw new LearnException("invalid_credentials", "Invalid Password/Credentials");
            }
            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if(user!=null)
            {
                throw new LearnException("user_already_exists", $"User already exists with email {email}");
            }
            user = new User(email, name);
            Console.WriteLine("Setting password now");
            user.SetPassword(password, _encryptor);
            Console.WriteLine("Password Set");
            await _userRepository.AddUser(user);
        }
    }
}
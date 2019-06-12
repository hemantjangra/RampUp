using System;
using Microservicedemo.Common.Exceptions;
using Microservicedemo.Services.Identity.Domain.Services;

namespace Microservicedemo.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id{get; protected set;}

        public string Email{get; protected set;}

        public string Name{get; protected set;}

        public string Password{get; protected set;}

        public string Salt{get; protected set;}

        public DateTime CreatedAt{get; protected set;}


        protected User()
        {
            
        }

        public User(string email, string name)
        {
            if(string.IsNullOrEmpty(email)){
                new LearnException($"User_Email_Empty", $"User Email can not be null or empty");
            }

            if(string.IsNullOrEmpty(name)){
                new LearnException($"User_Name_Empty", "User Name can not be null or empty");
            }
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.Name = name;
            this.CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncryptor encryptor)
        {
            Salt = encryptor.GetSalt(password);
            Console.WriteLine($"Setting Password, getting salt and salt is {Salt}");
            Password = encryptor.GetHash(password, Salt);
            Console.WriteLine($"Setting password and password is {Password}");
        }

        public bool ValidatePassword(string password, IEncryptor encryptor) =>
            Password.Equals(encryptor.GetHash(password, Salt));
    }
}
namespace Microservicedemo.Common.Events
{
    public class UserAuthenticated : IEvent
    {
        public string Email {get;}

        private UserAuthenticated()
        {
            
        }

        public UserAuthenticated(string email)
        {
            this.Email = email;
        }
    }
}
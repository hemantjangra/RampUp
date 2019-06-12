namespace Microservicedemo.Common.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public string Reason {get;}

        public string Code {get;}

        public string Email{get;}

        public CreateUserRejected(string reason, string code, string email)
        {
            this.Reason = reason;
            this.Code = code;
            this.Email = email;
        }

        protected CreateUserRejected()
        {
            
        }
    }
}
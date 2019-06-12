using System;
using System.Runtime.Serialization;

namespace Microservicedemo.Common.Exceptions
{
    public class LearnException : Exception
    {
        public string Code { get; }
        public LearnException()
        {
        }

        public LearnException(string code)
        {
            this.Code = code;
        }

        public LearnException(string message, params object[] args): this(string.Empty, message, args)
        {

        }

        public LearnException(string code, string message, params object[] args): this(null, code, message, args)
        {

        }

        public LearnException(Exception innerException, string message, params object[] args) : this(innerException, string.Empty, message, args)
        {

        }

        public LearnException(Exception innerException, string code, string message, params object[] args):base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
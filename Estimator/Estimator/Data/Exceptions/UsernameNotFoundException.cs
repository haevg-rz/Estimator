using System;

namespace Estimator.Data.Exceptions
{
    public class UsernameNotFoundException : Exception
    {
        private const string message = "Username is not found!";

        public UsernameNotFoundException() : base(message)
        {
        }
    }
}
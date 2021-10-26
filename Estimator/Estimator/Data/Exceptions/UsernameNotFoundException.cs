using System;

namespace Estimator.Data.Exceptions
{
    public class UsernameNotFoundException : Exception
    {
        const string message = "Username is not found!";

        public UsernameNotFoundException() : base(message)
        {

        }
    }
}
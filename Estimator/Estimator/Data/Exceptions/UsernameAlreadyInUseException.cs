using System;

namespace Estimator.Data.Exceptions
{
    public class UsernameAlreadyInUseException : Exception
    {
        const string message = "Username is already in use!";

        public UsernameAlreadyInUseException() : base(message)
        {

        }
    }
}
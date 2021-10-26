using System;
using System.Runtime.CompilerServices;

namespace Estimator.Data.Exceptions
{
    public class RoomIdNotFoundException : Exception
    {
        const string message = "RoomId not found!";

        public RoomIdNotFoundException() : base(message)
        {

        }

    }
}
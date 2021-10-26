using System;

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
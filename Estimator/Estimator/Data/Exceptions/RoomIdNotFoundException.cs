using System;

namespace Estimator.Data.Exceptions
{
    public class RoomIdNotFoundException : Exception
    {
        private const string message = "RoomId not found!";

        public RoomIdNotFoundException() : base(message)
        {
        }
    }
}
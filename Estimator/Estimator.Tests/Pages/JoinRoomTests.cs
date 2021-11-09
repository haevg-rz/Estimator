using System;
using Estimator.Data;
using Estimator.Pages;
using Xunit;

namespace Estimator.Tests.Pages
{
    public class JoinRoomTests
    {
        [Theory]
        [InlineData("12345", "Max Mustermann", false)]
        [InlineData("", "", true)]
        [InlineData("123456", "", true)]
        public void IsParameterEmptyTest(string roomId, string userName, bool isParameterEmpty)
        {
            #region Assign

           var joinRoom = new JoinRoom();

           joinRoom.RoomId = roomId;
           joinRoom.Username = userName;

            #endregion

            #region Act

            var result = joinRoom.IsParameterEmpty();

            #endregion

            #region Assert

            Assert.Equal(isParameterEmpty, result);

            #endregion
        }
    }
}

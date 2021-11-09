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
        public void IsUsernameOrRoomIdEmptyTest(string roomId, string userName, bool isUsernameOrRoomIdEmpty)
        {
            #region Assign

           var joinRoom = new JoinRoom();

           joinRoom.RoomId = roomId;
           joinRoom.Username = userName;

            #endregion

            #region Act

            var result = joinRoom.IsUsernameOrRoomIdEmpty();

            #endregion

            #region Assert

            Assert.Equal(isUsernameOrRoomIdEmpty, result);

            #endregion
        }
    }
}

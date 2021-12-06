using Estimator.Data.Enum;
using Estimator.Pages;
using Xunit;

namespace Estimator.Tests.Pages
{
    public class CreateRoomTests
    {
        [Theory]
        [InlineData("Max Mustermann", false)]
        [InlineData("", true)]
        public void IsUsernameEmptyTest(string userName, bool isUsernameEmptyExpected)
        {
            #region Assign

            var creatRoom = new CreateRoom
            {
                Username = userName
            };

            #endregion

            #region Act

            var result = creatRoom.IsUsernameEmpty();

            #endregion

            #region Assert

            Assert.Equal(isUsernameEmptyExpected, result);

            #endregion
        }

        [Theory]
        [InlineData("Fibonacci", RoomType.Fibonacci)]
        [InlineData("T-Shirt", RoomType.Tshirt)]
        [InlineData("Test", RoomType.Tshirt)]
        public void ConvertTypeTest(string type , RoomType convertType)
        {
            #region Assign

            var createRoom = new CreateRoom();

            #endregion

            #region Act

            var result = createRoom.ConvertType(type);

            #endregion

            #region Assert

            Assert.Equal(convertType, result);

            #endregion
        }

    }
}

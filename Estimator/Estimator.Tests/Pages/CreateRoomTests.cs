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
        [InlineData("Fibonacci", RoomTypes.Fibonacci)]
        [InlineData("T-Shirt", RoomTypes.Tshirt)]
        [InlineData("Test", RoomTypes.Tshirt)]
        public void ConvertTypeTest(string type , RoomTypes convertType)
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

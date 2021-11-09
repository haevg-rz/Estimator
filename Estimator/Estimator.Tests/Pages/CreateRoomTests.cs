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
        [InlineData("Fibonacci", 1)]
        [InlineData("T-Shirt", 2)]
        [InlineData("Test", 2)]
        public void ConvertTypeTest(string type , int convertType)
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

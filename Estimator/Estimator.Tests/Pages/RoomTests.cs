using Estimator.Data;
using Estimator.Data.Interface;
using Moq;
using Xunit;

namespace Estimator.Tests.Pages
{
    public class RoomTests
    {
        private const string result = "result";
        private const string title = "title";

        [Fact]
        public void SetNewTitleTest()
        {
            #region Assign

            var mock = new Mock<IRoom>();
            mock.SetupGet(m => m.estimationSuccessful).Returns(true);
            mock.SetupGet(m => m.estimationClosed).Returns(true);
            mock.SetupGet(m => m.Result).Returns(result);
            mock.Setup(m => m.UpdateView()).Verifiable();

            var room = new Estimator.Pages.Room();

            room = (Estimator.Pages.Room)mock.Object;
            mock.Object.SetNewTitel(title);

            #endregion

            #region Act

            room.SetNewTitel(title);

            #endregion

            #region Assert

            Assert.False(mock.Object.estimationSuccessful);

            #endregion
        }
    }
}

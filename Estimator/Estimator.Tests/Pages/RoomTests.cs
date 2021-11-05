using System;
using System.Reflection;
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
            mock.SetupSet(m => m.estimationSuccessful = true);
            mock.SetupSet(m => m.estimationClosed = true);
            mock.SetupSet(m => m.Result = result);
            mock.Setup(m => m.UpdateView()).Verifiable();

            #endregion

            #region Act

            mock.Object.SetNewTitel(title);

            #endregion

            #region Assert

            Assert.False(mock.Object.estimationSuccessful);

            #endregion
        }
    }
}

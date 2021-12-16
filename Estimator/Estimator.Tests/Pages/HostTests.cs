using Estimator.Data.Interface;
using Moq;
using Xunit;

namespace Estimator.Tests.Pages
{
    public class HostTests
    {
        [Fact]
        public void OnDeleteDialogCloseTest()
        {
            #region Assign

            var mock = new Moq.Mock<IHost>();
            mock.Setup(k => k.StateHasChanged());

            #endregion

            #region Act

            #endregion

            #region Assert

            #endregion
        }
    }
}
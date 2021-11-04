using Estimator.Data.Interface;
using Estimator.Pages;
using Moq;
using Xunit;

namespace Estimator.Tests.Pages
{
    public class HostTests
    {
        [Fact]
        public void SetDiagramTest()
        {
            #region Assign

            var mock = new Mock<IHost>();
            mock.SetupGet(x => x.JsRuntime);

            var host = new Host
            {
                Username = "host",
                RoomId = "123456",
                RoomManager = Samples.RoomSample.GetRoomManagerSample()
            };



            host.CloseEstimation();

            #endregion

            #region Act

            host.SetDiagram();

            #endregion

            #region Assert

            Assert.Equal(3, host.diagramData.Count);

            #endregion
        }
    }
}

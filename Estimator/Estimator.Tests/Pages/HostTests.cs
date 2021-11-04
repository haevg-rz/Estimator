using Estimator.Data.Interface;
using Estimator.Pages;
using Telerik.JustMock;
using Xunit;

namespace Estimator.Tests.Pages
{
    public class HostTests
    {
        [Fact]
        public void SetDiagramTest()
        {
            #region Assign

            var host = new Host
            {
                Username = "host",
                RoomId = "123456",
                RoomManager = Samples.RoomSample.GetRoomManagerSample()
            };

            Mock.Arrange(() => host.Alert(Arg.AnyString)).DoNothing().MustBeCalled();
            Mock.Arrange(() => host.NavigateTo(Arg.AnyString)).DoNothing().MustBeCalled();
            Mock.Arrange(() => host.CopyToClipboard(Arg.AnyString)).DoNothing().MustBeCalled();
            Mock.Arrange(() => host.GeneratePieChart()).DoNothing().MustBeCalled();


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
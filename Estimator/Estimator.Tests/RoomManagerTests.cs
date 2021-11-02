using Estimator.Data;
using Estimator.Data.Exceptions;
using Xunit;

namespace Estimator.Tests
{
    public class RoomManagerTests
    {
        #region fields

        private const string roomId = "12345";
        private const string wrongRoomId = "12346";
        private const string title = "TestEstimation";
        private const string estimationEntry = "testEstimation";

        private const string estimatorName = "Nadine";
        private const string hostName = "Max Mustermann";

        private const int type = 1; // 1 = FibonacciNumbers

        #endregion

        [Fact]
        public void CreateRoomTest()
        {
            #region Assign

            var roomManager = new RoomManager();

            var estimator = new Data.Estimator(hostName, string.Empty);

            #endregion

            #region Act

            var resultRoomId = roomManager.CreateRoom(type, estimator);

            #endregion

            #region Assert

            Assert.Equal(6, resultRoomId.Length);
            Assert.Single(roomManager.rooms);

            #endregion
        }

        [Fact]
        public void CloseRoomTest()
        {
            #region Assign

            var roomManager = new RoomManager();

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), 1));

            #endregion

            #region Act

            roomManager.CloseRoom(roomId);

            #endregion

            #region Assert

            Assert.Empty(roomManager.rooms);

            #endregion
        }

        [Fact]
        public void JoinRoomTest()
        {
            #region Assign

            var roomManager = new RoomManager();

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));

            #endregion

            #region Act

            roomManager.JoinRoom(roomId, estimatorName);

            #endregion

            #region Assert

            Assert.Equal(2, roomManager.rooms[0].estimators.Count);
            Assert.Equal("Nadine", roomManager.rooms[0].estimators[1].Name);

            #endregion
        }

        [Fact]
        public void JoinRoomExceptionRoomIdNotFoundTest()
        {
            #region Assign

            var roomManager = new RoomManager();

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));

            #endregion

            #region Act

            var e = Assert.Throws<RoomIdNotFoundException>(() =>
                roomManager.JoinRoom(wrongRoomId, estimatorName));

            #endregion

            #region Assert

            Assert.Equal("RoomId not found!", e.Message);

            #endregion
        }

        [Fact]
        public void JoinRoomExceptionUsernameAlreadyInUseTest()
        {
            #region Assign

            var roomManager = new RoomManager();

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));

            #endregion

            #region Act

            var e = Assert.Throws<UsernameAlreadyInUseException>(() =>
                roomManager.JoinRoom(roomId, hostName));

            #endregion

            #region Assert

            Assert.Equal("Username is already in use!", e.Message);

            #endregion
        }

        [Fact]
        public void LeaveRoomTest()
        {
            #region Assign

            var roomManager = new RoomManager();
            var estimator = new Data.Estimator(estimatorName, string.Empty);

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));
            roomManager.rooms[0].estimators.Add(estimator);

            #endregion

            #region Act

            roomManager.LeaveRoom(estimator, roomId);

            #endregion

            #region Assert

            Assert.Single(roomManager.rooms[0].estimators);

            #endregion
        }

        [Fact]
        public void StartEstimationTest()
        {
            #region Assign

            var roomManager = new RoomManager();
            var estimator = new Data.Estimator(estimatorName, estimationEntry);

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));
            roomManager.rooms[0].estimators.Add(estimator);

            #endregion

            #region Act

            roomManager.StartEstimation(roomId, title);

            #endregion

            #region Assert

            Assert.Equal(string.Empty, roomManager.rooms[0].estimators[0].Estimation);
            Assert.Equal("TestEstimation", roomManager.rooms[0].titel);

            #endregion
        }

        [Fact]
        public void EntryVoteTest()
        {
            #region Assign

            var roomManager = new RoomManager();
            var estimator = new Data.Estimator(estimatorName, string.Empty);

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));
            roomManager.rooms[0].estimators.Add(estimator);

            var estimatorWithNewEstimation = new Data.Estimator(estimatorName, estimationEntry);

            #endregion

            #region Act

            roomManager.EntryVote(estimatorWithNewEstimation, roomId);

            #endregion

            #region Assert

            Assert.Equal("testEstimation", roomManager.rooms[0].estimators[1].Estimation);

            #endregion
        }

        [Fact]
        public void GetRoomTypeTest()
        {
            #region Assign

            var roomManager = new RoomManager();
            var estimator = new Data.Estimator(estimatorName, string.Empty);

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));
            roomManager.rooms[0].estimators.Add(estimator);

            #endregion

            #region Act

            var result = roomManager.GetRoomType(roomId, estimatorName);

            #endregion

            #region Assert

            Assert.Equal(1, result);

            #endregion
        }

        [Theory]
        [InlineData("host", "host", true)]
        [InlineData("host", "heins", false)]
        public void IsHostTest(string host, string hostName, bool isHost)
        {
            #region Assign

            var roomManager = new RoomManager();
            var estimator = new Data.Estimator(host);

            roomManager.rooms.Add(new Room(roomId, new Data.Estimator(hostName), type));

            #endregion

            #region Act

            var result = roomManager.IsHost(host, roomId);

            #endregion

            #region Assert

            Assert.Equal(result, isHost);

            #endregion
        }
    }
}
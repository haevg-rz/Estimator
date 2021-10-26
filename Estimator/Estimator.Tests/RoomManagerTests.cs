using Estimator.Data;
using Estimator.Data.Exceptions;
using System;
using Xunit;

namespace Estimator.Tests
{
    public class RoomManagerTests
    {
        #region fields

        private readonly string roomId = "12345";
        private readonly string wrongRoomId = "12346";
        private readonly string title = "TestEstimation";
        private readonly string estimationEntry = "testEstimation";

        private readonly string estimatorName = "Nadine";
        private readonly string hostName = "Max Mustermann";

        private readonly int type = 1; // 1 = fiboncinumbers

        #endregion

        [Fact]
        public void CreateRoomTest()
        {
            #region Assign

            var roomManager = new RoomManager();

            var estimator = new Data.Estimator(this.hostName, String.Empty);

            #endregion

            #region Act

            var resultRoomId = roomManager.CreateRoom(this.type, estimator);

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

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), 1));

            #endregion

            #region Act

            roomManager.CloseRoom(this.roomId);

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

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));

            #endregion

            #region Act

            roomManager.JoinRoom(this.roomId, this.estimatorName);

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

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));

            #endregion

            #region Act

            var e = Assert.Throws<RoomIdNotFoundException>(() => roomManager.JoinRoom(this.wrongRoomId, this.estimatorName));

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

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));

            #endregion

            #region Act

            var e = Assert.Throws<UsernameAlreadyInUseException>(() => roomManager.JoinRoom(this.roomId, this.hostName));

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
            var estimator = new Data.Estimator(this.estimatorName, String.Empty);

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));
            roomManager.rooms[0].estimators.Add(estimator);

            #endregion

            #region Act

            roomManager.LeaveRoom(estimator, this.roomId);

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
            var estimator = new Data.Estimator(this.estimatorName, this.estimationEntry);

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));
            roomManager.rooms[0].estimators.Add(estimator);

            #endregion

            #region Act

            roomManager.StartEstimation(this.roomId, this.title);

            #endregion

            #region Assert

            Assert.Equal(String.Empty, roomManager.rooms[0].estimators[0].Estimation);
            Assert.Equal("TestEstimation", roomManager.rooms[0].titel);

            #endregion

        }

        [Fact]
        public void EntryVoteTest()
        {
            #region Assign

            var roomManager = new RoomManager();
            var estimator = new Data.Estimator(this.estimatorName, String.Empty);

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));
            roomManager.rooms[0].estimators.Add(estimator);

            var estimatorWithNewEstimation = new Data.Estimator(this.estimatorName, this.estimationEntry);

            #endregion

            #region Act

            roomManager.EntryVote(estimatorWithNewEstimation, this.roomId);

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
            var estimator = new Data.Estimator(this.estimatorName, String.Empty);

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));
            roomManager.rooms[0].estimators.Add(estimator);

            #endregion

            #region Act

            var type = roomManager.GetRoomType(this.roomId, this.estimatorName);

            #endregion

            #region Assert

            Assert.Equal(1, type);

            #endregion

        }

        [Fact]
        public void IsHostTest()
        {
            #region Assign

            var roomManager = new RoomManager();
            var estimator = new Data.Estimator(this.estimatorName, String.Empty);

            roomManager.rooms.Add(new Room(this.roomId, new Data.Estimator(this.hostName), this.type));


            #endregion

            #region Act

            var isHost = roomManager.IsHost(this.hostName, this.roomId);

            #endregion

            #region Assert

            Assert.True(isHost);

            #endregion

        }
    }
}

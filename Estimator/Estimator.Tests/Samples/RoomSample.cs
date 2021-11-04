using Estimator.Data;
using System.Collections.Generic;

namespace Estimator.Tests.Samples
{
    public static class RoomSample
    {
        private static RoomManager RoomManager { get; } = new RoomManager()
        {
            Rooms = new List<Room>
            {
                new Room("123456", new Data.Model.Estimator("host"), 1)
            }
        };
        
        public static RoomManager GetRoomManagerSample()
        {
            RoomManager.JoinRoom("123456", "name1");
            RoomManager.JoinRoom("123456", "name2");
            RoomManager.JoinRoom("123456", "name3");
            RoomManager.JoinRoom("123456", "name4");

            RoomManager.EntryVote(new Data.Model.Estimator("name1", "3"), "123456");
            RoomManager.EntryVote(new Data.Model.Estimator("name2", "3"), "123456");
            RoomManager.EntryVote(new Data.Model.Estimator("name3", "5"), "123456");
            RoomManager.EntryVote(new Data.Model.Estimator("name4", "8"), "123456");
            RoomManager.EntryVote(new Data.Model.Estimator("host", "8"), "123456");

            return RoomManager;
        }
    }
}

using Estimator.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estimator.Data
{
    public class RoomManager
    {
        #region fields

        private List<Room> rooms = new List<Room>();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        #endregion

        #region public

        public string CreateRoom(string titel, int type, Estimator estimator)
        {
            var roomId = this.GetRoomId();
            this.rooms.Add(new Room(roomId, estimator, type));

            return roomId;
        }

        public void CloseRoom(string roomId)
        {
            this.rooms.RemoveAll(r => r.GetRoomID() == roomId);
        }

        public int JoinRoom(string roomId, string estimatorName)
        {
            if (this.rooms.Any(r => r.GetRoomID() == roomId || r.IsEstimatorRegistered(estimatorName))) return 0;

            foreach (var r in this.rooms.Where(r => r.GetRoomID() == roomId)) r.AddEstimator(new Estimator(estimatorName));

            return this.rooms.Where(r => r.GetRoomID() == roomId).Select(r => r.GetRoomType()).FirstOrDefault();
        }

        public void LeaveRoom(Estimator estimator, string roomId)
        {
            this.roomDictonary[roomId].RemoveVoter(estimator);
        }

        public void EntryVote(Estimator estimator, string roomId)
        {
            this.roomDictonary[roomId].SetVote(estimator);
        }

        public void StartVoting(string roomId, string taskname)
        {
            this.roomDictonary[roomId].ResetAllVotes();
            this.roomDictonary[roomId].SetTaskName(taskname);
        }

        public List<DiagramData> CloseVoting(string roomId, int type)
        {
            this.roomDictonary[roomId].SetDiagramList(type);
            return this.roomDictonary[roomId].GetDiagramList();
        }

        #endregion

        #region private

        private string GetRoomId()
        {
            var stringChars = new char[6];
            var random = new Random();
            var isRoomIdUnique = false;

            do
            {
                for (var i = 0; i < stringChars.Length; i++) stringChars[i] = chars[random.Next(chars.Length)];

                isRoomIdUnique = !(this.rooms.Any(e => e.GetRoomID() == stringChars.ToString()));

            } while (isRoomIdUnique);

            return stringChars.ToString();
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using Estimator.Data.Model;
using Exception = System.Exception;

namespace Estimator.Data
{
    public class RoomManager
    {

        #region fields

        private Dictionary<string, Room> roomDictonary = new Dictionary<string, Room>();

        #endregion

        #region public

        public string CreateRoom(string taskName, int type, Voter voter)
        {
            var isNewRoomId = false;
            var roomId = String.Empty;
            do
            {
                roomId = this.GetRoomId();
                if (this.roomDictonary.ContainsKey(roomId) == false)
                    isNewRoomId = true;

            } while (!isNewRoomId);

            this.roomDictonary.Add(roomId, new Room(taskName, type));
            this.roomDictonary[roomId].AddVoter(voter);
            return roomId;
        }

        public Exceptions CloseRoom(string roomId)
        {
            if (!ExistRoomId(roomId))
                return new Exceptions(true, "The RoomID is not exist");

            this.roomDictonary.Remove(roomId);
            return new Exceptions();
        }

        public (Exceptions exception,int type) JoinRoom(string roomId, string name)
        {
            if (!ExistRoomId(roomId))
                return (new Exceptions(true, "The RoomID is not exist"),0);

            if (this.roomDictonary[roomId].IsUsernameAvailable(name))
                return (new Exceptions(true, "Username is Available"), 0);

            this.roomDictonary[roomId].AddVoter(new Voter(name, String.Empty));
            return this.roomDictonary.ContainsKey(roomId) ? (new Exceptions(),0) : (new Exceptions(), this.roomDictonary[roomId].GetType());
        }

        public Exceptions LeaveRoom(Voter voter, string roomId)
        {
            if (!ExistRoomId(roomId))
                return new Exceptions(true, "The RoomID is not exist");

            this.roomDictonary[roomId].RemoveVoter(voter);
            return new Exceptions();
        }

        public Exceptions EntryVote(Voter voter,string roomId)
        {
            if (!ExistRoomId(roomId))
                return new Exceptions(true, "The RoomID is not exist");

            if (!this.roomDictonary[roomId].IsUsernameAvailable(voter.Name))
                return new Exceptions(true,"Username is not Available");

            this.roomDictonary[roomId].SetVote(voter);

            return new Exceptions();
        }

        public Exceptions StartVoting(string roomId, string taskname)
        {
            if (!ExistRoomId(roomId))
                return new Exceptions(true, "The RoomID is not exist");

            this.roomDictonary[roomId].ResetAllVotes();
            this.roomDictonary[roomId].SetTaskName(taskname);

            return new Exceptions();
        }

        public (Exceptions exception, List<DiagramData> diagrammDataList) CloseVoting(string roomId,int type)
        {
            if (!ExistRoomId(roomId))
                return (new Exceptions(true, "The RoomID is not exist"),null);

            this.roomDictonary[roomId].SetDiagramList(type);
            return (new Exceptions() ,this.roomDictonary[roomId].GetDiagramList());
        }

        #endregion

        #region private

        private string GetRoomId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        private bool ExistRoomId(string roomId)
        {
            return this.roomDictonary.ContainsKey(roomId);
        }

        #endregion
    }
}

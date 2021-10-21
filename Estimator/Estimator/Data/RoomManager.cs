using System;
using System.Collections.Generic;
using System.Diagnostics;
using Estimator.Data.Model;

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
            //Trace.WriteLine("hello");
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

        public void CloseRoom(string roomId)
        {
            this.roomDictonary.Remove(roomId);
        }

        public (bool sucess,int type) JoinRoom(string roomId, string name)
        {
            var hasVoterJoin = this.roomDictonary[roomId].IsVoterJoin(name);

            if (hasVoterJoin)
                return (false,0);

            this.roomDictonary[roomId].AddVoter(new Voter(name, String.Empty));
            return this.roomDictonary.ContainsKey(roomId) ? (false,0) : (true,this.roomDictonary[roomId].GetType());
        }

        public void LeaveRoom(Voter voter, string roomId)
        {
            this.roomDictonary[roomId].RemoveVoter(voter);
        }

        public void EntryVote(Voter voter,string roomId)
        {
            this.roomDictonary[roomId].SetVote(voter);
        }

        public void StartVoting(string roomId, string taskname)
        {
            this.roomDictonary[roomId].ResetAllVotes();
            this.roomDictonary[roomId].SetTaskName(taskname);
        }

        public List<DiagramData> CloseVoting(string roomId,int type)
        {
            this.roomDictonary[roomId].SetDiagramList(type);
            return this.roomDictonary[roomId].GetDiagramList();
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

        #endregion
    }
}

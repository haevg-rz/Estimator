using System;
using System.Collections.Generic;

namespace Estimator.Data
{
    public class RoomManager
    {
        private Dictionary<string, Room> roomDictonary = new Dictionary<string, Room>();

        public string CreateRoom(string name, int type)
        {
            var isNewRoomId = false;
            var roomId = String.Empty;
            do
            {
                roomId = this.GetRoomId();
                if (this.roomDictonary.ContainsKey(roomId) == false)
                    isNewRoomId = true;

            } while (!isNewRoomId);

            this.roomDictonary.Add(roomId, new Room(name, type));
            return roomId;
        }

        public int JoinRoom(string roomId, string name)
        {
            var hasVoterJoin = this.roomDictonary[roomId].HasVoterJoin(name);

            if (hasVoterJoin)
                return 0;

            this.roomDictonary[roomId].AddVoter(new Voter(name, null));
            return this.roomDictonary.ContainsKey(roomId) ? 0 : this.roomDictonary[roomId].GetType();
        }

        public void CloseRoom(string roomId)
        {
            this.roomDictonary.Remove(roomId);
        }

        public void VoterEntry(Voter voter,string roomId)
        {
            this.roomDictonary[roomId].AddVote(voter);
        }

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
    }
}

using System;
using System.Collections.Generic;

namespace Estimator.Data
{
    public class RoomManager
    {
        private Dictionary<string, Room> roomList = new Dictionary<string, Room>();

        public void CreateRoom(string name, int type)
        {
            var isNewRoomId = false;
            var roomId = String.Empty;
            do
            {
                roomId = this.RandomString(6);
                if (this.roomList.ContainsKey(roomId) == false)
                    isNewRoomId = true;

            } while (!isNewRoomId);

            this.roomList.Add(roomId, new Room(name, type));
        }

        public int JoinRoom(string roomId)
        {
            return this.roomList.ContainsKey(roomId) ? 0 : this.roomList[roomId].GetType();
        }

        public void CloseRoom(string roomId)
        {
            this.roomList.Remove(roomId);
        }

        public void VoterEntry(string user, string vote,string roomId)
        {
            this.roomList[roomId].AddUser(user,vote);
        }

        private string RandomString(int lenght)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[lenght];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}

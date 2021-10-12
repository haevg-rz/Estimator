using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimator.Data.Model;

namespace Estimator.Data
{
    public class RoomManager
    {
        public Dictionary<string, List<VotingResults>> RoomList = new Dictionary<string, List<VotingResults>>();
        public string CreateRoom(List<VotingResults> createList)
        {
            var isNewRoomId = false;
            var roomId = String.Empty;
            do
            {
                roomId = this.RandomString(6);
                if (this.RoomList.ContainsKey(roomId) == false)
                    isNewRoomId = true;

            } while (!isNewRoomId);

            this.RoomList.Add(roomId, createList);
            return roomId;
        }

        public List<VotingResults> JoinRoom(string roomId)
        {

            return this.RoomList.ContainsKey(roomId) ? this.RoomList[roomId] : null;
        }

        public void CloseRoom(string roomId)
        {
            this.RoomList.Remove(roomId);
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

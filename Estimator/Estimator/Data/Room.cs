using System.Collections.Generic;

namespace Estimator.Data
{
    public class Room
    {
        private string roomName;
        private int type;

        private List<Voter> user;

        public Room(string name, int type)
        {
            this.roomName = name;
            this.type = type;
            this.user = new List<Voter>();
        }

        public void AddUser(string user, string vote)
        {
            this.user.Add(new Voter(user,vote));
        }

        public int GetType()
        {
            return this.type;
        }
    }
}

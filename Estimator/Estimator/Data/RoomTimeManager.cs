using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Estimator.Data
{
    public class RoomTimeManager
    {

        private System.Timers.Timer roomTimer;
        private string roomId;

        public async void SetRoomTimer(string roomId)
        {
            this.roomId = roomId;
            this.roomTimer = new System.Timers.Timer(20000);
            this.roomTimer.Elapsed += RoomTimedEvent;
            this.roomTimer.Enabled = true;
        }

        private void RoomTimedEvent(Object source, ElapsedEventArgs e)
        {
            Instances.RoomManager.CloseRoom(this.roomId);
        }

    }
}

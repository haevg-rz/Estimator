using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Estimator.Data
{
    public class RoomTimeManager
    {

        private Timer roomTimer;
        private string roomId;

        private const int timerIntervall = 24 * 60 * 60 * 1000; // 24h intervall

        public async void SetRoomTimer(string roomId)
        {
            this.roomId = roomId;
            this.roomTimer = new Timer(timerIntervall);
            this.roomTimer.Elapsed += RoomTimedEvent;
            this.roomTimer.Enabled = true;
        }

        private void RoomTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.roomTimer.Stop();
            Instances.RoomManager.CloseRoom(this.roomId);
        }

    }
}

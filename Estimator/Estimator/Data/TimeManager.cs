using System.Timers;

namespace Estimator.Data
{
    public class TimeManager
    {
        private Timer roomTimer;
        private string roomId;

        private const int timerIntervall = 24 * 60 * 60 * 1000; // 24h intervall

        public async void SetRoomTimer(string roomId)
        {
            this.roomId = roomId;
            this.roomTimer = new Timer(timerIntervall);
            this.roomTimer.Elapsed += this.RoomTimedEvent;
            this.roomTimer.Enabled = true;
        }

        private void RoomTimedEvent(object source, ElapsedEventArgs e)
        {
            this.roomTimer.Stop();
            Instances.RoomManager.CloseRoom(this.roomId);
        }
    }
}
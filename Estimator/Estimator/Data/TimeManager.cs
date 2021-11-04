﻿using System.Timers;
using Estimator.Data.Interface;
using Microsoft.AspNetCore.Components;

namespace Estimator.Data
{
    public class TimeManager
    {
        [Inject] private IRoomManager RoomManager { get; }
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
            this.RoomManager.CloseRoom(this.roomId);
        }
    }
}
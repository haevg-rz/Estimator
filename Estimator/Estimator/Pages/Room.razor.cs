using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using System;

namespace Estimator.Pages
{
    public partial class Room
    {
        [Parameter] public string RoomId { get; set; } = String.Empty;
        [Parameter] public string Username { get; set; } = String.Empty;
        public bool isFibonacci { get; set; } = false;

        public Room()
        {

        }

        private async void Estimate()
        {
            var estimation = "3";
            try
            {
                Data.Instances.RoomManager.EntryVote(new Data.Estimator(this.Username, estimation), this.RoomId); //TODO

            }
            catch (UsernameNotFoundException e)
            {
                //TODO
            }
            catch (Exception e)
            {
                //TODO
            }
        }
    }
}

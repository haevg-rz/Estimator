using System;
using System.Diagnostics;
using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using Estimator = Estimator.Data.Estimator;

namespace Estimator.Pages
{
    public partial class JoinGroup
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        private async void JoinRoom()
        {
            if (this.Username != string.Empty || this.RoomId != string.Empty)
            {
                //TODO
            }

            try
            {            
                Data.Instances.RoomManager.JoinRoom(this.RoomId, this.Username);
                this.NavigationManager.NavigateTo($"room/{this.RoomId}/{this.Username}");
            }
            catch (RoomIdNotFoundException e)
            {
                //TODO
            }
            catch (UsernameAlreadyInUseException e)
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
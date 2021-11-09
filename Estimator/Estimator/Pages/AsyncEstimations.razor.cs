using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Estimator.Pages
{
    public partial class AsyncEstimations
    {
        [Parameter] public string Username { get; set; }
        [Parameter] public string RoomId { get; set; }

        private async Task JoinAsyncRoom()
        {
            if (this.IsUsernameOrRoomIdEmpty())
            {
                await this.Alert("Username or RoomId is empty!");
                return;
            }

            if (this.RoomManager.IsHost(this.Username, this.RoomId))
            {
                await this.Alert("You are not the host in this room");
                return;
            }

            try
            {
                this.NavigateTo($"/host/{this.RoomId}/{this.Username}");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Something went wrong!");
            }
        }
        private bool IsUsernameOrRoomIdEmpty()
        {
            return this.Username.Equals(string.Empty) || this.RoomId.Equals(string.Empty);
        }

        private async Task Alert(string alertMessage)
        {
            await this.JsRuntime.InvokeVoidAsync("alert", alertMessage);
        }
        private void NavigateTo(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }
    }
}

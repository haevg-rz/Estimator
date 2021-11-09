using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Estimator.Tests.Pages")]

namespace Estimator.Pages
{
    public partial class JoinRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        private async void JoinRoomById()
        {

            if(this.IsUsernameOrRoomIdEmpty())
            {
                await Alert("Username or RoomId is empty!");
                return;
            }

            try
            {
                this.RoomManager.JoinRoom(this.RoomId, this.Username);
                this.NavigateTo($"room/{this.RoomId}/{this.Username}");
            }
            catch (RoomIdNotFoundException e)
            {
                await this.Alert(e.Message);
            }
            catch (UsernameAlreadyInUseException e)
            {
                await this.Alert(e.Message);
            }
            catch (Exception e)
            {
                await this.Alert("Something went wrong!");
            }
        }
        internal bool IsUsernameOrRoomIdEmpty()
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
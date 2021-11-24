using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Estimator.Shared;

[assembly: InternalsVisibleTo("Estimator.Tests.Pages")]

namespace Estimator.Pages
{
    public partial class JoinRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        private async void JoinRoomById()
        {
            if (this.IsUsernameOrRoomIdEmpty())
            {
                await this.Alert("Username or RoomId is empty!");
                return;
            }

            if (this.RoomManager.IsSolidInput(this.Username))
            {
                await this.Alert("Username is not solid!\n Please use only A-Z, a-z and 0-9");
                return;
            }

            try
            {
                if (this.RoomManager.IsRoomAsync(this.RoomId) && this.RoomManager.IsHost(this.Username, this.RoomId))
                {
                    await this.MainLayout.HideNavMenue();
                    this.NavigateTo($"host/{this.RoomId}/{this.Username}");
                }
                else
                {
                    this.RoomManager.JoinRoom(this.RoomId, this.Username);
                    await this.MainLayout.HideNavMenue();
                    this.NavigateTo($"room/{this.RoomId}/{this.Username}");
                }
            }
            catch (RoomIdNotFoundException e)
            {
                await this.Alert(e.Message);
            }
            catch (UsernameAlreadyInUseException e)
            {
                await this.Alert(e.Message);
            }
            catch (Exception)
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
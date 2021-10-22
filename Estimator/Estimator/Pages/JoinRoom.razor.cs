using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace Estimator.Pages
{
    public partial class JoinRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        private bool alert = true;

        private async void JoinRoomById()
        {
            if (this.Username == string.Empty || this.RoomId == string.Empty)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Username or RoomId is empty!");
                return;
            }

            try
            {
                Data.Instances.RoomManager.JoinRoom(this.RoomId, this.Username);
                this.NavigationManager.NavigateTo($"room/{this.RoomId}/{this.Username}");
            }
            catch (RoomIdNotFoundException e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "RoomId not found!");
            }
            catch (UsernameAlreadyInUseException e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Username is already in use!");
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong!");
            }
        }
    }
}
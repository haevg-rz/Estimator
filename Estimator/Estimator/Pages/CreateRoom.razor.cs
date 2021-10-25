using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Estimator.Pages
{
    public partial class CreateRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        private async void CreateNewRoom()
        {
            if(this.Username == string.Empty)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Please enter a username!");
                return;
            }

            try
            {
                this.RoomId = Data.Instances.RoomManager.CreateRoom(1, new Data.Estimator(this.Username));
                this.NavigationManager.NavigateTo($"host/{this.RoomId}/{this.Username}");
                return;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong!\n Please try again.");
            }
        }
    }
}
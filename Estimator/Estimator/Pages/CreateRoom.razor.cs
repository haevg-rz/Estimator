using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.IIS.Core;

[assembly: InternalsVisibleTo("Estimator.Tests.Pages")]

namespace Estimator.Pages
{
    public partial class CreateRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        [Parameter] public string Type { get; set; } = "Fibonacci";
        public bool IsAsync { get; set; } = false;
        public string AsyncRoomHours { get; set; }
        public bool ShowDialog { get; set; }

        private async void CreateNewRoom()
        {
            if (this.IsUsernameEmpty())
            {
                await this.Alert("Please enter a username!");
                return;
            }

            try
            {
                this.RoomId = this.RoomManager.CreateRoom(this.ConvertType(this.Type), new Data.Model.Estimator(this.Username));
                if (this.IsAsync)
                {
                    this.OpenAsyncEstimationWindow();
                }
                else
                {
                    this.NavigateTo($"host/{this.RoomId}/{this.Username}");
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Something went wrong!\n Please try again.");
            }
        }

        internal bool IsUsernameEmpty()
        {
            return this.Username == string.Empty;
        }

        internal int ConvertType(string type)
        {
            return type == "Fibonacci" ? 1 : 2;
        }

        private async Task Alert(string alertMessage)
        {
            await this.JsRuntime.InvokeVoidAsync("alert", alertMessage);
        }

        private void NavigateTo(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }

        private void OnDeleteDialogClose(bool accepted)
        {
            this.ShowDialog = false;
            this.StateHasChanged();
        }
        private void OpenAsyncEstimationWindow()
        {
            this.ShowDialog = true;
            this.StateHasChanged();
        }
    }
}
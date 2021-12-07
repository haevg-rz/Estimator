using Estimator.Data.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Estimator.Tests.Pages")]

namespace Estimator.Pages
{
    public partial class CreateRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        [Parameter] public string Type { get; set; } = "Fibonacci";
        public bool IsAsync { get; set; } = false;
        public string AsyncRoomHours { get; set; } = "3";
        public bool ShowDialog { get; set; }
        private const string pattern = "^[0-9]+$";

        private string joinUrl { get; set; }
        private string hostUrl { get; set; }

        private async void CreateNewRoom()
        {
            if (this.IsUsernameEmpty())
            {
                await this.Alert("Please enter a username!");
                return;
            }

            if (this.RoomManager.IsSolidInput(this.Username))
            {
                await this.Alert("Username is not solid!\n Please use only A-Z, a-z and 0-9");
                return;
            }

            if (this.IsAsync && !this.AsyncRoomLifespanIsSolid(this.AsyncRoomHours))
            {
                await this.Alert("Please enter a lifespan for the async room between 1 and 24 days");
                return;
            }


            try
            {
                if (this.IsAsync)
                {
                    this.RoomId = this.RoomManager.CreateRoom(this.ConvertType(this.Type),
                        new Data.Model.Estimator(this.Username),
                        int.Parse(this.AsyncRoomHours));
                    this.OpenAsyncEstimationWindow();
                }
                else
                {
                    this.RoomId = this.RoomManager.CreateRoom(this.ConvertType(this.Type),
                        new Data.Model.Estimator(this.Username));
                    this.NavigateTo($"host/{this.RoomId}/{this.Username}");
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Something went wrong!\n Please try again.");
            }
        }

        private bool AsyncRoomLifespanIsSolid(string input)
        {
            if (Regex.IsMatch(input, pattern))
            {
                var i = int.Parse(input);
                return i > 0 && i < 25;
            }
            else
            {
                return false;
            }
        }

        internal bool IsUsernameEmpty()
        {
            return this.Username == string.Empty;
        }

        internal RoomType ConvertType(string type)
        {
            return type == "Fibonacci" ? RoomType.Fibonacci : RoomType.Tshirt;
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
            this.BuildCopyUrlForAsyncEstimationWindow();
            this.ShowDialog = true;
            this.StateHasChanged();
        }

        private async void BuildCopyUrlForAsyncEstimationWindow()
        {
            try
            {
                var uri = new Uri(this.NavigationManager.Uri);
                this.joinUrl = $"{uri.Scheme}://{uri.Authority}/joinroom/{this.RoomId}";
                this.hostUrl = $"{uri.Scheme}://{uri.Authority}/joinroom/{this.RoomId}/{this.Username}";
            }
            catch (Exception e)
            {
                await this.Alert("Copy url has failed!");
            }
        }
    }
}
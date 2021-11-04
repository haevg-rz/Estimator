using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Estimator.Data.Interface;

namespace Estimator.Pages
{
    public partial class CreateRoom
    {
        [Inject] internal IRoomManager RoomManager { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] private NavigationManager NavigationManager { get; }

        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        [Parameter] public string Type { get; set; } = "Fibonacci";


        private async void CreateNewRoom()
        {
            if (this.Username == string.Empty)
            {
                await this.Alert("Please enter a username!");
                return;
            }

            try
            {
                this.RoomId =
                    this.RoomManager.CreateRoom(this.ConvertType(this.Type),
                        new Data.Model.Estimator(this.Username));
                this.NavigateTo($"host/{this.RoomId}/{this.Username}");
                return;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Something went wrong!\n Please try again.");
            }
        }

        private int ConvertType(string type)
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
    }
}
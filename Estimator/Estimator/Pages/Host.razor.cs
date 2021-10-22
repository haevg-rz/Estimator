using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Estimator.Data.Exceptions;
using Microsoft.JSInterop;

namespace Estimator.Pages
{
    public partial class Host
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        public string Titel { get; set; } = string.Empty;
        public List<string> Estimator { get; set; } = new List<string>();
        public bool isFibonacci { get; set; } = false;
        public bool isHost { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if(Data.Instances.RoomManager.IsHost(this.Username, this.RoomId))
            {
                this.isHost = true;
                var type = Data.Instances.RoomManager.GetRoomType(this.RoomId, this.Username);
                this.isFibonacci = type.Equals(1);
            }
            else
            {
                this.isHost = false;
                await this.JsRuntime.InvokeVoidAsync("alert", "You are not the host");
            }

        }

        private async void CloseRoom()
        {
            try
            {
                Data.Instances.RoomManager.CloseRoom(this.RoomId);
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "LeaveRoom went wrong! Please try again.");
            }

            this.NavigationManager.NavigateTo($"/");
        }

        private async void StartEstimation()
        {
            try
            {
                Data.Instances.RoomManager.StartEstimation(this.RoomId, this.Titel);
            }
            catch (RoomIdNotFoundException e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "RoomId not found!");
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong! Please try again.");
            }
        }

        private async void CloseEstimation()
        {
            try
            {
                Data.Instances.RoomManager.CloseEstimation(this.RoomId);
            }
            catch (RoomIdNotFoundException e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "RoomId not found!");
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong! Please try again.");
            }
        }
    }
}
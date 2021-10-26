using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Estimator.Pages
{
    public partial class Room
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        public string Titel { get; set; } = string.Empty;
        public List<Data.Estimator> Estimators { get; set; } = new List<Data.Estimator>();
        public bool isFibonacci { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            //TODO
            var type = Data.Instances.RoomManager.GetRoomType(this.RoomId, this.Username);
            this.isFibonacci = type.Equals(1);

            var room = Data.Instances.RoomManager.GetRoomById(this.RoomId);
            this.Estimators = room.GetEstimators();
            this.Titel = room.GetTitel();

            room.StartEstimationEvent += this.SetNewTitel;
            room.UpdateEstimatorListEvent += this.UpdateEstimatorListEvent;
            room.RoomClosedEvent += this.ClosePage;
            room.NewEstimationEvent += this.UpdateEstimatorListEvent;
        }

        private void UpdateEstimatorListEvent()
        {
            this.UpdateView();
        }

        private async void ClosePage()
        {
            await this.JsRuntime.InvokeVoidAsync("alert", "The host closed this room!");
            this.NavigationManager.NavigateTo($"/joinroom");
        }

        private async void Estimate()
        {
            var estimation = "3";
            try
            {
                Data.Instances.RoomManager.EntryVote(new Data.Estimator(this.Username, estimation), this.RoomId); //TODO
            }
            catch (UsernameNotFoundException e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", e.Message);
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong!");
            }
        }

        private async void SetNewTitel(string titel)
        {
            this.Titel = titel;
            this.UpdateView();
        }

        private async void LeaveRoom()
        {
            try
            {
                Data.Instances.RoomManager.LeaveRoom(new Data.Estimator(this.Username), this.RoomId);
            }
            catch (Exception e)
            {
                Trace.WriteLine("LeaveRoom went wrong!");
            }

            this.NavigationManager.NavigateTo($"/joinroom");
        }

        private async void UpdateView()
        {
            await this.InvokeAsync(() => { this.StateHasChanged(); });
        }
    }
}
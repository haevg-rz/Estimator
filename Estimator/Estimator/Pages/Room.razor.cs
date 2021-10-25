using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Estimator.Pages
{
    public partial class Room
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        [Parameter] public string Titel { get; set; } = string.Empty;
        public bool isFibonacci { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            //TODO
            var type = Data.Instances.RoomManager.GetRoomType(this.RoomId, this.Username);
            this.isFibonacci = type.Equals(1);

            var room = Data.Instances.RoomManager.GetRoomById(this.RoomId);
            //room.StartEstimation += this.SetNewTitel;
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
                await this.JsRuntime.InvokeVoidAsync("alert", "Username is not registered!");
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong!");
            }
        }

        private void SetNewTitel(string titel)
        {
            this.Titel = titel;
            this.UriHelper.NavigateTo(this.UriHelper.Uri, forceLoad: true);
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
    }
}
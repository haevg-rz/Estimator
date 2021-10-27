using Estimator.Data.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Estimator.Pages
{
    public partial class Host
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        public string Titel { get; set; } = string.Empty;
        public string TitelTextbox { get; set; } = string.Empty;
        public List<Data.Estimator> Estimators { get; set; } = new List<Data.Estimator>();
        private bool IsFibonacci { get; set; }
        private bool IsHost { get; set; }
        public string CurrentEstimation { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;



        protected override async Task OnInitializedAsync()
        {
            if (Data.Instances.RoomManager.IsHost(this.Username, this.RoomId))
                try
                {
                    this.IsHost = true;
                    var type = Data.Instances.RoomManager.GetRoomType(this.RoomId, this.Username);
                    this.IsFibonacci = type.Equals(1);

                    var room = Data.Instances.RoomManager.GetRoomById(this.RoomId);
                    this.Estimators = room.GetEstimators();
                    room.UpdateEstimatorListEvent += this.UpdateEstimatorListEvent;
                    room.NewEstimationEvent += this.UpdateEstimatorListEvent;
                    room.UpdateEstimatorListEvent += this.UpdateView;
                    room.CloseEstimationEvent += this.SetDiagramm;

                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                    throw;
                }
            else
                this.IsHost = false;
        }

        private void SetDiagramm()
        {
            this.Result = Data.Instances.RoomManager.GetDiagramDataByRoomId(this.RoomId);
            this.UpdateView();
        }

        
        private void UpdateEstimatorListEvent()
        {
            this.UpdateView();
        }

        private async void CloseRoom()
        {
            try
            {
                var room = Data.Instances.RoomManager.GetRoomById(this.RoomId);
                room.UpdateEstimatorListEvent -= this.UpdateView;
                room.CloseEstimationEvent -= this.SetDiagramm;

                Data.Instances.RoomManager.CloseRoom(this.RoomId);
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "LeaveRoom went wrong! Please try again.");
            }

            this.NavigationManager.NavigateTo($"/createroom");
        }

        private async void StartEstimation()
        {
            try
            {
                this.Result = string.Empty;

                this.Titel = this.TitelTextbox;
                this.TitelTextbox = string.Empty;
                Data.Instances.RoomManager.StartEstimation(this.RoomId, this.Titel);
                this.UpdateView();
            }
            catch (RoomIdNotFoundException e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", e.Message);
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
                await this.JsRuntime.InvokeVoidAsync("alert", e.Message);
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong! Please try again.");
            }
        }

        private async void CopyRoomId()
        {
            try
            {
                await this.JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", this.RoomId);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.JsRuntime.InvokeVoidAsync("alert", "Copy roomId failed!");
            }
        }

        private async void CopyUrl()
        {
            try
            {
                var uri = new Uri(this.NavigationManager.Uri);
                await this.JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText",
                    $"{uri.Scheme}://{uri.Authority}/joinroom/{this.RoomId}");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.JsRuntime.InvokeVoidAsync("alert", "Copy url failed!");
            }
        }

        private async void UpdateView()
        {
            await this.InvokeAsync(() => { this.StateHasChanged(); });
        }

        private void Onchange(ChangeEventArgs args)
        {
            this.CurrentEstimation = args.Value.ToString();
        }

        private async void Estimate()
        {
            if (this.CurrentEstimation.Equals(string.Empty))
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Please choose a Card!");
                return;
            }
            try
            {
                Data.Instances.RoomManager.EntryVote(new Data.Estimator(this.Username, this.CurrentEstimation), this.RoomId);
            }
            catch (UsernameNotFoundException e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", e.Message);
            }
            catch (Exception e)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", e.Message);
            }
        }
    }
}
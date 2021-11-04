using Estimator.Data.Exceptions;
using Estimator.Data.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Estimator.Data.Interface;

[assembly: InternalsVisibleTo("Estimator.Tests.Pages")]

namespace Estimator.Pages
{
    public partial class Host : IHost
    {
        [Inject] internal IRoomManager RoomManager { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] private NavigationManager NavigationManager { get; }

        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        public string Titel { get; set; } = string.Empty;
        public string TitelTextbox { get; set; } = string.Empty;
        public List<Data.Model.Estimator> Estimators { get; set; } = new List<Data.Model.Estimator>();
        private bool IsFibonacci { get; set; }
        private bool IsHost { get; set; }
        public string CurrentEstimation { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public bool EstimationSuccessful { get; set; } = false;
        public bool EstimationClosed { get; set; } = false;

        internal List<DiagramData> diagramData = new List<DiagramData>();

        protected override async Task OnInitializedAsync()
        {
            if (this.RoomManager.IsHost(this.Username, this.RoomId))
                try
                {
                    this.IsHost = true;
                    var type = this.RoomManager.GetRoomType(this.RoomId, this.Username);
                    this.IsFibonacci = type.Equals(1);

                    var room = this.RoomManager.GetRoomById(this.RoomId);
                    this.Estimators = room.GetEstimators();
                    room.UpdateEstimatorListEvent += this.UpdateEstimatorListEvent;
                    room.NewEstimationEvent += this.UpdateEstimatorListEvent;
                    room.UpdateEstimatorListEvent += this.UpdateView;
                    room.CloseEstimationEvent += this.SetDiagram;
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                    throw;
                }
            else
                this.IsHost = false;
        }

        internal async void SetDiagram()
        {
            this.EstimationClosed = true;
            this.diagramData = this.RoomManager.GetDiagramDataByRoomId(this.RoomId);
            await this.JsRuntime.InvokeVoidAsync("GeneratePieChart", this.diagramData);
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
                var room = this.RoomManager.GetRoomById(this.RoomId);
                room.UpdateEstimatorListEvent -= this.UpdateView;
                room.CloseEstimationEvent -= this.SetDiagram;

                this.RoomManager.CloseRoom(this.RoomId);
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
                this.EstimationSuccessful = false;
                this.EstimationClosed = false;
                this.Result = string.Empty;

                this.Titel = this.TitelTextbox;
                this.RoomManager.StartEstimation(this.RoomId, this.Titel);
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


        internal async void CloseEstimation()
        {
            try
            {
                this.EstimationClosed = true;

                this.RoomManager.CloseEstimation(this.RoomId);
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
                this.RoomManager.EntryVote(new Data.Model.Estimator(this.Username, this.CurrentEstimation),
                    this.RoomId);
                this.EstimationSuccessful = true;
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
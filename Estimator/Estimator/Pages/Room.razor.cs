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
    public partial class Room : IRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        public string Titel { get; set; } = string.Empty;
        public List<Data.Model.Estimator> Estimators { get; set; } = new List<Data.Model.Estimator>();
        public bool isFibonacci { get; set; } = false;
        public bool estimationSuccessful { get; set; } = false;
        public bool estimationClosed { get; set; } = false;
        public string Result { get; set; } = string.Empty;
        public string CurrentEstimation { get; set; } = string.Empty;

        private List<DiagramData> diagramData = new List<DiagramData>();

        protected override async Task OnInitializedAsync()
        {
            var type = this.RoomManager.GetRoomType(this.RoomId, this.Username);
            this.isFibonacci = type.Equals(1);

            var room = this.RoomManager.GetRoomById(this.RoomId);
            this.Estimators = room.GetEstimators();
            this.Titel = room.GetTitel();

            room.StartEstimationEvent += this.SetNewTitel;
            room.UpdateEstimatorListEvent += this.UpdateView;
            room.RoomClosedEvent += this.ClosePage;
            room.NewEstimationEvent += this.UpdateView;
            room.CloseEstimationEvent += this.SetDiagramm;
        }

        private async void ClosePage()
        {
            var room = this.RoomManager.GetRoomById(this.RoomId);
            room.StartEstimationEvent -= this.SetNewTitel;
            room.UpdateEstimatorListEvent -= this.UpdateView;
            room.RoomClosedEvent -= this.ClosePage;
            room.CloseEstimationEvent -= this.SetDiagramm;

            await this.Alert("The host closed this room!");
            this.NavigateTo($"/joinroom");
        }

        private async void SetDiagramm()
        {
            this.estimationClosed = true;
            this.diagramData = this.RoomManager.GetDiagramDataByRoomId(this.RoomId);
            await this.GeneratePieChart();
            this.UpdateView();
        }

        private async void Estimate()
        {
            if (this.CurrentEstimation.Equals(string.Empty))
            {
                await this.Alert("Please choose a Card!");
                return;
            }

            try
            {
                this.RoomManager.EntryVote(new Data.Model.Estimator(this.Username, this.CurrentEstimation),
                    this.RoomId);
                this.estimationSuccessful = true;
            }
            catch (UsernameNotFoundException e)
            {
                await this.Alert(e.Message);
            }
            catch (Exception e)
            {
                await this.Alert("Something went wrong!");
            }
        }

        public async void SetNewTitel(string titel)
        {
            this.estimationSuccessful = false;
            this.estimationClosed = false;

            this.Result = string.Empty;
            this.Titel = titel;
            this.UpdateView();
        }

        private async void LeaveRoom()
        {
            try
            {
                this.RoomManager.LeaveRoom(new Data.Model.Estimator(this.Username), this.RoomId);

                var room = this.RoomManager.GetRoomById(this.RoomId);
                room.StartEstimationEvent -= this.SetNewTitel;
                room.UpdateEstimatorListEvent -= this.UpdateView;
                room.RoomClosedEvent -= this.ClosePage;
                room.CloseEstimationEvent -= this.SetDiagramm;
            }
            catch (Exception e)
            {
                Trace.WriteLine("LeaveRoom went wrong!");
            }

            this.NavigateTo("/joinroom");
        }

        public async void UpdateView()
        {
            await this.InvokeAsync(() => { this.StateHasChanged(); });
        }

        private void Onchange(ChangeEventArgs args)
        {
            this.CurrentEstimation = args.Value.ToString();
        }

        private async Task Alert(string alertMessage)
        {
            await this.JsRuntime.InvokeVoidAsync("alert", alertMessage);
        }

        private void NavigateTo(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }

        private async Task GeneratePieChart()
        {
            await this.JsRuntime.InvokeVoidAsync("GeneratePieChart", this.diagramData);
        }
    }
}
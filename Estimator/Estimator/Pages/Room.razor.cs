﻿using Estimator.Data.Exceptions;
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

        private List<DiagramValue> diagramValues = new List<DiagramValue>(){new DiagramValue("0","1")};

        protected override async Task OnInitializedAsync()
        {
            var type = this.RoomManager.GetRoomType(this.RoomId, this.Username);
            this.isFibonacci = type.Equals(1);

            var room = this.RoomManager.GetRoomById(this.RoomId);
            this.Estimators = room.GetEstimators();
            this.Titel = room.GetTitel();

            await this.GenerateBarDiagram();
            await this.GeneratePieDiagram();

            room.StartEstimationEvent += this.SetNewTitel;
            room.UpdateEstimatorListEvent += this.UpdateView;
            room.RoomClosedEvent += this.ClosePage;
            room.NewEstimationEvent += this.UpdateView;
            room.CloseEstimationEvent += this.SetDiagram;
        }

        private async void ClosePage()
        {
            var room = this.RoomManager.GetRoomById(this.RoomId);
            room.StartEstimationEvent -= this.SetNewTitel;
            room.UpdateEstimatorListEvent -= this.UpdateView;
            room.RoomClosedEvent -= this.ClosePage;
            room.CloseEstimationEvent -= this.SetDiagram;

            await this.Alert("The host closed this room!");
            this.NavMenueManager.Show();
            this.NavigateTo($"/joinroom");
        }

        private async void SetDiagram()
        {
            this.estimationClosed = true;
            this.diagramValues = this.RoomManager.GetDiagramDataByRoomId(this.RoomId);

            if (this.isPieDiagram)
            {
                await this.GenerateBarDiagram();
                await this.GeneratePieDiagram();
            }
            else
            {
                await this.GeneratePieDiagram();
                await this.GenerateBarDiagram();
            }

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
            catch (Exception)
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
                room.CloseEstimationEvent -= this.SetDiagram;
            }
            catch (Exception)
            {
                Trace.WriteLine("LeaveRoom went wrong!");
            }

            this.NavMenueManager.Show();
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

        public async Task GeneratePieDiagram()
        {
            var (category, count) = this.ConvertDiagramValuesToArray();
            await this.JsRuntime.InvokeVoidAsync("GeneratePieChart", category, count);

        }

        public async Task GenerateBarDiagram()
        {
            var (category, count) = this.ConvertDiagramValuesToArray();
            await this.JsRuntime.InvokeVoidAsync("GenerateBarChart", category, count);
        }

        private (string[] category, string[] count) ConvertDiagramValuesToArray()
        {
            var category = new List<string>();
            var count = new List<string>();

            foreach (var diagramValue in this.diagramValues)
            {
                category.Add(diagramValue.EstimationCategory);
                count.Add(diagramValue.EstimationCount);
            }

            return (category.ToArray(), count.ToArray());
        }

        private bool isPieDiagram = true;
        private string diagramType => this.isPieDiagram ? "bar" : "pie";
        private async void SwitchDiagramType()
        {
            this.isPieDiagram = !this.isPieDiagram;

            if (this.isPieDiagram)
            {
                await this.GenerateBarDiagram();
                await this.GeneratePieDiagram();
            }
            else
            {
                await this.GeneratePieDiagram();
                await this.GenerateBarDiagram();
            }
        }
    }
}
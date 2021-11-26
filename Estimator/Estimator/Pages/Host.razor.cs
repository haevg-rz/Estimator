﻿using Estimator.Data.Exceptions;
using Estimator.Data.Interface;
using Estimator.Data.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Estimator.Tests.Pages")]

namespace Estimator.Pages
{
    public partial class Host : IHost
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string TitelTextbox { get; set; } = string.Empty;
        public List<Data.Model.Estimator> Estimators { get; set; } = new List<Data.Model.Estimator>();
        private string QRCodeString { get; set; }

        private bool IsFibonacci { get; set; }
        private bool IsHost { get; set; }
        private bool showQRCode { get; set; }

        public string CurrentEstimation { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;

        public bool EstimationSuccessful { get; set; }
        public bool EstimationClosed { get; set; }
        public bool AsyncEstimation { get; set; }

        public List<DiagramValue> DiagramValues { get; set; } = new List<DiagramValue>();

        protected override async Task OnInitializedAsync()
        {
            if (this.RoomManager.IsHost(this.Username, this.RoomId))
                try
                {
                    this.IsHost = true;
                    var type = this.RoomManager.GetRoomType(this.RoomId, this.Username);
                    this.AsyncEstimation = this.RoomManager.IsRoomAsync(this.RoomId);
                    this.IsFibonacci = type.Equals(1);

                    var room = this.RoomManager.GetRoomById(this.RoomId);
                    this.Estimators = room.GetEstimators();

                    this.SetupEvents(room);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                    throw;
                }
            else
                this.IsHost = false;
        }

        private void SetupEvents(Data.Room room)
        {
            room.UpdateEstimatorListEvent += this.UpdateEstimatorListEvent;
            room.NewEstimationEvent += this.UpdateEstimatorListEvent;
            room.UpdateEstimatorListEvent += this.UpdateView;
            room.CloseEstimationEvent += this.SetDiagram;
        }

        public async void SetDiagram()
        {
            this.EstimationClosed = true;
            this.DiagramValues = this.RoomManager.GetDiagramDataByRoomId(this.RoomId);
            await this.GenerateDiagram();
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
            catch (Exception)
            {
                await this.Alert("Close Room went wrong! Please try again.");
            }

            this.NavMenueManager.Show();
            this.NavigateTo($"/createroom");
        }

        private async void LeaveRoom()
        {
            try
            {
                var room = this.RoomManager.GetRoomById(this.RoomId);
                room.UpdateEstimatorListEvent -= this.UpdateView;
                room.CloseEstimationEvent -= this.SetDiagram;

            }
            catch (Exception)
            {
                Trace.WriteLine("LeaveRoom went wrong!");
            }

            this.NavMenueManager.Show();
            this.NavigateTo($"/createroom");

        }

        private async void StartEstimation()
        {
            if (this.RoomManager.IsSolidInput(this.TitelTextbox) && !this.TitelTextbox.Equals(string.Empty))
            {
                await this.Alert("Title is not solid!\n Please use only A-Z, a-z and 0-9");
                return;
            }
            try
            {
                this.EstimationSuccessful = false;
                this.EstimationClosed = false;
                this.Result = string.Empty;

                this.Title = this.TitelTextbox;
                this.RoomManager.StartEstimation(this.RoomId, this.Title);
                this.UpdateView();
            }
            catch (RoomIdNotFoundException e)
            {
                await this.Alert(e.Message);
            }
            catch (Exception)
            {
                await this.Alert("Something went wrong! Please try again.");
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
                await this.Alert(e.Message);
            }
            catch (Exception)
            {
                await this.Alert("Something went wrong! Please try again.");
            }
        }

        private async void CopyRoomId()
        {
            try
            {
                await this.CopyToClipboard(this.RoomId);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Copy roomId failed!");
            }
        }

        private async void CopyUrl()
        {
            try
            {
                var uri = new Uri(this.NavigationManager.Uri);
                await this.CopyToClipboard($"{uri.Scheme}://{uri.Authority}/joinroom/{this.RoomId}");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Copy url failed!");
            }
        }

        private async void CopyHostUrl()
        {
            try
            {
                var uri = new Uri(this.NavigationManager.Uri);
                await this.CopyToClipboard($"{uri.Scheme}://{uri.Authority}/joinroom/{this.RoomId}/{this.Username}"); 
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Copy url for Host failed!");
            }

        }

        public async void UpdateView()
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
                await this.Alert("Please choose a Card!");
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
                await this.Alert(e.Message);
            }
            catch (Exception e)
            {
                await this.Alert(e.Message);
            }
        }

        public async Task Alert(string alertMessage)
        {
            await this.JsRuntime.InvokeVoidAsync("alert", alertMessage);
        }

        public void NavigateTo(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }

        public async Task CopyToClipboard(string content)
        {
            await this.JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", content);
        }

        public async Task GenerateDiagram()
        {
            var (category, count) = this.ConvertDiagramValuesToArray();
            await this.JsRuntime.InvokeVoidAsync("GenerateChart",this.diagramType, category, count);
        }

        public void OpenQRCode()
        {
            using (var ms = new MemoryStream())
            {
                var uri = new Uri(this.NavigationManager.Uri);
                var generator = new PayloadGenerator.Url($"{uri.Scheme}://{uri.Authority}/joinroom/{this.RoomId}");
                var payload = generator.ToString();

                var qrGenerator = new QRCoder.QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(payload, QRCoder.QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                using (var oBitmap = qrCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, ImageFormat.Png);
                    this.QRCodeString = "data:image/png;base64, " + Convert.ToBase64String(ms.ToArray());
                }
            }

            this.showQRCode = true;
            this.StateHasChanged();
        }
        private void OnDeleteDialogClose(bool accepted)
        {
            this.showQRCode = false;
            this.StateHasChanged();
        }

        private (string[] category,string[] count) ConvertDiagramValuesToArray()
        {
            var category = new List<string>();
            var count = new List<string>();

            foreach (var diagramValue in this.DiagramValues)
            {
                category.Add(diagramValue.EstimationCategory);
                count.Add(diagramValue.EstimationCount);
            }

            return (category.ToArray(), count.ToArray());
        }

        private bool isPieDiagram = true;
        private string diagramType => this.isPieDiagram ? "pie" : "bar";
        private async void SwitchDiagramType()
        {
            this.isPieDiagram = !this.isPieDiagram;
            await this.GenerateDiagram();
        }

    }
}
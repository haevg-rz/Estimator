using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Estimator.Data.Interface;
using Microsoft.JSInterop;

namespace Estimator.Components
{
    public partial class AsyncEstimationWindow
    {
        [Parameter] public string RoomId { get; set; }
        [Parameter] public string HostName { get; set; }
        [Parameter] public EventCallback<bool> OnClose { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IMainLayout MainLayout { get; set; }

        private async void OpenHostPage()
        {
            try
            {
                this.MainLayout.HideNavMenue();
                this.NavigateTo($"host/{this.RoomId}/{this.HostName}");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                 await this.Alert("Something went wrong!");
            }
        }
        

        private Task ClosePage()
        {
            return this.OnClose.InvokeAsync(false);
        }

        private Task ModalOk()
        {
            return this.OnClose.InvokeAsync(true);
        }

        private async void CopyRoomId()
        {
            await this.CopyToClipboard(this.RoomId);
        }


        private async void CopyHostname()
        {
            await this.CopyToClipboard(this.HostName);
        }

        public async Task CopyToClipboard(string content)
        {
            await this.JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", content);
        }

        private void NavigateTo(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }
        private async Task Alert(string alertMessage)
        {
            await this.JsRuntime.InvokeVoidAsync("alert", alertMessage);
        }
    }
}

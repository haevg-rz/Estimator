using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Estimator.Data.Interface;
using Estimator.Shared;
using Microsoft.JSInterop;

namespace Estimator.Components
{
    public partial class AsyncEstimationWindow
    {
        [Parameter] public string RoomId { get; set; }
        [Parameter] public string HostName { get; set; }
        [Parameter] public string JoinUri { get; set; }
        [Parameter] public string HostUri { get; set; }
        [Parameter] public EventCallback<bool> OnClose { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private async void OpenHostPage()
        {
            try
            {
                this.UpdateView();
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

        private async void CopyjoinUri()
        {
            await this.CopyToClipboard(this.JoinUri);
        }


        private async void CopyHostUri()
        {
            await this.CopyToClipboard(this.HostUri);
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

        public async void UpdateView()
        {
            await this.InvokeAsync(() => { this.StateHasChanged(); });
        }

    }
}

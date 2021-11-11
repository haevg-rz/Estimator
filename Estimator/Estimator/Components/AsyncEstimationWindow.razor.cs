using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Estimator.Components
{
    public partial class AsyncEstimationWindow
    {
        [Parameter] public string RoomId { get; set; }
        [Parameter] public string HostName { get; set; }
        [Parameter] public EventCallback<bool> OnClose { get; set; }
        public JSRuntime JsRuntime { get; set; }

        private Task ModalCancel()
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

        private void CopyEstimatorUrl()
        {
            //var Uri = new Uri(this.NavigationManager.Uri);
            //await host.CopyToClipboard($"{uri.Scheme}://{uri.Authority}/joinroom/{this.RoomId}");
        }

        private void CopyHostname()
        {
            //await host.CopyToClipboard(this.HostName);
        }

        private void CopyHostUrl()
        {
            //var Uri = new Uri(this.NavigationManager.Uri);
            //await host.CopyToClipboard($"{uri.Scheme}://{uri.Authority}//{this.hostname}");
        }
        public async Task CopyToClipboard(string content)
        {
            await this.JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", content);
        }
    }
}

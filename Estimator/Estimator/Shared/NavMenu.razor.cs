using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Estimator.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;
        private string NavMenuCssClass => this.collapseNavMenu ? "collapse" : null;
        [Inject] public NavigationManager NavigationManager { get; set; }

        private void ToggleNavMenu()
        {
            this.collapseNavMenu = !this.collapseNavMenu;
        }

        private async Task OnClick()
        {
            try
            {
                var uri = new Uri(this.NavigationManager.Uri);

                var isRoom = uri.Segments.SingleOrDefault(s => s.Equals("room/"));
                var isHost = uri.Segments.SingleOrDefault(s => s.Equals("host/"));

                if (!Equals(isRoom, null) || !Equals(isHost, null))
                {
                    await this.Alert($"You can not navigate from {uri.AbsoluteUri} right now.\n Please leave or close the room");
                    this.NavigationManager.NavigateTo(uri.AbsoluteUri);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.Alert("Something went wrong. Please try again.");
            }
        }

        [Inject] private IJSRuntime JsRuntime { get; set; }

        public async Task Alert(string alertMessage)
        {
            await this.JsRuntime.InvokeVoidAsync("alert", alertMessage);
        }
    }
}
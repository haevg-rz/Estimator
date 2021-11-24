using Estimator.Data.Interface;

namespace Estimator.Shared
{
    public partial class NavMenu : INavMenu
    {
        public bool collapseNavMenu { get; set; } = false;
        private string NavMenuCssClass => this.collapseNavMenu ? "collapse" : null;

        public void ToggleNavMenu()
        {
            //this.collapseNavMenu = !this.collapseNavMenu;
        }

        public async void ShowNavMenue()
        {
            this.collapseNavMenu = true;
            await this.InvokeAsync(() => { this.StateHasChanged(); });
        }

        public async void HideNavMenue()
        {
            this.collapseNavMenu = false;
            await this.InvokeAsync(() => { this.StateHasChanged(); });
        }
    }
}
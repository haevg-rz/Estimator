using Estimator.Data.Interface;
using System.Threading.Tasks;

namespace Estimator.Shared
{
    public partial class MainLayout : IMainLayout
    {
        private bool CollapseNavMenu { get; set; }
        private string NavMenueCssClass => this.CollapseNavMenu ? "collapse" : null;

        public async Task ShowNavMenue()
        {
            this.CollapseNavMenu = true;
            //await this.InvokeAsync(() => { this.StateHasChanged(); });
        }

        public async Task HideNavMenue()
        {
            this.CollapseNavMenu = false;
            //await this.InvokeAsync(() => { this.StateHasChanged(); });
        }
    }
}
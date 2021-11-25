using System;
using System.Runtime.CompilerServices;
using Estimator.Data.Interface;

namespace Estimator.Shared
{
    public partial class MainLayout
    {
        private bool CollapseNavMenu { get; set; }
        private string NavMenueCssClass => this.CollapseNavMenu ? "collapse" : null;

        protected override void OnInitialized()
        {
            this.NavMenueManager.OnChange += this.UpdateMenu;

            base.OnInitialized();
        }

        private void UpdateMenu()
        {
            this.CollapseNavMenu = this.NavMenueManager.CollapseNavMenu;
            this.StateHasChanged();
        }
    }
}
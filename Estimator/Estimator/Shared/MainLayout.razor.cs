using Estimator.Data.Interface;

namespace Estimator.Shared
{
    public partial class MainLayout : IMainLayout
    {
        private static bool CollapseNavMenu { get; set; }
        private static string NavMenueCssClass => CollapseNavMenu ? "collapse" : null;

        public void ShowNavMenue()
        {
            CollapseNavMenu = false;
        }

        public void HideNavMenue()
        {
            CollapseNavMenu = true;
        }
    }
}
namespace Estimator.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;
        private string NavMenuCssClass => this.collapseNavMenu ? "collapse" : null;

        public delegate void Notify();

        public event Notify NavMenuButtonClockedEvent;

        private void ToggleNavMenu()
        {
            this.collapseNavMenu = !this.collapseNavMenu;
        }
        
        private void OnClick()
        {
            this.NavMenuButtonClockedEvent?.Invoke();
        }
    }
}
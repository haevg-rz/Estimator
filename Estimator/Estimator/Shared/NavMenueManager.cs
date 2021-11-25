using System;

namespace Estimator.Shared
{
    public class NavMenueManager
    {
        public bool CollapseNavMenu { get; set; }

        public void Show()
        {
            this.CollapseNavMenu = false;
            this.NotifyStateChanged();
        }

        public void Hide()
        {
            this.CollapseNavMenu = true;
            this.NotifyStateChanged();
        }


        public event Action OnChange;

        private void NotifyStateChanged()
        {
            this.OnChange?.Invoke();
        }
    }
}
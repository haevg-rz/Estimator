using System;
using System.Linq;
using System.Threading.Tasks;

namespace Estimator.Shared
{
    public partial class MainLayout
    {
        public bool ShowDialog { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            this.NavMenu.NavMenuButtonClockedEvent += this.CheckNavigation;
        }


        public void CheckNavigation()
        {
            var uri = new Uri(this.NavigationManager.Uri);
            try
            {
                var a = uri.Segments.Single(s => s.Equals("room/"));
                if (uri.Segments[1].Equals("room/") || uri.Segments[1].Equals("host/"))
                {
                    this.Message = "hello my friend";
                    this.ShowDialog = true;
                }
            }
            catch (Exception)
            {

            }

        }

        public void OnWindowClose(bool result)
        {
            this.ShowDialog = false;

            if (!result)
                this.NavigationManager.NavigateTo(this.NavigationManager.Uri);
        }
    }
}

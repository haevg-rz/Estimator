using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class JoinGroup
    {
        [Parameter] public string Username { get; set; } = "Tobi";

        [Parameter] public string RoomId { get; set; } = "123456";

        private bool show = true;

        public async void SwitchPage()
        {
            if (this.show)
            {
                this.show = false;
            }
            else
            {
                this.show = true;
            }
            return;
        }
    }
}
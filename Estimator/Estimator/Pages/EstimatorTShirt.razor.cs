using System;
using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class EstimatorTShirt
    {
        [Parameter] public string RoomId { get; set; } = String.Empty;
        [Parameter] public string Username { get; set; } = String.Empty;
        [Parameter] public bool UserSubscribed { get; set; }

        public EstimatorTShirt()
        {
            this.UserSubscribed = this.Username == String.Empty;
        }

        private void Login()
        {
            this.UserSubscribed = true;
        }
    }
}

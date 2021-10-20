using Microsoft.AspNetCore.Components;
using System;

namespace Estimator.Pages
{
    public partial class EstimatorFibonacci
    {
        [Parameter] public string RoomId { get; set; } = String.Empty;
        [Parameter] public string Username { get; set; } = String.Empty;
        [Parameter] public bool UserSubscribed { get; set; }

        public EstimatorFibonacci()
        {
            this.UserSubscribed = this.Username == String.Empty;
        }

        private void Login()
        {
            this.UserSubscribed = true;
        }
    }
}

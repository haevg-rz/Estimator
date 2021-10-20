using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class JoinGroup
    {
        [Parameter] public string RoomId { get; set; }
        [Parameter] public string Username { get; set; }

        public async void OpenPage()
        {
            //If(type== fibonacci) //TODO
            if (this.Username != null && this.RoomId != null)
            {
                this.NavigationManager.NavigateTo($"Fibonacci/{this.RoomId}/{this.Username}");
            }
        }
    }
}
using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class JoinGroup
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;

        public async void OpenPage()
        {
            //If(type== fibonacci) //TODO
            if (this.Username != string.Empty && this.RoomId != string.Empty)
                this.NavigationManager.NavigateTo($"Fibonacci/{this.RoomId}/{this.Username}");
        }
    }
}
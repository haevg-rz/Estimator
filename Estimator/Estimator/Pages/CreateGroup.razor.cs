using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class CreateGroup
    {
        [Parameter] public string RoomId { get; set; }
        [Parameter] public string Username { get; set; }

        public async void OpenPage()
        {
            //If(type== fibonacci) //TODO
            this.NavigationManager.NavigateTo($"host/{this.RoomId}/{this.Username}");
        }
    }
}
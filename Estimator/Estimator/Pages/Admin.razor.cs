using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Estimator.Pages
{
    public partial class Admin
    {
        public List<Data.Model.Estimator> Estimators { get; set; }
        public List<Data.Room> Rooms { get; set; } = new List<Data.Room>();
        public string Password { get; set; }
        private bool isAdmin = false;

        private async void ShowRooms()
        {
            this.Rooms = await this.RoomManager.GetListOfRooms(this.Password);
            if (this.Rooms == null)
            {
                await this.Alert("You are not the host!");
                return;
            }
            else
            {
                this.isAdmin = true;
                this.UpdateView();
                return;
            }
        }

        public async Task Alert(string alertMessage)
        {
            await this.JsRuntime.InvokeVoidAsync("alert", alertMessage);
        }
        public async void UpdateView()
        {
            await this.InvokeAsync(() => { this.StateHasChanged(); });
        }
    }
}

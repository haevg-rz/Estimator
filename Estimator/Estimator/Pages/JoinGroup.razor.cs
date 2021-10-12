using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class JoinGroup
    {
        public string Username { get; set; } = "Tobi";

        public string RoomId { get; set; } = "123456";

        private bool joinRoom = true;
        private bool fibonacciRoom = false;
        private bool tshirtRoom = false;

        public async void OpenJoinRoom()
        {
            this.joinRoom = true;
            this.fibonacciRoom = false;
            this.tshirtRoom = false;
            return;
        }
        public async void OpenFibonacciRoom()
        {
            this.joinRoom = false;
            this.fibonacciRoom = true;
            this.tshirtRoom = false;
            return;
        }
        public async void OpenTshirtRoom()
        {
            this.joinRoom = false;
            this.fibonacciRoom = false;
            this.tshirtRoom = true;
            return;
        }

    }
}
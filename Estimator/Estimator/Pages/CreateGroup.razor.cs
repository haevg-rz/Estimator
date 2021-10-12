namespace Estimator.Pages
{
    public partial class CreateGroup
    {
        private string Username { get; set; } = "TobiJetztAdmin";
        private string RoomId { get; set; } = "GHA5W787AG4A";

        private bool CreateRoom = true;
        private bool FibonacciRoom = false;
        private bool TshirtRoom = false;

        private async void OpenCreateRoom()
        {
            this.CreateRoom = true;
            this.FibonacciRoom = false;
            this.TshirtRoom = false;
        }
        private async void OpenFibonacciRoom()
        {
            this.CreateRoom = false;
            this.FibonacciRoom = true;
            this.TshirtRoom = false;
        }
        private async void OpenTshirtRoom()
        {
            this.CreateRoom = false;
            this.FibonacciRoom = false;
            this.TshirtRoom = true;
        }

    }
}

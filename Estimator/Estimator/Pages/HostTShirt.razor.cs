using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Estimator.Pages
{
    public partial class HostTShirt
    {
        [Parameter] public string RoomId { get; set; }
        [Parameter] public string Username { get; set; }

    }
}

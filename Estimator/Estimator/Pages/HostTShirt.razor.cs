using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Estimator.Pages
{
    public partial class HostTShirt
    {
        [Parameter] public string RoomId { get; set; } = String.Empty;
        [Parameter] public string Username { get; set; } = String.Empty;
        [Parameter] public List<string> Estimator { get; set; } = new List<string> { "tobi", "leo", "robb", "felix" };

    }
}

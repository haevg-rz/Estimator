using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class HostFibonacci
    {
        [Parameter] public string RoomId { get; set; }
        [Parameter] public string Username { get; set; }
        [Parameter] public List<string> Estimator { get; set; } = new List<string>{"tobi", "leo", "robb", "felix"};
    }
}

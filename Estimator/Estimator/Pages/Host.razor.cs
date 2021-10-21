using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Estimator.Pages
{
    public partial class Host
    {
        [Parameter] public string RoomId { get; set; } = String.Empty;
        [Parameter] public string Username { get; set; } = String.Empty;
        public List<string> Estimator { get; set; } = new List<string> { "tobi", "leo", "robb", "felix" };
        public bool isFibonacci { get; set; } = false;
    }
}

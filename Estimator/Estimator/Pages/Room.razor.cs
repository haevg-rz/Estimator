using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Estimator.Pages
{
    public partial class Room
    {
        [Parameter] public string RoomId { get; set; } = String.Empty;
        [Parameter] public string Username { get; set; } = String.Empty;
        public bool isFibonacci { get; set; } = false;

        public Room()
        {

        }

    }
}

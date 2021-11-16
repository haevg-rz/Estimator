using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Estimator.Components
{
    public partial class QRCodeWindow
    {
        [Parameter] public string QRCodeString { get; set; }
        [Parameter] public EventCallback<bool> OnClose { get; set; }

        private Task ClosePage()
        {
            return this.OnClose.InvokeAsync(false);
        }
    }
}
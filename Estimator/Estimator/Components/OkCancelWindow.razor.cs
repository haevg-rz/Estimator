using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Estimator.Components
{
    public partial class OkCancelWindow
    {
        [Parameter] public string Message { get; set; }
        [Parameter] public EventCallback<bool> OnClose { get; set; }

        private Task ClosePage()
        {
            return this.OnClose.InvokeAsync(false);
        }

        private Task CloseOk()
        {
            return this.OnClose.InvokeAsync(true);
        }
        private Task CloseCancel()
        {
            return this.OnClose.InvokeAsync(false);
        }
    }
}

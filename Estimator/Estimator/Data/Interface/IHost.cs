using Microsoft.JSInterop;

namespace Estimator.Data.Interface
{
    public interface IHost
    {
        IJSRuntime JsRuntime { get; set; }
    }
}

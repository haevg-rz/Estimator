using System.Threading.Tasks;

namespace Estimator.Data.Interface
{
    public interface IHost
    {
        Task Alert(string alertMessage);
        void NavigateTo(string path);
        Task CopyToClipboard(string content);
        Task GeneratePieChart();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Estimator.Data.Model;

namespace Estimator.Data.Interface
{
    public interface IHost
    {
        List<DiagramData> DiagramData { get; set; }
        bool EstimationClosed { get; set; }

        IRoomManager RoomManager { get; set; }
        Task Alert(string alertMessage);
        void NavigateTo(string path);
        Task CopyToClipboard(string content);
        Task GeneratePieChart();
        void SetDiagram();
        void UpdateView();
    }
}
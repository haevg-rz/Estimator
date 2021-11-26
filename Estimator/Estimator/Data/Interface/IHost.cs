using System.Collections.Generic;
using System.Threading.Tasks;
using Estimator.Data.Model;

namespace Estimator.Data.Interface
{
    public interface IHost
    {
        List<DiagramValue> DiagramValues { get; set; }
        Task Alert(string alertMessage);
        void NavigateTo(string path);
        Task CopyToClipboard(string content);
        Task GenerateDiagram();
        void SetDiagram();
        void UpdateView();
    }
}
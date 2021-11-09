﻿namespace Estimator.Data.Interface
{
    public interface IRoom
    {
        bool estimationSuccessful { get; set; }
        bool estimationClosed { get; set; }
        string Titel { get; set; }
        string Result { get; set; }
        void SetNewTitel(string titel);
        void UpdateView();
    }
}

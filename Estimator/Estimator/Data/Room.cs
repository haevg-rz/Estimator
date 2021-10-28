using Estimator.Data.Exceptions;
using Estimator.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Estimator.Tests")]
namespace Estimator.Data
{
    public class Room
    {
        private TimeManager TimeManager { get; } = new TimeManager();

        #region fields

        private readonly List<string> fibonacciNumbers = new List<string>()
            {"0", "0.5", "1", "2", "3", "5", "8", "13", "21", "34", "infinite", "coffee"};

        private readonly List<string> tshirtSizes = new List<string>()
            {"XS", "S", "M", "L", "XL", "XXL", "infinite", "coffee"};

        private string RoomID;
        internal string titel = string.Empty;
        private int type; // 1 = fibonacinumbers 2 = Tshirt Sizes
        private Estimator host;

        internal List<Estimator> estimators = new List<Estimator>();
        private List<DiagramData> diagramDataList;

        #endregion

        #region public

        public Room(string roomId, Estimator host, int type)
        {
            this.RoomID = roomId;
            this.host = host;
            this.type = type;
            this.AddEstimator(host);

            this.TimeManager.SetRoomTimer(roomId);

        }

        public void AddEstimator(Estimator estimator)
        {
            if (this.estimators.Any(e => e.Name.Equals(estimator.Name)))
                throw new UsernameAlreadyInUseException();

            this.estimators.Add(estimator);
            this.UpdateEstimatorListEvent?.Invoke();
        }

        public void RemoveEstimator(Estimator estimator)
        {
            try
            {
                this.estimators.Remove(this.estimators.Single(e => e.Name.Equals(estimator.Name)));
                this.UpdateEstimatorListEvent?.Invoke();
                return;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new UsernameNotFoundException();
            }
        }

        public void SetEstimation(Estimator estimator)
        {
            try
            {
                this.estimators.Single(e => e.Name.Equals(estimator.Name)).Estimation = estimator.Estimation;
                this.NewEstimationEvent?.Invoke();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new UsernameNotFoundException();
            }
        }

        public void ResetAllEstimates()
        {
            foreach (var e in this.estimators)
                e.Estimation = string.Empty;

            this.NewEstimationEvent?.Invoke();
        }

        public bool IsEstimatorRegistered(string estimatorName)
        {
            return this.estimators.Any(e => e.Name == estimatorName);
        }

        public string GetTitel()
        {
            return this.titel;
        }

        public void SetTitel(string titel)
        {
            this.titel = titel;
            this.StartEstimationEvent?.Invoke(titel);
        }

        public int GetRoomType()
        {
            return this.type;
        }

        public string GetRoomID()
        {
            return this.RoomID;
        }

        public void CloseEstimation()
        {
            this.CloseEstimationEvent?.Invoke();
        }
        public bool IsHost(string name)
        {
            return this.host.Name == name;
        }

        public void SetDiagramList()
        {
            var diagramData = new List<DiagramData>();

            var voteList = this.GetVoteList(this.type);
            foreach (var estimateCategory in voteList)
            {
                var estimationCount = this.estimators.Count(voter => estimateCategory == voter.Estimation);
                if (estimationCount != 0)
                    diagramData.Add(new DiagramData(estimateCategory, estimationCount.ToString()));

            }

            this.diagramDataList = diagramData;

        }

        public List<DiagramData> GetDiagramList()
        {
            return this.diagramDataList;
        }

        public void CloseClients()
        {
            this.RoomClosedEvent?.Invoke();
        }

        public List<Estimator> GetEstimators()
        {
            return this.estimators;
        }

        #endregion

        #region private

        private List<string> GetVoteList(int type)
        {
            return type == 1 ? this.fibonacciNumbers : this.tshirtSizes;
        }

        #endregion

        #region Events

        public delegate void NotifyString(string s);

        public delegate void Notify();

        public event Notify CloseEstimationEvent;
        public event Notify UpdateEstimatorListEvent;
        public event Notify RoomClosedEvent;
        public event Notify NewEstimationEvent;

        public event NotifyString StartEstimationEvent;

        #endregion


        public string GetResult() //TODO Delete PlatzhalterMethod
        {
            var result = string.Empty;
            foreach (var e in this.estimators)
            {
                result += e.Estimation;
                result += " ";
            }

            return result;
        }
    }
}
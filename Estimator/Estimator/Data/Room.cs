using Estimator.Data.Exceptions;
using Estimator.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Estimator.Shared;

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

        internal string RoomID;
        internal string titel = string.Empty;
        internal int type; // 1 = fibonacinumbers 2 = Tshirt Sizes
        private Model.Estimator host;
        private bool isAsync;

        internal List<Model.Estimator> estimators = new List<Model.Estimator>();
        internal List<DiagramValue> diagramDataList;

        #endregion

        #region public

        public Room(string roomId, Model.Estimator host, int type)
        {
            try
            {
                this.RoomID = roomId;
                this.host = host;
                this.type = type;
                this.AddEstimator(host);
                this.isAsync = false;

                this.TimeManager.SetRoomTimer(roomId, 1);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
            
        }

        public Room(string roomId, Model.Estimator host, int type, int daysUntilResolution)
        {
            try
            {
                this.RoomID = roomId;
                this.host = host;
                this.type = type;
                this.AddEstimator(host);
                this.isAsync = true;

                this.TimeManager.SetRoomTimer(roomId, daysUntilResolution);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }

        }

        public void AddEstimator(Model.Estimator estimator)
        {
            if (this.estimators.Any(e => e.Name.Equals(estimator.Name)))
                throw new UsernameAlreadyInUseException();

            this.estimators.Add(estimator);
            this.UpdateEstimatorListEvent?.Invoke();
        }

        public void RemoveEstimator(Model.Estimator estimator)
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

        public void SetEstimation(Model.Estimator estimator)
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

        public bool IsEstimated(string estimatorName)
        {
            try
            {
                return this.estimators.Single(e => e.Name.Equals(estimatorName)).Estimation != String.Empty;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new UsernameNotFoundException();
            }
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
            return this.host.Name.Equals(name);
        }

        public void SetDiagramList()
        {
            var diagramData = new List<DiagramValue>();

            var voteList = this.GetVoteList(this.type);
            foreach (var estimateCategory in voteList)
            {
                var estimationCount = this.estimators.Count(voter => estimateCategory == voter.Estimation);
                if (estimationCount != 0)
                    diagramData.Add(new DiagramValue(estimateCategory, estimationCount.ToString()));
            }

            this.diagramDataList = diagramData;
        }

        public List<DiagramValue> GetDiagramList()
        {
            return this.diagramDataList;
        }

        public void CloseClients()
        {
            this.RoomClosedEvent?.Invoke();
        }

        public List<Model.Estimator> GetEstimators()
        {
            return this.estimators;
        }

        public bool IsAsync()
        {
            return this.isAsync;
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
    }
}
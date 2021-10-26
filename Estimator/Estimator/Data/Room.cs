﻿using Estimator.Data.Exceptions;
using Estimator.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Estimator.Data
{
    public class Room
    {
        #region fields

        private readonly List<string> fibonacciNumbers = new List<string>()
            {"0", "0.5", "1", "2", "3", "5", "8", "13", "21", "34", "infinite", "coffee"};

        private readonly List<string> tshirtSizes = new List<string>()
            {"XS", "S", "M", "L", "XL", "XXL", "infinite", "coffee"};

        private string RoomID;
        private string titel = string.Empty;
        private int type; // 1 = fibonacinumbers 2 = Tshirt Sizes
        private Estimator host;

        private List<Estimator> estimators = new List<Estimator>();
        private List<DiagramData> diagramDataList;

        #endregion

        #region public

        public Room(string roomId, Estimator host, int type)
        {
            this.RoomID = roomId;
            this.host = host;
            this.type = type;
            this.AddEstimator(host);
        }

        public void AddEstimator(Estimator estimator)
        {
            if (this.estimators.Any(e => e.Name.Equals(estimator.Name)))
                throw new UsernameAlreadyInUseException();

            this.estimators.Add(estimator);
            this.UpdateEstimatorList?.Invoke();
        }

        public void RemoveEstimator(Estimator estimator)
        {
            try
            {
                this.estimators.Remove(this.estimators.Single(e => e.Name.Equals(estimator.Name)));
                this.UpdateEstimatorList?.Invoke();
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
            this.StartEstimation?.Invoke(titel);
        }

        public int GetRoomType()
        {
            return this.type;
        }

        public string GetRoomID()
        {
            return this.RoomID;
        }

        public bool IsHost(string name)
        {
            return this.host.Name == name;
        }

        //TODO: Überarbeiten! @Leo
        public void SetDiagramList(int type)
        {
            var diagrammData = new List<DiagramData>();
            var voteList = this.GetVoteList(type);
            foreach (var voteTopic in voteList)
            {
                var voteNumber = this.estimators.Count(voter => voteTopic == voter.Estimation);
                diagrammData.Add(new DiagramData(voteTopic, voteNumber.ToString()));
            }

            this.diagramDataList = diagrammData;
        }

        public List<DiagramData> GetDiagramList()
        {
            return this.diagramDataList;
        }

        public void CloseClients()
        {
            this.RoomClosed?.Invoke();
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

        public event Notify UpdateEstimatorList;
        public event Notify RoomClosed;

        public event NotifyString StartEstimation;

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Estimator.Data;
using Estimator.Data.Model;
using Xunit;

namespace Estimator.Tests
{
    public class RoomManagerTest
    {
        [Fact]
        public void CreateRoomTest()
        {
            #region Assign

            var core = new Core();

            var listVotingResultForTest = new List<VotingResults>()
            {
                new VotingResults("S",0,new List<string>()),
                new VotingResults("M",0,new List<string>()),
                new VotingResults("L",0,new List<string>()),
                new VotingResults("XL",0,new List<string>())
            };

            #endregion

            #region Act



            #endregion

            #region Assert



            #endregion
        }

    }
}

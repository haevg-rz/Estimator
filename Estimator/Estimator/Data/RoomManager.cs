using Estimator.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Estimator.Data.Exceptions;

namespace Estimator.Data
{
    public class RoomManager
    {
        #region fields

        private List<Room> rooms = new List<Room>
        {
            new Room("123456", new Estimator("admin"), 2 ),
            new Room("234567", new Estimator("admin"), 1 )
        };
        private const string permitedRoomIdChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        #endregion

        #region public

        public string CreateRoom(int type, Estimator estimator)
        {
            try
            {
                var roomId = this.GetRoomId();
                this.rooms.Add(new Room(roomId, estimator, type));

                return roomId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void CloseRoom(string roomId)
        {
            try
            {
                this.rooms.RemoveAll(r => r.GetRoomID().Equals(roomId));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new RoomIdNotFoundException();
            }
        }

        public void JoinRoom(string roomId, string estimatorName)
        {
            if(this.rooms.All(r => r.GetRoomID() != roomId))
                throw new RoomIdNotFoundException();


            if (this.rooms.Single(r => r.GetRoomID().Equals(roomId)).IsEstimatorRegistered(estimatorName))
                throw new UsernameAlreadyInUseException();

            try
            {
                this.rooms.Single(r => r.GetRoomID().Equals(roomId)).AddEstimator(new Estimator(estimatorName));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }

        }

        public bool IsHost(string hostName, string roomId)
        {
            try
            {
                return this.rooms.Single(r => r.GetRoomID().Equals(roomId)).IsHost(hostName);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new RoomIdNotFoundException();
            }

        }

        public int GetRoomType(string roomId, string estimatorName)
        {
            try
            {
                return this.rooms.Single(r => r.GetRoomID().Equals(roomId) && r.IsEstimatorRegistered(estimatorName)).GetRoomType();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new RoomIdNotFoundException();
            }
        }

        public void LeaveRoom(Estimator estimator, string roomId)
        {
            try
            {
                this.rooms.Single(r => r.GetRoomID() == roomId).RemoveEstimator(estimator);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new RoomIdNotFoundException();
            }
        }

        public void EntryVote(Estimator estimator, string roomId)
        {
            try
            {
                this.rooms.Single(r => r.GetRoomID() == roomId).SetEstimation(estimator);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public void StartEstimation(string roomId, string titel)
        {
            try
            {
                this.rooms.Single(r => r.GetRoomID().Equals(roomId)).ResetAllEstimates();
                this.rooms.Single(r => r.GetRoomID().Equals(roomId)).SetTitel(titel);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new RoomIdNotFoundException();
            }
        }

        public List<DiagramData> CloseEstimation(string roomId)
        {
            try
            {
                //this.rooms.Single(r=> r.GetRoomID().Equals(roomId)).SetDiagramList(type);
                return this.rooms.Single(r=> r.GetRoomID().Equals(roomId)).GetDiagramList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new RoomIdNotFoundException();
            }
        }

        #endregion

        #region private

        private string GetRoomId()
        {
            var stringChars = new char[6];
            var random = new Random();
            var isRoomIdUnique = false;

            do
            {
                for (var i = 0; i < stringChars.Length; i++) stringChars[i] = permitedRoomIdChars[random.Next(permitedRoomIdChars.Length)];

                isRoomIdUnique = !this.rooms.Any(e => e.GetRoomID() == stringChars.ToString());
            } while (isRoomIdUnique);

            return stringChars.ToString();
        }

        #endregion
    }
}
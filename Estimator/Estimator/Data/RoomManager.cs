using Estimator.Data.Enum;
using Estimator.Data.Exceptions;
using Estimator.Data.Interface;
using Estimator.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Estimator.Tests")]
[assembly: InternalsVisibleTo("Estimator.Tests.Pages")]

namespace Estimator.Data
{
    public class RoomManager : IRoomManager
    {
        #region fields

        internal List<Room> Rooms = new List<Room>();

        internal AdminArea adminArea = new AdminArea();

        private const string permitedRoomIdChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private const string pattern = "^[A-Za-z0-9]+$";

        #endregion

        #region public

        public string CreateRoom(RoomType type, Model.Estimator estimator) //TODO only 1 verweis??
        {
            try
            {
                var roomId = this.GetRoomId();
                this.Rooms.Add(new Room(roomId, estimator, type));
                return roomId;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public string CreateRoom(RoomType type, Model.Estimator estimator, int daysUntilResolution)
        {
            try
            {
                var roomId = this.GetRoomId();
                this.Rooms.Add(new Room(roomId, estimator, type, daysUntilResolution));
                return roomId;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }


        public void CloseRoom(string roomId)
        {
            try
            {
                this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).CloseClients();
                this.Rooms.RemoveAll(r => r.GetRoomID().Equals(roomId));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public Room GetRoomById(string roomId)
        {
            try
            {
                return this.Rooms.Single(r => r.GetRoomID().Equals(roomId));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new RoomIdNotFoundException();
            }
        }

        public void JoinRoom(string roomId, string estimatorName)
        {
            if (!this.Rooms.Any(r => r.GetRoomID().Equals(roomId)))
                throw new RoomIdNotFoundException();


            if (this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).IsEstimatorRegistered(estimatorName))
                throw new UsernameAlreadyInUseException();

            try
            {
                this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).AddEstimator(new Model.Estimator(estimatorName));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public void LeaveRoom(Model.Estimator estimator, string roomId)
        {
            try
            {
                this.Rooms.Single(r => r.GetRoomID() == roomId).RemoveEstimator(estimator);
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
                this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).ResetAllEstimates();
                this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).SetTitel(titel);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public void CloseEstimation(string roomId)
        {
            try
            {
                this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).SetDiagramList();
                this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).CloseEstimation();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public List<DiagramValue> GetDiagramDataByRoomId(string roomId)
        {
            try
            {
                return this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).GetDiagramList();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public void EntryVote(Model.Estimator estimator, string roomId)
        {
            try
            {
                this.Rooms.Single(r => r.GetRoomID() == roomId).SetEstimation(estimator);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        public RoomType GetRoomType(string roomId, string estimatorName)
        {
            try
            {
                return this.Rooms.Single(r => r.GetRoomID().Equals(roomId) && r.IsEstimatorRegistered(estimatorName))
                    .GetRoomType();
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
                return this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).IsHost(hostName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Room>> GetListOfRooms(string password)
        {
            if (await this.adminArea.IsAdmin(password))
                return this.Rooms;

            return null;
        }

        #endregion

        #region private

        public string GetRoomId()
        {
            var stringChars = new char[6];
            var random = new Random();
            var isRoomIdUnique = false;

            do
            {
                for (var i = 0; i < stringChars.Length; i++)
                    stringChars[i] = permitedRoomIdChars[random.Next(permitedRoomIdChars.Length)];

                isRoomIdUnique = this.Rooms.All(e => e.GetRoomID() != stringChars.ToString());
            } while (!isRoomIdUnique);

            return new string(stringChars);
        }

        public bool IsRoomAsync(string roomId)
        {
            return this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).IsAsync();
        }

        public bool HasEstimatorEstimated(string roomId, string estimatorName)
        {
            return this.Rooms.Single(r => r.GetRoomID().Equals(roomId)).HasEstimated(estimatorName);
        }

        public bool IsInvalidInput(string input)
        {
            return !Regex.IsMatch(input, pattern);
        }

        #endregion
    }
}
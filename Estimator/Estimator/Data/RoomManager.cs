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

        private List<Room> rooms = new List<Room>();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        #endregion

        #region public

        public string CreateRoom(string titel, int type, Estimator estimator)
        {
            var roomId = this.GetRoomId();
            this.rooms.Add(new Room(roomId, estimator, type));

            return roomId;
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

        public int JoinRoom(string roomId, string estimatorName)
        {
            if (this.rooms.Any(r => r.GetRoomID() == roomId))
                throw new RoomIdNotFoundException();


            if (this.rooms.Single(r => r.GetRoomID().Equals(roomId)).IsEstimatorRegistered(estimatorName))
                throw new UsernameAlreadyInUseException();

            int roomType;

            try
            {
                this.rooms.Single(r => r.GetRoomID().Equals(roomId)).AddEstimator(new Estimator(estimatorName));
                roomType = this.rooms.Single(r => r.GetRoomID().Equals(roomId)).GetRoomType();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }

            return roomType;
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

        public void StartEsimation(string roomId, string titel)
        {
            try
            {
                this.rooms.Single(r => r.GetRoomID().Equals(roomId)).ResetAllVotes();
                this.rooms.Single(r => r.GetRoomID().Equals(roomId)).SetTitel(titel);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new RoomIdNotFoundException();
            }
        }

        public List<DiagramData> CloseVoting(string roomId, int type)
        {
            try
            {
                this.rooms.Single(r=> r.GetRoomID().Equals(roomId)).SetDiagramList(type);
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
                for (var i = 0; i < stringChars.Length; i++) stringChars[i] = chars[random.Next(chars.Length)];

                isRoomIdUnique = !this.rooms.Any(e => e.GetRoomID() == stringChars.ToString());
            } while (isRoomIdUnique);

            return stringChars.ToString();
        }

        #endregion
    }
}
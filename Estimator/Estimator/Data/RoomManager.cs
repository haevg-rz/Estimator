﻿using System;
using System.Collections.Generic;
using Estimator.Data.Model;

namespace Estimator.Data
{
    public class RoomManager
    {
        private Dictionary<string, Room> roomDictonary = new Dictionary<string, Room>();

        private readonly string theVoterHasJoin = "the Voter has join";
        private readonly string wrongRoomId = "wrong room Id";

        public string CreateRoom(string name, string type, Voter voter)
        {
            var isNewRoomId = false;
            var roomId = String.Empty;
            do
            {
                roomId = this.GetRoomId();
                if (this.roomDictonary.ContainsKey(roomId) == false)
                    isNewRoomId = true;

            } while (!isNewRoomId);

            this.roomDictonary.Add(roomId, new Room(name, type));
            this.roomDictonary[roomId].AddVoter(voter);
            return roomId;
        }

        public (bool sucess,string message) JoinRoom(string roomId, string name)
        {
            var hasVoterJoin = this.roomDictonary[roomId].HasVoterJoin(name);

            if (hasVoterJoin)
                return (false,theVoterHasJoin);

            this.roomDictonary[roomId].AddVoter(new Voter(name, String.Empty));
            return this.roomDictonary.ContainsKey(roomId) ? (false,this.wrongRoomId) : (true,this.roomDictonary[roomId].GetType());
        }

        public void CloseRoom(string roomId)
        {
            this.roomDictonary.Remove(roomId);
        }

        public void EntryVote(Voter voter,string roomId)
        {
            this.roomDictonary[roomId].SetVote(voter);
        }

        public void StartVoting(string roomId, string taskname)
        {
            this.roomDictonary[roomId].ResetAllVotes();
            this.roomDictonary[roomId].SetTaskName(taskname);
        }

        public List<DiagramData> CloseVotingHost(string roomId,string type)
        {
            this.roomDictonary[roomId].SetDiagramList(type);
            return this.roomDictonary[roomId].GetDiagramList();
        }

        public List<DiagramData> CloseVoting(string roomId)
        {
            return this.roomDictonary[roomId].GetDiagramList();
        }

        private string GetRoomId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}

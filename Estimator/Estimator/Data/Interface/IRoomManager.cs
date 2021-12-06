using Estimator.Data.Enum;
using Estimator.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estimator.Data.Interface
{
    public interface IRoomManager
    {
        string CreateRoom(RoomType type, Model.Estimator estimator);
        public string CreateRoom(RoomType type, Model.Estimator estimator, int daysUntilResolution);
        void CloseRoom(string roomId);
        Room GetRoomById(string roomId);
        void JoinRoom(string roomId, string estimatorName);
        void LeaveRoom(Model.Estimator estimator, string roomId);
        void StartEstimation(string roomId, string titel);
        void CloseEstimation(string roomId);
        List<DiagramValue> GetDiagramDataByRoomId(string roomId);
        void EntryVote(Model.Estimator estimator, string roomId);
        RoomType GetRoomType(string roomId, string estimatorName);
        bool IsHost(string hostName, string roomId);
        string GetRoomId();
        bool IsRoomAsync(string roomId);
        bool HasEstimatorEstimated(string roomId, string estimatorName);
        Task<List<Data.Room>> GetListOfRooms(string password);
        bool IsSolidInput(string input);

    }
}
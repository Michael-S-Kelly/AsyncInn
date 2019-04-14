using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoomManager
    {
        Task CreateRoom(Room room);

        void UpdateRoom(int id, Room room);

        bool DeleteRoom(int id);

        Task<Room> GetRoom(int id);

        Task<List<Room>> GetRooms();

        Task<List<RoomAmenities>> GetRoomAmenity(int amenitiesID);

        //Task<List<RoomAmenities>> GetRoomAmenities(int AmenitiesID);

        Task AssignAmenity(RoomAmenities roomAmenity);

        //Task RemoveRoomAmenity(int amenitiesID);

        bool RoomExists(int id);
    }
}

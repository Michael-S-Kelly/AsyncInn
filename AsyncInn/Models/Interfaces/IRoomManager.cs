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

        void DeleteRoom(Room room);

        Task<Room> GetRoom(int id);

        Task<List<Room>> GetRooms();

        Task<List<HotelRoom>> GetHotelRooms(int id);

        Task<List<RoomAmenities>> GetRoomAmenities(int ID);
        Task<List<RoomAmenities>> GetRoomAmenity(int AmenitiesID);
        Task AddAmenity(RoomAmenities roomAmenities);
        Task RemoveAmenity(int AmenitiesID);

        bool RoomExists(int id);
    }
}

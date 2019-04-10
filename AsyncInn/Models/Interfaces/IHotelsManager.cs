using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelsManager
    {
        Task CreateHotel(Hotel hotel);

        void UpdateHotel(int id, Hotel hotel);

        void DeleteHotel(int id, Hotel hotel);

        Task<Hotel> GetHotel(int id);

        Task<List<Hotel>> GetHotels();

        Task<List<HotelRoom>> GetHotelRooms(int id);
        Task<HotelRoom> GetHotelRoom(int roomNumber);
        Task AssignRoom(HotelRoom hotelRoom);
        Task UpdateRoomAssignment(HotelRoom hotelRoom);
        Task RemoveRoomAssignment(int roomNumber);

        

        bool HotelRoomExists(int roomNumber);

        bool HotelExists(int id);
    }
}

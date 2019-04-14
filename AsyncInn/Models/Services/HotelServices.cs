using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AsyncInn.Models.Services
{
    public class HotelServices : IHotelsManager
    {
        private AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task AssignRoom(HotelRoom hotelRoom)
        {
            _context.Add(hotelRoom);
            await _context.SaveChangesAsync();
        }

        public async Task CreateHotel(Hotel hotel)
        {
            _context.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public void DeleteHotel(int id)
        {
            var room = _context.Hotel.Where(i => i.ID == id);
            if (room != null)
            {
                _context.Remove(room);
                _context.SaveChanges();
            }
            
        }

        public async Task<Hotel> GetHotel(int id)
        {
            Hotel hotel = await _context.Hotel
                .Include(hr => hr.HotelRoom)
                .ThenInclude(r => r.Room)
                .FirstOrDefaultAsync(i => i.ID == id);
            return hotel;
        }

        public async Task<HotelRoom> GetHotelRoom(int roomNumber)
        {
            return await _context.HotelRoom.FirstOrDefaultAsync(rn => rn.RoomNumber == roomNumber);
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int id)
        {
            return await _context.HotelRoom.Where(r => r.RoomID == id).ToListAsync();
        }

        public async Task<List<Hotel>> GetHotels()
        {
            return await _context.Hotel.ToListAsync();
        }

        public bool HotelRoomExists(int roomNumber)
        {
            return _context.HotelRoom.Any(rn => rn.RoomNumber == roomNumber);
        }

        public bool HotelExists(int id)
        {
            return _context.Hotel.Any(r => r.ID == id);
        }

        public async Task RemoveRoomAssignment(int roomNumber)
        {
            var rmNum = await GetHotelRoom(roomNumber);
            _context.HotelRoom.Remove(rmNum);
        }

        public async void UpdateHotel(int id, Hotel hotel)
        {
            _context.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAssignment(HotelRoom hotelRoom)
        {
            _context.Update(hotelRoom);
            await _context.SaveChangesAsync();
        }
    }
}

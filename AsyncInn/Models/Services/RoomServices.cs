using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class RoomServices : IRoomManager
    {
        private AsyncInnDbContext _context;

        public RoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task AssignAmenity(RoomAmenities roomAmenities)
        {
            _context.Add(roomAmenities);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRoom(Room room)
        {
            _context.Add(room);
            await _context.SaveChangesAsync();
        }

        public void DeleteRoom(int id)
        {
            _context.Remove(room);
            _context.SaveChanges();
        }

        public async Task<Room> GetRoom(int id)
        {
            Room room = await _context.Room
                .Include(ra => ra.RoomAmenities)
                .ThenInclude(a => a.Amenities)
                .FirstOrDefaultAsync(i => i.ID == id);
            return room;
        }

        public async Task<RoomAmenities> GetRoomAmenity(int amenitiesID)
        {
            return await _context.RoomAmenities.FirstOrDefaultAsync(rn => rn.AmenitiesID == amenitiesID);
        }

        public async Task<List<RoomAmenities>> GetRoomAmenities(int id)
        {
            return await _context.RoomAmenities.Where(ra => ra.AmenitiesID == id).ToListAsync();
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Room.ToListAsync();
        }

        public bool RoomAmenitiesExists(int amenitiesID)
        {
            return _context.RoomAmenities.Any(ra => ra.AmenitiesID == amenitiesID);
        }

        public bool RoomExists(int id)
        {
            return _context.Room.Any(r => r.ID == id);
        }

        public async Task RemoveRoomAmenity(int amenityID)
        {
            var amenity = await GetRoomAmenity(amenityID);
            _context.RoomAmenities.Remove(amenity);
        }

        public async void UpdateRoom(int id, Room room)
        {
            _context.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAmenity(RoomAmenities roomAmenities)
        {
            _context.Update(roomAmenities);
            await _context.SaveChangesAsync();
        }
    }
}

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
        private AsyncInnDbContext _room;

        public RoomServices(AsyncInnDbContext room)
        {
            _room = room;
        }

        /// <summary>
        /// (Create) Adds a new room template
        /// </summary>
        /// <param name="room">new room</param>
        /// <returns></returns>
        public async Task CreateRoom(Room room)
        {
            _room.Add(room);
            await _room.SaveChangesAsync();
        }

        /// <summary>
        /// (Read) Returns a list of all the room templates
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> GetRooms()
        {
            return await _room.Room.ToListAsync();
        }

        /// <summary>
        /// (Read) Gets a room template by ID
        /// </summary>
        /// <param name="id">Room ID</param>
        /// <returns></returns>
        public async Task<Room> GetRoom(int id)
        {
            var room = await _room.Room.FindAsync(id);
            if (room == null)
            {
                return null;
            }

            return room;
        }

        /// <summary>
        /// (Update) Updates a room template by ID
        /// </summary>
        /// <param name="id">Room ID</param>
        /// <param name="room">Room</param>
        public async void UpdateRoom(int id, Room room)
        {
            if (id == room.ID)
            {
                _room.Update(room);
                await _room.SaveChangesAsync();
            }
        }

        /// <summary>
        /// (Delete) Deletes a room template by ID
        /// </summary>
        /// <param name="id">Room ID</param>
        /// <returns></returns>
        public bool DeleteRoom(int id)
        {
            var room = _room.Room.Where(i => i.ID == id);
            if (room != null)
            {
                _room.Remove(room);
                _room.SaveChanges();
            }
            return true;
        }
        
        /// <summary>
        /// (Create) Assigns an amenity to the room
        /// </summary>
        /// <param name="roomAmenities"></param>
        /// <returns></returns>
        public async Task AssignAmenity(RoomAmenities roomAmenity)
        {
            _room.Add(roomAmenity);
            await _room.SaveChangesAsync();
        }

        /// <summary>
        /// (Read) Gets an Amenity by ID
        /// </summary>
        /// <param name="amenitiesID">Amenity ID</param>
        /// <returns></returns>
        async Task<List<RoomAmenities>> IRoomManager.GetRoomAmenity(int amenitiesID)
        {
            return await _room.RoomAmenities.Where(rn => rn.AmenitiesID == amenitiesID).ToListAsync();
        }

        /*
        /// <summary>
        /// (Read) A list of Amenities by Room ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<RoomAmenities>> GetRoomAmenities(int id)
        {
            RoomAmenities roomAmenities = await _room.RoomAmenities
                .Include(r => r.RoomID)
                .ThenInclude(a => a.AmenitiesID)
                .FirstOrDefaultAsync(rm => rm.RoomID == id);
            return roomAmenities;
            //return await _room.RoomAmenities.Where(ra => ra.RoomID == id).ToListAsync();
        }

        public async Task RemoveRoomAmenity(int id)
        {
            var amenity = await GetRoomAmenities(id);
            _room.RoomAmenities.Remove(AmenitiesID);
        }
        */


        public bool RoomAmenitiesExists(int amenitiesID)
        {
            return _room.RoomAmenities.Any(ra => ra.AmenitiesID == amenitiesID);
        }

        public bool RoomExists(int id)
        {
            return _room.Room.Any(r => r.ID == id);
        }
    }
}

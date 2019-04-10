using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenitiesManager
    {
        Task CreateAmenity(Amenities amenities);

        void DeleteAmenity(Amenities amenities);

        Task<Amenities> GetAmenity(int id);

        Task<List<Amenities>> GetAmenities(Amenities amenities);

        Task<List<RoomAmenities>> GetRoomAmenities(int id);
    }
}

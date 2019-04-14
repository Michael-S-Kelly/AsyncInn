using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenitiesManager
    {
        Task CreateAmenty(Amenities amenity);

        bool DeleteAmenity(int id);

        Task<Amenities> GetAmenity(int id);

        Task<List<Amenities>> GetAmenities();

        bool AmenityExists(int id);
    }
}

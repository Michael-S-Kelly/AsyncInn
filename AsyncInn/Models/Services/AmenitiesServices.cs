using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class AmenitiesServices : IAmenitiesManager
    {
        private AsyncInnDbContext _context;
        
        public AmenitiesServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        async Task IAmenitiesManager.CreateAmenty(Amenities amenity)
        {
            _context.Add(amenity);
            await _context.SaveChangesAsync();
        }

        bool IAmenitiesManager.DeleteAmenity(int id)
        {
            var amenity = _context.Amenities.Where(i => i.ID == id);
            if (amenity != null)
            {
                _context.Remove(amenity);
                _context.SaveChanges();
            }
            return true;
        }

        async Task<Amenities> IAmenitiesManager.GetAmenity(int id)
        {
            Amenities amenity = await _context.Amenities
                .FirstOrDefaultAsync(i => i.ID == id);
            return amenity;
        }

        async Task<List<Amenities>> IAmenitiesManager.GetAmenities()
        {
            return await _context.Amenities.ToListAsync();
        }

        public bool AmenityExists(int id)
        {
            return _context.Room.Any(r => r.ID == id);
        }
    }
}

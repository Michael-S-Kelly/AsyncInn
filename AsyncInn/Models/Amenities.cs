using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Amenities
    {
        public int ID { get; set; }
        public int Name { get; set; }

        public RoomAmenities RoomAmenities { get; set; }
    }
}

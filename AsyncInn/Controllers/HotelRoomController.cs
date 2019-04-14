using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Controllers
{
    public class HotelRoomController : Controller
    {
        private readonly AsyncInnDbContext _context;
        private readonly IHotelsManager _hotel;
        private readonly IRoomManager _room;

        /// <summary>
        /// Connects the class to the Database
        /// </summary>
        /// <param name="context"></param>
        public HotelRoomController(IHotelsManager hotel, IRoomManager room, AsyncInnDbContext context)
        {
            _context = context;
            _hotel = hotel;
            _room = room;
        }

        /// <summary>
        /// Gets Hotel Rooms
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var AsyncInnDbContext = _context.HotelRoom
                .Include(h => h.Hotel)
                .Include(r => r.Room);
            return View(await AsyncInnDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets Hotel Room Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom
                .Include(h => h.Hotel)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.RoomID == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return View(hotelRoom);
        }

        /// <summary>
        /// Get Create Hotel Room
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name");
            ViewData["HotelID"] = new SelectList(_context.Hotel, "ID", "Name");
            return View();
        }

        /// <summary>
        /// Post Create Hotel Room
        /// </summary>
        /// <param name="hotelRoom"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelID,RoomNumber,RoomID,Rate,PetFriendly")] HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelID"] = new SelectList(_context.Room, "ID", "Name", hotelRoom.HotelID);
            ViewData["RoomID"] = new SelectList(_context.Amenities, "ID", "Name", hotelRoom.RoomID);
            return View(hotelRoom);
        }

        /// <summary>
        /// Get Edit Hotel Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom.FindAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            ViewData["HotelID"] = new SelectList(_context.Room, "ID", "Name", hotelRoom.HotelID);
            ViewData["RoomID"] = new SelectList(_context.Amenities, "ID", "Name", hotelRoom.RoomID);
            return View(hotelRoom);
        }

        /// <summary>
        /// Post Edit Hotel Room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotelRoom"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HotelID,RoomNumber,RoomID,Rate,PetFriendly")] HotelRoom hotelRoom)
        {
            if (id != hotelRoom.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelRoomExists(hotelRoom.RoomID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelID"] = new SelectList(_context.Room, "ID", "Name", hotelRoom.HotelID);
            ViewData["RoomID"] = new SelectList(_context.Amenities, "ID", "Name", hotelRoom.RoomID);
            return View(hotelRoom);
        }

        /// <summary>
        /// Get Delete Hotel Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom
                .Include(h => h.Hotel)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.RoomNumber == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return View(hotelRoom);
        }

        /// <summary>
        /// Post Delete Hotel Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelRoom = await _context.HotelRoom.FindAsync(id);
            _context.HotelRoom.Remove(hotelRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Confirms whether the Hotel Room Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool HotelRoomExists(int id)
        {
            return _context.HotelRoom.Any(r => r.RoomNumber == id);
        }
    }
}
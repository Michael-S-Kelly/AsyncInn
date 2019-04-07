using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;

namespace AsyncInn.Controllers
{
    public class RoomAmenitiesController : Controller
    {
        private readonly AsyncInnDbContext _context;

        public RoomAmenitiesController(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets Amenities
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var AsyncInnDbContext = _context.RoomAmenities
                .Include(r => r.Room)
                .Include(a => a.Amenities);
            return View(await AsyncInnDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets Room Amenities Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAmenities = await _context.RoomAmenities
                .Include(r => r.Room)
                .Include(a => a.Amenities)
                .FirstOrDefaultAsync(a => a.AmenitiesID == id);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            return View(roomAmenities);
        }

        /// <summary>
        /// Get Create Room Amenities
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "Name");
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name");
            return View();
        }

        /// <summary>
        /// Post Create Room Amenities
        /// </summary>
        /// <param name="roomAmenities"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,AmenitiesID")] RoomAmenities roomAmenities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomAmenities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name", roomAmenities.RoomID);
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "Name", roomAmenities.AmenitiesID);
            return View(roomAmenities);
        }

        /// <summary>
        /// Get Edit Room Amenities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAmenities = await _context.RoomAmenities.FindAsync(id);
            if (roomAmenities == null)
            {
                return NotFound();
            }
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name", roomAmenities.RoomID);
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "Name", roomAmenities.AmenitiesID);
            return View(roomAmenities);
        }

        /// <summary>
        /// Post Edit Room Amenities
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomAmenities"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomID,AmenitiesID")] RoomAmenities roomAmenities)
        {
            if (id != roomAmenities.AmenitiesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomAmenities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomAmenitiesExists(roomAmenities.AmenitiesID))
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
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Name", roomAmenities.RoomID);
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "Name", roomAmenities.AmenitiesID);
            return View(roomAmenities);
        }

        /// <summary>
        /// Get Delete Room Amenity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAmenities = await _context.RoomAmenities
                .Include(r => r.RoomID)
                .Include(a => a.AmenitiesID)
                .FirstOrDefaultAsync(a => a.AmenitiesID == id);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            return View(roomAmenities);
        }

        /// <summary>
        /// Post Delete Room Amenity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomAmenities = await _context.RoomAmenities.FindAsync(id);
            _context.RoomAmenities.Remove(roomAmenities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Confirms whether the Room Amenity Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool RoomAmenitiesExists(int id)
        {
            return _context.RoomAmenities.Any(a => a.AmenitiesID == id);
        }
    }
}

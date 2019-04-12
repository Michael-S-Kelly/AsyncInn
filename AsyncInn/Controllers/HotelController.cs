using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelsManager _hotels;

        /// <summary>
        /// Connects the class to the Database
        /// </summary>
        /// <param name="context"></param>
        public HotelController(IHotelsManager hotels)
        {
            _hotels = hotels;
        }

        /// <summary>
        /// Gets Hotel
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<Hotel> hotels = await _hotels.GetHotels();
            return View(hotels);
        }

        /// <summary>
        /// Gets Hotel Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var hotel = await _hotels.GetHotel(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        /// <summary>
        /// Get Create Hotel
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post Create Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,StreetAddress,City,State,Phone")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _hotels.CreateHotel(hotel);
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        /// <summary>
        /// Get Edit Hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var hotel = await _hotels.GetHotel(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        /// <summary>
        /// Post Edit Hotel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,StreetAddress,City,State,Phone")] Hotel hotel)
        {
            if (id != hotel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _hotels.UpdateHotel(id, hotel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.ID))
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
            return View(hotel);
        }

        /// <summary>
        /// Get Delete Hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var hotel = await _hotels.GetHotel(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }
        
        /// <summary>
        /// Post Delete Hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _hotels.DeleteHotel(id);

            return RedirectToAction(nameof(Index));
        }
        

        /// <summary>
        /// Confirms whether the Hotel Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool HotelExists(int id)
        {
            return _hotels.HotelExists(id);
        }
    }
}
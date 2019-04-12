using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Controllers
{
    public class AmenitiesController : Controller
    {
        private readonly IAmenitiesManager _amenities;

        /// <summary>
        /// Connects the class to the Database
        /// </summary>
        /// <param name="context"></param>
        public AmenitiesController(IAmenitiesManager amenities)
        {
            _amenities = amenities;
        }

        /// <summary>
        /// Gets Amenities
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<Amenities> amenities = await _amenities.GetAmenities();
            return View(amenities);
        }

        /// <summary>
        /// Gets Amenities Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var amenity = await _amenities.GetAmenity(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return View(amenity);
        }

        /// <summary>
        /// Get Create Amenities
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post Create Amenities
        /// </summary>
        /// <param name="amenities"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Amenities amenity)
        {
            if (ModelState.IsValid)
            {
                await _amenities.CreateAmenty(amenity);
                return RedirectToAction(nameof(Index));
            }
            return View(amenity);
        }

        /// <summary>
        /// Get Delete Amenities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var amenity = await _amenities.GetAmenity(id);
            if (amenity == null)
            {
                return NotFound();
            }

            return View(amenity);
        }

        /// <summary>
        /// Post Delete Amenities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _amenities.DeleteAmenity(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Confirms whether the Amenities Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool AmenitiesExists(int id)
        {
            return _amenities.AmenityExists(id);
        }
    }
}
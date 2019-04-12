using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomManager _room;

        public RoomController(IRoomManager context)
        {
            _room = context;
        }

        /// <summary>
        /// Gets Room
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<Room> room = await _room.GetRooms();
            return View(room);
        }

        /// <summary>
        /// Gets Room Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var room = await _room.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        /// <summary>
        /// Get Create Room
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post Create Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Layout")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _room.CreateRoom(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        /// <summary>
        /// Get Edit Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var room = await _room.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        /// <summary>
        /// Post Edit Room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Layout")] Room room)
        {
            if (id != room.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _room.UpdateRoom(id, room);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.ID))
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
            return View(room);
        }

        /// <summary>
        /// Get Delete Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var room = await _room.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        /// <summary>
        /// Post Delete Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _room.DeleteRoom(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Confirms whether the Room Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool RoomExists(int id)
        {
            return _room.RoomExists(id);
        }
    }
}
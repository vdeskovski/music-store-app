using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DomainModels;
using MusicStore.Repository;


namespace MusicStoreApp.Controllers
{
    public class UserPlaylistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserPlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserPlaylists
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserPlaylists.ToListAsync());
        }

        // GET: UserPlaylists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.UserPlaylists
                .Include("TrackInUserPlaylists")
                .Include("TrackInUserPlaylists.Track")
                .Include("TrackInUserPlaylists.Track.Album")
                .Include("TrackInUserPlaylists.Track.Album.Artist")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            return View(userPlaylist);
        }

        // GET: UserPlaylists/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserPlaylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] UserPlaylist userPlaylist)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var createdBy = _context.Users.FirstOrDefault(u => u.Id == userId);
                userPlaylist.User = createdBy;
                int i = 0;
                userPlaylist.Id = Guid.NewGuid();
                _context.Add(userPlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userPlaylist);
        }

        // GET: UserPlaylists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.UserPlaylists.FindAsync(id);
            if (userPlaylist == null)
            {
                return NotFound();
            }
            return View(userPlaylist);
        }

        // POST: UserPlaylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,TotalTracks,Id")] UserPlaylist userPlaylist)
        {
            if (id != userPlaylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPlaylistExists(userPlaylist.Id))
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
            return View(userPlaylist);
        }

        // GET: UserPlaylists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.UserPlaylists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            return View(userPlaylist);
        }

        // POST: UserPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userPlaylist = await _context.UserPlaylists.FindAsync(id);
            if (userPlaylist != null)
            {
                _context.UserPlaylists.Remove(userPlaylist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPlaylistExists(Guid id)
        {
            return _context.UserPlaylists.Any(e => e.Id == id);
        }
    }
}

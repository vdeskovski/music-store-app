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
using NuGet.DependencyResolver;

namespace MusicStoreApp.Controllers
{
    public class TracksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TracksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult AddToPlaylist(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdBy = _context.Users
                .Include("UserPlaylists")
                .FirstOrDefault(u => u.Id == userId);
            var playlists = createdBy.UserPlaylists;
            ViewData["UserPlaylists"] = new SelectList(playlists, "Id", "Name");
            var track = _context.Tracks.FirstOrDefault(x => x.Id == id);
            return View(track);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddToPlaylist(Guid trackId, Guid playlistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdBy = _context.Users
                .Include("UserPlaylists")
                .FirstOrDefault(u => u.Id == userId);
            var playlists = createdBy.UserPlaylists;
            ViewData["UserPlaylists"] = new SelectList(playlists, "Id", "Name");
            var selectedTrack = _context.Tracks.FirstOrDefault(x => x.Id == trackId);
            var selectedPlaylist = _context.UserPlaylists.FirstOrDefault(x => x.Id == playlistId);

            if (selectedPlaylist != null && selectedTrack != null)
            {
                if (selectedPlaylist.TrackInUserPlaylists == null)
                {
                    selectedPlaylist.TrackInUserPlaylists = new List<TrackInUserPlaylist>();
                }
                if (!selectedPlaylist.TrackInUserPlaylists.Any(t => t.TrackId == trackId))
                {
                    var trackInUserPlaylist = new TrackInUserPlaylist
                    {
                        TrackId = trackId,
                        UserPlaylistId = playlistId,
                        Track = selectedTrack,
                        UserPlaylist = selectedPlaylist,
                        Id = Guid.NewGuid()
                    };
                    selectedPlaylist.TotalTracks += 1;
                    _context.TrackInUserPlaylists.Add(trackInUserPlaylist);
                    _context.SaveChanges();
                }
            }




            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult DeleteFromUserPlaylist(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdBy = _context.Users
                .Include("UserPlaylists")
                .FirstOrDefault(u => u.Id == userId);
            var playlists = createdBy.UserPlaylists;
            var selectedTrackInPlaylist = _context.TrackInUserPlaylists.FirstOrDefault(x => x.TrackId == id);
            var selectedPlaylist = selectedTrackInPlaylist.UserPlaylist;
            if (selectedTrackInPlaylist != null)
            {
                _context.TrackInUserPlaylists.Remove(selectedTrackInPlaylist);
                selectedPlaylist.TotalTracks -= 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "UserPlaylists");
        }


        // GET: Tracks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tracks.Include(t => t.Album);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumName");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackName,Duration,AlbumId,Id")] Track track)
        {
            if (ModelState.IsValid)
            {
                track.Id = Guid.NewGuid();
                _context.Add(track);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", track.AlbumId);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TrackName,Duration,AlbumId,Id")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(Guid id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }
    }
}

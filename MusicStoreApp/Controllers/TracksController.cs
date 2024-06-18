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
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using NuGet.DependencyResolver;

namespace MusicStoreApp.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly IUserRepository _userRepository;
        private readonly IUserPlaylistService _userPlaylistService;
        private readonly IAlbumService _albumService;

        public TracksController(
            ITrackService trackService, 
            IUserRepository userRepository, 
            IUserPlaylistService userPlaylistService,
            IAlbumService albumService)
        {
            _trackService = trackService;
            _userRepository = userRepository;
            _userPlaylistService = userPlaylistService;
            _albumService = albumService;
        }

        [Authorize]
        public IActionResult AddToPlaylist(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdBy = _userRepository.Get(userId);
            var playlists = createdBy.UserPlaylists;
            ViewData["UserPlaylists"] = new SelectList(playlists, "Id", "Name");
            var track = _trackService.GetDetailsForTrack(id);
            return View(track);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddToPlaylist(Guid trackId, Guid playlistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdBy = _userRepository.Get(userId);
            var playlists = createdBy.UserPlaylists;
            ViewData["UserPlaylists"] = new SelectList(playlists, "Id", "Name");
            var selectedTrack = _trackService.GetDetailsForTrack(trackId);
            var selectedPlaylist = _userPlaylistService.GetDetailsForUserPlaylist(playlistId);

            _trackService.AddTrackToUserPlaylist(selectedPlaylist, selectedTrack);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult DeleteFromUserPlaylist(Guid id)
        {
            _trackService.DeleteTrackFromUserPlaylist(id);

            return RedirectToAction("Index", "UserPlaylists");
        }


        // GET: Tracks
        public IActionResult Index()
        {
            return View(_trackService.GetAllTracks());
        }

        // GET: Tracks/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _trackService.GetDetailsForTrack(id.Value);

            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "AlbumName");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TrackName,Duration,AlbumId,Id")] Track track)
        {
            if (ModelState.IsValid)
            {
                track.Id = Guid.NewGuid();
                _trackService.CreateNewTrack(track);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "Id", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _trackService.GetDetailsForTrack(id.Value);
            if (track == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "Id", track.AlbumId);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("TrackName,Duration,AlbumId,Id")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _trackService.UpdateExistingTrack(track);
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
            ViewData["AlbumId"] = new SelectList(_albumService.GetAllAlbums(), "Id", "Id", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = _trackService.GetDetailsForTrack(id.Value);
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
            _trackService.DeleteTrack(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(Guid id)
        {
            return _trackService.GetDetailsForTrack(id) != null;
        }
    }
}

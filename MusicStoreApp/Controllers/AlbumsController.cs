using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DomainModels;
using MusicStore.Repository;
using MusicStore.Service.Interface;

namespace MusicStoreApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;

        public AlbumsController(IAlbumService albumService, IArtistService artistService)
        {
            _albumService = albumService;
            _artistService = artistService;
        }

        // GET: Albums
        public IActionResult Index()
        {
            //var applicationDbContext = _context.Albums.Include(a => a.Artist);
            return View(_albumService.GetAllAlbums());
        }

        // GET: Albums/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _albumService.GetDetailsForAlbum(id.Value);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "StageName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AlbumName,AlbumPrice,AlbumImage,AlbumDescription,AlbumRating,ArtistId,Id")] Album album)
        {
            if (ModelState.IsValid)
            {
                album.Id = Guid.NewGuid();
                _albumService.CreateNewAlbum(album);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "Id", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _albumService.GetDetailsForAlbum(id.Value);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "Id", album.ArtistId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("AlbumName,AlbumPrice,AlbumImage,AlbumDescription,ArtistId,Id")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _albumService.UpdateExistingAlbum(album);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            ViewData["ArtistId"] = new SelectList(_artistService.GetAllArtists(), "Id", "Id", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

/*            var album = await _context.Albums
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);*/

            var album = _albumService.GetDetailsForAlbum(id.Value);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _albumService.DeleteAlbum(id);

            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(Guid id)
        {
            return _albumService.GetDetailsForAlbum(id) != null;
        }
    }
}

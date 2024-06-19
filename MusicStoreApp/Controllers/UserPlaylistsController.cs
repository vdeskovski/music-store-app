using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DomainModels;
using MusicStore.Repository;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;


namespace MusicStoreApp.Controllers
{
    public class UserPlaylistsController : Controller
    {
        private readonly IUserPlaylistService _userPlaylistService;
        private readonly IUserRepository _userRepository;

        public UserPlaylistsController(IUserPlaylistService userPlaylistService, IUserRepository userRepository)
        {
            _userPlaylistService = userPlaylistService;
            _userRepository = userRepository;
        }

        // GET: UserPlaylists
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_userPlaylistService.GetAllUserPlaylists(userId));
        }

        // GET: UserPlaylists/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = _userPlaylistService.GetDetailsForUserPlaylist(id.Value);

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
        public IActionResult Create([Bind("Name,Description,Id")] UserPlaylist userPlaylist)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var createdBy = _userRepository.Get(userId);
                userPlaylist.User = createdBy;
                userPlaylist.Id = Guid.NewGuid();
                _userPlaylistService.CreateNewUserPlaylist(userPlaylist);
                return RedirectToAction(nameof(Index));
            }
            return View(userPlaylist);
        }

        // GET: UserPlaylists/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = _userPlaylistService.GetDetailsForUserPlaylist(id.Value);

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
        public IActionResult Edit(Guid id, [Bind("Name,Description,TotalTracks,Id")] UserPlaylist userPlaylist)
        {
            if (id != userPlaylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userPlaylistService.UpdateExistingUserPlaylist(userPlaylist);
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
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = _userPlaylistService.GetDetailsForUserPlaylist(id.Value);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            return View(userPlaylist);
        }

        // POST: UserPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _userPlaylistService.DeleteUserPlaylist(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserPlaylistExists(Guid id)
        {
            return _userPlaylistService.GetDetailsForUserPlaylist(id) != null;
        }

        [HttpGet]
        [Authorize]
        public FileContentResult ExportAllPlaylists()
        {
            string fileName = "Playlists.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Playlists");
                worksheet.Cell(1, 1).Value = "Playlist Id";
                worksheet.Cell(1, 2).Value = "Owner Username";
                worksheet.Cell(1, 3).Value = "Playlist Name";
                worksheet.Cell(1, 4).Value = "Description";
                worksheet.Cell(1, 5).Value = "Total Tracks";

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var data = _userPlaylistService.GetAllUserPlaylists(userId);

                for (int i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    worksheet.Cell(i + 2, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = item.User.UserName;
                    worksheet.Cell(i + 2, 3).Value = item.Name;
                    worksheet.Cell(i + 2, 4).Value = item.Description;
                    worksheet.Cell(i + 2, 5).Value = item.TotalTracks.ToString();
                    var totalDuration = 0;
                    var totalDurationJ = 0;
                    for (int j = 0; j < item.TrackInUserPlaylists.Count(); j++)
                    {
                        worksheet.Cell(1, 6 + j).Value = "Track - " + (j + 1);
                        worksheet.Cell(i + 2, 6 + j).Value = item.TrackInUserPlaylists.ElementAt(j).Track.TrackName;
                        totalDuration += item.TrackInUserPlaylists.ElementAt(j).Track.Duration;
                        totalDurationJ = j;
                    }
                    worksheet.Cell(1, 7 + totalDurationJ).Value = "Total Duration";
                    worksheet.Cell(i + 2, 7 + totalDurationJ).Value = totalDuration + "min";
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }
    }
}

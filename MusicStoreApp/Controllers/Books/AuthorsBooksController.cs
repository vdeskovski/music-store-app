using Microsoft.AspNetCore.Mvc;
using MusicStore.Service.Interface.Books;

namespace MusicStoreApp.Controllers.Books
{
    public class AuthorsBooksController : Controller
    {

        private readonly IBookAuthorService _bookAuthorService;

        public AuthorsBooksController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }

        public IActionResult Index()
        {
            return View(_bookAuthorService.getAllBooks());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookAuthorService.getBook(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}

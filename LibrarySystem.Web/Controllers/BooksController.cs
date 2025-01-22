using AutoMapper;
using LibrarySystem.Models;
using LibrarySystem.Services;
using LibrarySystem.Web.Models.BooksViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            var bookViewModel = _mapper.Map<List<BookViewModel>>(books);
            return View(bookViewModel);
        }
    }
}
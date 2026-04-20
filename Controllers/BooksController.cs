using LibrarieOnline.Models;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AuthorId"] = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name");
            ViewData["CategoryId"] = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name");
            ViewData["PublisherId"] = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Price,AuthorId,CategoryId,PublisherId")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBook(book);
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["AuthorId"] = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Price,AuthorId,CategoryId,PublisherId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bookService.BookExists(book.BookId))
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

            ViewData["AuthorId"] = new SelectList(await _bookService.GetAuthors(), "AuthorId", "Name", book.AuthorId);
            ViewData["CategoryId"] = new SelectList(await _bookService.GetCategories(), "CategoryId", "Name", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(await _bookService.GetPublishers(), "PublisherId", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            await _bookService.AddToCart(id);
            return RedirectToAction("Index", "Orders");
        }
    }
}
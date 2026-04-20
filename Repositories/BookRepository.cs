using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarieOnline.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task Add(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Books.AnyAsync(e => e.BookId == id);
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task AddOrderWithItem(Book book)
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = book.Price
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                BookId = book.BookId,
                Quantity = 1,
                UnitPrice = book.Price
            };

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
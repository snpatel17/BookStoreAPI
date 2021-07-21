using BookStoreAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<Books>> GetAllBooksAsync()
        {
            var records = await _context.Books.Select(x => new Books() {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                CoverImage = x.CoverImage,
                Description = x.Description,
                Price = x.Price
            }).ToListAsync();

            return records;
        }
        public async Task<Books> GetBookByIdAsync(string bookId)
        {
            var record = await _context.Books.Where(x => x.Id == bookId).Select(x => new Books()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                CoverImage = x.CoverImage,
                Description = x.Description,
                Price = x.Price
            }).FirstOrDefaultAsync();

            return record;
        }

        public async Task<string> AddBookAsync(Books bookModel)
        {
            var book = new Books()
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Author = bookModel.Author,
                CoverImage = bookModel.CoverImage,
                Description = bookModel.Description,
                Price = bookModel.Price
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(string bookId, Books bookModel)
        {
            var book = new Books()
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Author = bookModel.Author,
                CoverImage = bookModel.CoverImage,
                Description = bookModel.Description,
                Price = bookModel.Price
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateBookAsync(string bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteBookAsync(string bookId)
        {
            var book = new Books() { Id = bookId};
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

        }
    }
}

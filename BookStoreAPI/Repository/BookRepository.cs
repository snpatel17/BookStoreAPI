using AutoMapper;
using BookStoreAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BooksModel>> GetAllBooksAsync()
        {
            var records = await _context.Books.ToListAsync();

            return _mapper.Map <List<BooksModel>>(records);
        }
        public async Task<BooksModel> GetBookByIdAsync(string bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BooksModel>(book);
        }

        public async Task<string> AddBookAsync(BooksModel bookModel)
        {
            var book = new BooksModel()
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

        public async Task UpdateBookAsync(string bookId, BooksModel bookModel)
        {
            var book = new BooksModel()
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
            var book = new BooksModel() { Id = bookId};
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

        }
    }
}

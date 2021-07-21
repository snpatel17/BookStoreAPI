using BookStoreAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
         Task<List<BooksModel>> GetAllBooksAsync();
        Task<BooksModel> GetBookByIdAsync(string bookId);

        Task<string> AddBookAsync(BooksModel bookModel);
        Task UpdateBookAsync(string bookId, BooksModel bookModel);
        Task UpdateBookAsync(string bookId, JsonPatchDocument bookModel);
        Task DeleteBookAsync(string bookId);


    }
}

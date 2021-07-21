using BookStoreAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
         Task<List<Books>> GetAllBooksAsync();
        Task<Books> GetBookByIdAsync(string bookId);

        Task<string> AddBookAsync(Books bookModel);
        Task UpdateBookAsync(string bookId, Books bookModel);
        Task UpdateBookAsync(string bookId, JsonPatchDocument bookModel);
        Task DeleteBookAsync(string bookId);


    }
}

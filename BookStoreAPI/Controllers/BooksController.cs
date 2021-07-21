using BookStoreAPI.Data;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById([FromRoute] string bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody]BooksModel bookModel)
        {
            var bookId = await _bookRepository.AddBookAsync(bookModel);
            return CreatedAtAction(nameof(GetBookById), new {bookId = bookId, controller= "books" }, bookId);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] BooksModel bookModel, [FromRoute] string bookId)
        {
            await _bookRepository.UpdateBookAsync(bookId,bookModel);
            return Ok();
        }
        [HttpPatch("{bookId}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] string bookId)
        {
            await _bookRepository.UpdateBookAsync(bookId, bookModel);
            return Ok();
        }
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] string bookId)
        {
            await _bookRepository.DeleteBookAsync(bookId);
            return Ok();
        }
    }
}

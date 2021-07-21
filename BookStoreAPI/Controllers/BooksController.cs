using BookStoreAPI.Data;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var books = await _bookRepository.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception err)
            {
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById([FromRoute] string bookId)
        {
            try
            {
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception err)
            {
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody]BooksModel bookModel)
        {
            try
            {
                var bookId = await _bookRepository.AddBookAsync(bookModel);
                return CreatedAtAction(nameof(GetBookById), new { bookId = bookId, controller = "books" }, bookId);
            }
            catch (Exception err)
            {
                return StatusCode(500, "Internal Server error");  
            }
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] BooksModel bookModel, [FromRoute] string bookId)
        {
            try
            {
                await _bookRepository.UpdateBookAsync(bookId, bookModel);
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, "Internal Server error");
            }
        }
        [HttpPatch("{bookId}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] string bookId)
        {
            try
            {
                await _bookRepository.UpdateBookAsync(bookId, bookModel);
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, "Internal Server error");
            }
        }
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] string bookId)
        {
            try
            {
                await _bookRepository.DeleteBookAsync(bookId);
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, "Internal Server error");
            }
        }
    }
}

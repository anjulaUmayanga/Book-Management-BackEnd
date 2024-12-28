
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        
        private static List<Book> Books = new List<Book>();

        
        private static int _lastId = 0;

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(Books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> AddBook(Book newBook)
        {
            
            newBook.Id = ++_lastId;

            
            Books.Add(newBook);

            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.ISBN = updatedBook.ISBN;
            book.PublicationDate = updatedBook.PublicationDate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            Books.Remove(book);
            return NoContent();
        }
    }
}


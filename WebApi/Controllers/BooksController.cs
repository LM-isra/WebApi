using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = _context.Books.Include(x => x.Author).Include(x => x.Editor).ToList();
            if (!books.Any()) return NotFound();

            return books;
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Book> Get(int id)
        {
            var book = _context.Books.Include(x => x.Author).Include(x => x.Editor).FirstOrDefault(x => x.BookId == id);
            if (book == null) return NotFound();

            return book;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Book book)
        {
            if (book.BookId != id) return BadRequest();

            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Book> Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.BookId == id);

            if (book == null) return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;
        }
    }
}

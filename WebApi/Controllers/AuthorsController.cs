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
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthorsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            var authors = _context.Authors.Include(x => x.Books).Include(x => x.Musics).ToList();

            if (!authors.Any()) return NotFound();

            return authors;
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public ActionResult<Author> Get(int id)
        {
            var author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.AuthorId == id);

            if (author == null) return NotFound();

            return author;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = author.AuthorId }, author);
        }

        [HttpPut]
        public ActionResult Put(int id, [FromBody] Author author)
        {
            if (author.AuthorId != id) return BadRequest();

            _context.Entry(author).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Author> Delete(int id)
        {
            var author =_context.Authors.FirstOrDefault(x => x.AuthorId == id);
            
            if (author == null) return NotFound();
            
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return author;
        }
    }
}

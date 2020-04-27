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
    public class EditorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EditorsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Editor>> Get()
        {
            var editors = _context.Editors.Include(x => x.Books).Include(x => x.Musics).ToList();

            if (!editors.Any()) return NotFound();

            return editors;
        }

        [HttpGet("{id}", Name = "ObtenerEditor")]
        public ActionResult<Editor> Get(int id)
        {
            var editor = _context.Editors.Include(x => x.Books).Include(x => x.Musics).FirstOrDefault(x => x.EditorId == id);

            if (editor == null) return NotFound();

            return editor;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Editor editor)
        {
            _context.Editors.Add(editor);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerEditor", new { id = editor.EditorId }, editor);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Editor editor)
        {
            if (editor.EditorId == id) return BadRequest();

            _context.Entry(editor).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Editor> Delete(int id)
        {
            var editor = _context.Editors.FirstOrDefault(x => x.EditorId == id);
            if (editor == null) return BadRequest();

            return editor;
        }
    }
}

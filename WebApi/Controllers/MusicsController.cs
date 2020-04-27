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
    public class MusicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public MusicsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Music>> Get()
        {
            var musics = _context.Musics.Include(x => x.Editor).Include(x => x.Author).ToList();

            if (!musics.Any()) return NotFound();

            return musics;
        }

        [HttpGet("{id}", Name = "ObtenerMusica")]
        public ActionResult<Music> Get(int id)
        {
            var music = _context.Musics.Include(x => x.Author).Include(x => x.Editor).FirstOrDefault(x => x.MusicId == id);

            if (music == null) return NotFound();

            return music;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Music music)
        {
            _context.Musics.Add(music);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerMusica", new { id = music.MusicId }, music);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Music music)
        {
            if (music.MusicId != id) return BadRequest();

            _context.Entry(music).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Music> Delete(int id)
        {
            var music = _context.Musics.FirstOrDefault(x => x.MusicId == id);

            if (music == null) return NotFound();

            _context.Musics.Remove(music);
            _context.SaveChanges();
            return music;
        }
    }
}

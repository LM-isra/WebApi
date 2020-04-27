using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Editor
    {
        [Key]
        public int EditorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Adress { get; set; }

        public List<Book> Books { get; set; }
        public List<Music> Musics { get; set; }
    }
}

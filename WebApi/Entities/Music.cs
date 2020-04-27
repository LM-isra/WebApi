using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class Music
    {
        [Key]
        public int MusicId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        [Required]
        public int EditorId { get; set; } 
        public Editor Editor { get; set; }  
        
        
    }
}

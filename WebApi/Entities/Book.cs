using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int EditorId { get; set; }
        public Author Author { get; set; }
        public Editor Editor { get; set; }

    }
}

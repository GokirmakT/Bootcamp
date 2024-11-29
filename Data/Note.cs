using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todoApp.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; } 

        [Required]
        public string Title { get; set; } 

        public string Content { get; set; } 

        public string Color { get; set; }  

        public DateTime CreatedAt { get; set; } = DateTime.Now;     

        
        public int UserId { get; set; } 

        [ForeignKey("UserId")]
        public User User { get; set; } 
    }
}

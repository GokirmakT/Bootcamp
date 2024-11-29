using System.ComponentModel.DataAnnotations;


namespace todoApp.Data
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserSurname { get; set; }

        [Required]
        [EmailAddress]
        public string UserMail { get; set; }

        [Required]
        [MinLength(6)]
        public string UserPassword { get; set; }
    }
}

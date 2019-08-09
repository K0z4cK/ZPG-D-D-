using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("tbl_Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50), Index(IsUnique = true)]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(30, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual ICollection<Character> Characters {get;set;}
    }
}
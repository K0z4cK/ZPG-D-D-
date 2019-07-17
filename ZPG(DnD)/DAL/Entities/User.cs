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

        public virtual ICollection<Character> Characters {get;set;}
    }
}
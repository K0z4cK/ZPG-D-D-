using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tblCharacters")]
    public class Character
    {
        [Key]
        public int Id { get; set; }



        [ForeignKey("UserOf")]
        public int UserId { get; set; }

        public virtual User UserOf { get; set; }

    }
}
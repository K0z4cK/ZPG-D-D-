using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tbl_Characters")]
    public class Character: Entity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserOf")]
        public int UserId { get; set; }

        public virtual User UserOf { get ; set; }
        }
}
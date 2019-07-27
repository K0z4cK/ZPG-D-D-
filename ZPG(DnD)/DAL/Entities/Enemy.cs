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
    public class Enemy: Entity
    {
        [Key]
        public int Id { get; set; }
        public int ExpGained { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tbl_Inventories")]
    public class CharacterInventory
    {
        [Key]
        [ForeignKey("CharacterOf")]
        public int Id { get; set; }
        public virtual Character CharacterOf { get; set; }

        public virtual ICollection<CharacterItem> CharacterItems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tbl_CharacterItems")]
    public class CharacterItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ItemOf")]
        public int ItemId { get; set; }

        [ForeignKey("InventoryOf")]
        public int InventoryId { get; set; }
        public bool isDressed { get; set; }

        public virtual CharacterInventory InventoryOf { get; set; }

        public virtual Item ItemOf { get; set; }
    }
}

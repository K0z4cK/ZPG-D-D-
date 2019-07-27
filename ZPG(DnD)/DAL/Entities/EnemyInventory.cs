using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tbl_EnemyInventories")]
    public class EnemyInventory
    {
        [Key]
        [ForeignKey("CharacterOf")]
        public int Id { get; set; }
        public virtual Enemy CharacterOf { get; set; }

        public virtual ICollection<EnemyItem> CharacterItems { get; set; }
    }
}

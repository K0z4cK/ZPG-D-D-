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
        [ForeignKey("EnemyOf")]
        public int Id { get; set; }
        public virtual Enemy EnemyOf { get; set; }

        public virtual ICollection<EnemyItem> EnemyItems { get; set; }
    }
}

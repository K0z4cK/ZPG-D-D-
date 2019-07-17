using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Inventory
    {
        [ForeignKey("CharacterOf")]
        public int Id { get; set; }


        public virtual Character CharacterOf { get; set; }

        public virtual ICollection<UserItem> UserItems { get; set; }
    }
}

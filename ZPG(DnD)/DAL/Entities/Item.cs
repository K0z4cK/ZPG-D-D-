using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public enum typeOfItem {Trash, Helmet, Armor, Gloves, Boots, Weapon, Amulet}
    public enum typeOfBonus { None, AddHP, AddArmor, AddInitiative, AddSpeed, AddChanceToHurt }
    [Table("tbl_Items")]
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public typeOfItem TypeOfItem { get; set; }
        public int equipmentBonus { get; set; }
        public typeOfBonus TypeOfBonus { get; set; }
        public int itemBonus { get; set; }
        public int Price { get; set; }
        public virtual ICollection<CharacterItem> CharacterItems { get; set; }
        public virtual ICollection<EnemyItem> EnemyItems { get; set; }
    }
}

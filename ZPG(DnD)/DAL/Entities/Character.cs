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
        public int Level { get; set; }
        public int Exp { get; set; }
        public Races Race { get; set; }
        public Clases Class { get; set; }
        public Aligments Aligment { get; set; }
        public Backgrounds Background { get; set; }
        public virtual User UserOf { get ; set; }
        public virtual CharacterSkills CharacterSkillsOf { get; set; }
    }
}
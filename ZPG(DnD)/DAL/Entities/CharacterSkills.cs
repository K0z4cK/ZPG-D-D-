using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tbl_CharacterSkills")]
    public class CharacterSkills
    {
        [Key]
        [ForeignKey("CharacterOf")]
        public int Id { get; set; }
        public virtual Character CharacterOf { get; set; }
        public int Acrobatics { get; set; }
        public int AnimalHandling { get; set; }
        public int Athletics { get; set; }
        public int Medicine { get; set; }
        public int Persuasion { get; set; }
        public int Religion { get; set; }
        public int SleightOfHand { get; set; }
        public int Stealth { get; set; }
        public int Survival { get; set; }
    }
}


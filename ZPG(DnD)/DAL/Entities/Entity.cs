using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public enum Races { Human, Elf, Ork, Hobbit, Dwarf, Goblin, Murlok, Danmer, Altmer, Bosmer, Demon, Kajit }
    public enum Clases { Fighter, Ranger, Sourcer, Rogue, Paladin, Wizard, Withcer, Druid }
    public enum Aligments { LawfulGood, NeutralGood, ChaoticGood, LawfulNeutral, TrueNeutral, ChaoticNeutral, LawfulEvil, NeutralEvil, ChaoticEvil }
    public enum Backgrounds {Spy, Soilder, ChoosedOne, Revenant, Deprived }

    public abstract class Entity
    {
        public  string Name { get; set; }
        public  int ArmorClass { get; set; }
        public  int Intitiative { get; set; }
        public  int Speed { get; set; }
        public  int HPMax { get; set; }
        public int HP { get; set; }

    }
}

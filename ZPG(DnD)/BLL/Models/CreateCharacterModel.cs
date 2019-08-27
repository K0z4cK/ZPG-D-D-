using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CreateCharacterModel
    {
        public string Name { get; set; }
        public Races Race { get; set; }
        public Clases Class { get; set; }
        public Aligments Aligment { get; set; }
        public Backgrounds Background { get; set; }
        public int Coins { get; set; }
        public int Exp { get; set; }
        public int Level { get; set; }
        public int ArmorClass { get; set; }
        public int Intitiative { get; set; }
        public int Speed { get; set; }
        public int HPMax { get; set; }
        public int HP { get; set; }
    }
}

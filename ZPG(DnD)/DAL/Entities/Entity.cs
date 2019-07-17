using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public abstract class Entity
    {
        public abstract string Name { get; set; }
        public abstract int ArmorClass { get; set; }
        public abstract int Intitiative { get; set; }
        public abstract int Speed { get; set; }
        public abstract int HPMax { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    class CreateCharacterModel
    {
        public string Name { get; set; }
        public int ArmorClass { get; set; }
        public int Intitiative { get; set; }
        public int Speed { get; set; }
        public int HPMax { get; set; }
        public int HP { get; set; }
    }
}

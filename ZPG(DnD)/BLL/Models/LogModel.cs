using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class LogModel
    {
        public string returnModel { get; set; }
        public int enemyHP { get; set; }
        public bool Looted { get; set; }
        public bool enemyCreated { get; set; }
    }
}

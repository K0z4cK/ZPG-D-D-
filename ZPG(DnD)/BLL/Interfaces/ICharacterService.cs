using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICharacterService
    {
        int Walk();
        int Explore();
        int Fight();
        int TryToSpeak();
        int Pray();
        int Steal();
        int Sneak();

    }
}

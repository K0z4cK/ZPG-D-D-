using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICharacterService
    {
        bool Create(CreateCharacterModel);
        bool Pick();
        bool Walk();
        bool Explore();
        bool Fight();
        bool TryToSpeak();
        bool Pray();
        bool Steal();
        bool Sneak();
        bool Die();
        bool CheckSituation();

    }
}

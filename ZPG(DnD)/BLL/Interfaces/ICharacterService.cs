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
        IEnumerable<CreateCharacterModel> GetCharactersByUserId(int userId);
        int Create(CreateCharacterModel character, int userId);
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

using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BLL.Interfaces
{
    public interface ICharacterService
    {
        IEnumerable<CreateCharacterModel> GetCharactersByUserId(int userId);
        int Create(CreateCharacterModel character, CreateCharacterStats stats, CreateCharacterSkills skills, int userId);
        int SetCharacter(CreateCharacterModel character, CreateCharacterSkills skills, CreateCharacterStats stats);
        int Delete(CreateCharacterModel character, int userId);
        bool Pick();
        bool Walk();
        bool GoToCity();
        bool Explore();
        bool Fight(CreateCharacterModel character, ListView log);
        bool TryToSpeak();
        bool Pray();
        bool Steal();
        bool Sneak();
        bool Die();
        bool CheckSituation(CreateCharacterModel character, ListView log);

    }
}

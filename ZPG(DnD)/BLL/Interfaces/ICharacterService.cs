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
        string Walk(CreateCharacterModel character);
        string GoToCity();
        bool Explore();
        string Fight(CreateCharacterModel character);
        bool TryToSpeak();
        bool Pray();
        bool Steal();
        bool Sneak();
        string Die();
        string CheckSituation(CreateCharacterModel character);

    }
}

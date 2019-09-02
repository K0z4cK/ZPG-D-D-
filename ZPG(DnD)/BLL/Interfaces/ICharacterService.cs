using BLL.Models;
using DAL.Entities;
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
        string GoToCity(CreateCharacterModel character);
        bool Explore();
        string Fight(CreateCharacterModel character);
        string Loot(CreateCharacterModel character, ICollection<CharacterItem> charInventory);
        bool TryToSpeak();
        bool Pray();
        bool Steal();
        bool Sneak();
        string Die(CreateCharacterModel character);
        string CheckSituation(CreateCharacterModel character, ICollection<CharacterItem> charInventory);

    }
}

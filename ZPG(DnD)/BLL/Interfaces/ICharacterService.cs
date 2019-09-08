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
        LogModel Pick();
        LogModel Walk(CreateCharacterModel character);
        LogModel GoToCity(CreateCharacterModel character);
        bool Explore();
        LogModel Fight(CreateCharacterModel character);
        LogModel Loot(CreateCharacterModel character, ICollection<CharacterItem> charInventory);
        LogModel TryToSpeak(CreateCharacterModel character, CreateCharacterSkills skills, CreateCharacterStats stats);
        bool Pray();
        bool Steal();
        bool Sneak();
        LogModel Die(CreateCharacterModel character);
        LogModel CheckSituation(CreateCharacterModel character, ICollection<CharacterItem> charInventory);

    }
}

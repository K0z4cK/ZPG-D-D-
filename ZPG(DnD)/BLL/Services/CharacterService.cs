using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly EFContext _context = new EFContext();
        private readonly IZPGRepository<Character> _characterRepository;
        private readonly IZPGRepository<CharacterSkills> _skillsRepository;
        private readonly IZPGRepository<CharacterStats> _statsRepository;
        private readonly IZPGRepository<CharacterInventory> _inventoryRepository;
        private readonly IZPGRepository<CharacterItem> _characterItemRepository;

        public CharacterService()
        {
            _characterRepository = new CharacterRepository(_context);
            _skillsRepository = new CharacterSkillsRepository(_context);
            _statsRepository = new CharacterStatsRepository(_context);
            _inventoryRepository = new CharacterInventoryRepository(_context);
            _characterItemRepository = new CharacterItemRepository(_context);
        }
        public IEnumerable<CreateCharacterModel> GetCharactersByUserId(int userId)
        {
            return _characterRepository.Get().
                  Where(t => t.UserId == userId).
                  Select(t => new CreateCharacterModel()
                  {
                      ArmorClass = t.ArmorClass,
                      Name = t.Name,
                      HP= t.HP,
                      HPMax=t.HPMax,
                      Intitiative=t.Intitiative,
                      Speed = t.Speed
                  }
              );
        }
        public bool CheckSituation()
        {
            throw new NotImplementedException();
        }

        public int Create(CreateCharacterModel character, CreateCharacterStats stats, CreateCharacterSkills skills, int userId)
        {
            Random random = new Random();
            character.HPMax = random.Next(150);
            character.HP = character.HPMax;
            character.Intitiative = random.Next(-6, 10);
            character.Speed = random.Next(50);
            character.ArmorClass = random.Next(40);
            character.Coins = 0;
            character.Level = 1;
            character.Exp = 0;

            stats.Charisma = random.Next(-4, 6);
            stats.Constitution = random.Next(-4, 6);
            stats.Dexterity = random.Next(-4, 6);
            stats.Intelligence = random.Next(-4, 6);
            stats.Strength = random.Next(-4, 6);
            stats.Wisdom = random.Next(-4, 6);

            skills.Acrobatics = random.Next(-2, 4);
            skills.AnimalHandling = random.Next(-2, 4);
            skills.Athletics = random.Next(-2, 4);
            skills.Medicine = random.Next(-2, 4);
            skills.Persuasion = random.Next(-2, 4);
            skills.Religion = random.Next(-2, 4);
            skills.SleightOfHand = random.Next(-2, 4);
            skills.Stealth = random.Next(-2, 4);
            skills.Survival = random.Next(-2, 4);

            _characterRepository.Add(new Character()
            {
                Name = character.Name,
                HPMax = character.HPMax,
                HP = character.HP,
                Race = character.Race,
                Class = character.Class,
                Aligment = character.Aligment,
                Background = character.Background,
                Intitiative = character.Intitiative,
                Speed = character.Speed,
                ArmorClass = character.ArmorClass,
                Coins = character.Coins,
                Level = character.Level,
                Exp = character.Exp,
                UserId = userId

            });

            _skillsRepository.Add(new CharacterSkills()
            {
                Id = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id,
                Acrobatics = skills.Acrobatics,
                AnimalHandling = skills.AnimalHandling,
                Athletics = skills.Athletics,
                Medicine = skills.Medicine,
                Persuasion = skills.Persuasion,
                Religion = skills.Religion,
                SleightOfHand = skills.SleightOfHand,
                Stealth = skills.Stealth,
                Survival = skills.Survival
            });
            _statsRepository.Add(new CharacterStats()
            {
                Id = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id,
                Charisma = stats.Charisma,
                Constitution = stats.Constitution,
                Dexterity = stats.Dexterity,
                Intelligence = stats.Intelligence,
                Strength = stats.Strength,
                Wisdom = stats.Wisdom
            });
            _inventoryRepository.Add(new CharacterInventory()
            {
                Id = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id,
            });
            for(int i = 1; i <= 7; i++) 
                _characterItemRepository.Add(new CharacterItem()
                {
                    ItemId = i,
                    InventoryId = _characterRepository.Get().FirstOrDefault(
                    u => u.Name == character.Name).Id
                });
            

            return _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id;
        }
        public int Delete(CreateCharacterModel character, int userId)
        {
            int characterId = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id;

            _inventoryRepository.Delete(_inventoryRepository.Get().FirstOrDefault(u => u.Id == characterId).Id);

            _statsRepository.Delete(_statsRepository.Get().FirstOrDefault(
                u => u.Id == characterId).Id);

            _skillsRepository.Delete(_skillsRepository.Get().FirstOrDefault(
                u => u.Id == characterId).Id);

            _characterRepository.Delete(_characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id);

            return characterId;
        }
        public bool Die()
        {
            throw new NotImplementedException();
        }

        public bool Explore()
        {
            throw new NotImplementedException();
        }

        public bool Fight()
        {
            throw new NotImplementedException();
        }

        public bool Pick()
        {
            throw new NotImplementedException();
        }

        public bool Pray()
        {
            throw new NotImplementedException();
        }

        public bool Sneak()
        {
            throw new NotImplementedException();
        }

        public bool Steal()
        {
            throw new NotImplementedException();
        }

        public bool TryToSpeak()
        {
            throw new NotImplementedException();
        }

        public bool Walk()
        {
            throw new NotImplementedException();
        }
    }
}

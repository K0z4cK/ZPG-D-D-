using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        private readonly IZPGRepository<Enemy> _EnemyRepository;
        private readonly IZPGRepository<EnemyInventory> _enemyInventoryRepository;
        private readonly IZPGRepository<EnemyItem> _enemyItemRepository;
        private CreateEnemyModel enemy;
        public CharacterService()
        {
            _characterRepository = new CharacterRepository(_context);
            _skillsRepository = new CharacterSkillsRepository(_context);
            _statsRepository = new CharacterStatsRepository(_context);
            _inventoryRepository = new CharacterInventoryRepository(_context);
            _characterItemRepository = new CharacterItemRepository(_context);
            _EnemyRepository = new EnemyRepository(_context);
            _enemyInventoryRepository = new EnemyInventoryRepository(_context);
            _enemyItemRepository = new EnemyItemRepository(_context);
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
            character.isFighting = false;

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
        public int SetCharacter(CreateCharacterModel character, CreateCharacterSkills skills, CreateCharacterStats stats)
        {
            var characterRepository = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name);

            int characterId = characterRepository.Id;

            var SkillsTemp = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId);
            var StatsTemp = _statsRepository.Get().FirstOrDefault(u => u.Id == characterId);

            character.Aligment = characterRepository.Aligment;
            character.Background = characterRepository.Background;
            character.Class = characterRepository.Class;
            character.Race = characterRepository.Race;

            skills.Acrobatics = SkillsTemp.Acrobatics;
            skills.AnimalHandling = SkillsTemp.AnimalHandling;
            skills.Athletics = SkillsTemp.Athletics;
            skills.Medicine = SkillsTemp.Medicine;
            skills.Persuasion = SkillsTemp.Persuasion;
            skills.Religion = SkillsTemp.Religion;
            skills.SleightOfHand = SkillsTemp.SleightOfHand;
            skills.Stealth = SkillsTemp.Stealth;
            skills.Survival = SkillsTemp.Survival;

            stats.Charisma = StatsTemp.Charisma;
            stats.Constitution = StatsTemp.Constitution;
            stats.Dexterity = StatsTemp.Dexterity;
            stats.Intelligence = StatsTemp.Intelligence;
            stats.Strength = StatsTemp.Strength;
            stats.Wisdom = StatsTemp.Wisdom;
            return characterId;
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
        public string CheckSituation(CreateCharacterModel character)
        {
            if (!character.isFighting && character.HP > 1)
                return Walk(character);
            else if (character.isFighting)
                return Fight(character);
            else if (!character.isFighting && character.HP == 1)
                return GoToCity();
            else return Die();
        }

        public string Die()
        {
            return "You Died";
        }

        public bool Explore()
        {
            throw new NotImplementedException();
        }

        public string Fight(CreateCharacterModel character)
        {
            if (character.HP <= 0 || enemy.HP <= 0)
            {
                character.isFighting = false;
                if (enemy.HP <= 0 && character.HP > 0)
                {
                    character.Exp += enemy.ExpGained;
                    return "You killed " + enemy.Name;
                }
                return Die();
            }
            Random random = new Random();
            CreateCharacterSkills skills = new CreateCharacterSkills();
            CreateCharacterStats stats = new CreateCharacterStats();
            int characterId = SetCharacter(character, skills, stats);
            var charInventory = _inventoryRepository.Get().FirstOrDefault(u => u.Id == characterId).CharacterItems;
            int WeaponId = 0;
            foreach (var item in charInventory)
                if (item.ItemOf.TypeOfItem == typeOfItem.Weapon && item.ItemOf.isDressed == true)
                    WeaponId = item.ItemId;
            var charWeapon = charInventory.FirstOrDefault(u => u.ItemId == WeaponId).ItemOf;
            
            
            
            int charInit = random.Next(character.Intitiative, 11);
            int charHit = random.Next(skills.SleightOfHand + stats.Strength + charWeapon.equipmentBonus);
            int enemInit = random.Next(enemy.Intitiative, 11);
            int enemyHit = random.Next((enemy.HPMax + enemy.HP) / 4);

            
            
            if (charInit < enemInit)
            {
                character.HP -= enemyHit;
                return enemy.Name + " hit you by " + enemyHit.ToString();
            }
            else
            {
                enemy.HP -= charHit;
                return "You hit " + enemy.Name + " by " + charHit.ToString();
            }
            

            /*if (character.HP > 0)
                return Walk();
            else return Die();*/
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

        public string Walk(CreateCharacterModel character)
        {
            Random random = new Random();
            int enemyId = random.Next(_EnemyRepository.Get().Last().Id);
            if (enemyId == 2)
                enemyId--;
            if (enemyId == 0)
                enemyId++;
            var EnemyTemp = _EnemyRepository.Get().FirstOrDefault(u => u.Id == enemyId);
            
            enemy = new CreateEnemyModel()
            {
                ArmorClass = EnemyTemp.ArmorClass,
                ExpGained = EnemyTemp.ExpGained,
                HP = EnemyTemp.HP,
                HPMax = EnemyTemp.HPMax,
                Intitiative = EnemyTemp.Intitiative,
                Name = EnemyTemp.Name,
                IsBoss = EnemyTemp.IsBoss,
                Speed = EnemyTemp.Speed
            };
            character.isFighting = true;
            return "You meet " + enemy.Name + " and he look realy agressive";
        }

        public string GoToCity()
        {
            return "You Go To City";
        }

        
    }
}

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
        private readonly IZPGRepository<Character> _characterRepository;
        private readonly IZPGRepository<CharacterSkills> _skillsRepository;
        private readonly IZPGRepository<CharacterStats> _statsRepository;
        private readonly IZPGRepository<CharacterInventory> _inventoryRepository;
        private readonly IZPGRepository<CharacterItem> _characterItemRepository;
        private readonly IZPGRepository<Enemy> _EnemyRepository;
        private readonly IZPGRepository<EnemyInventory> _enemyInventoryRepository;
        private readonly IZPGRepository<EnemyItem> _enemyItemRepository;
        private CreateEnemyModel enemy;
        private ICollection<EnemyItem> enemyInventory;
        private ICollection<CharacterItem> _charInventory;
        private LogModel logModel = new LogModel();
        private bool enemyLooted = true;
        private int _characterId;
        public CharacterService(EFContext _context)
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
        public LogModel CheckSituation(CreateCharacterModel character, ICollection<CharacterItem> charInventory)
        {
            _charInventory = charInventory;
            if (!character.isFighting && character.HP > 1 && enemyLooted)
                return Walk(character);
            else if (character.isFighting)
                return Fight(character);
            else if (!character.isFighting && !enemyLooted)
                return Loot(character, charInventory);
            else if (!character.isFighting && character.HP == 1)
                return GoToCity(character);

            else return Die(character);
        }

        public LogModel Die(CreateCharacterModel character)
        {
            logModel.enemyHP = enemy.HP;
            logModel.Looted = false;
            logModel.enemyCreated = false;
            logModel.returnModel = character.Name + " Died";
            return logModel;
        }

        public bool Explore()
        {
            throw new NotImplementedException();
        }

        public LogModel Fight(CreateCharacterModel character)
        {
            logModel.enemyHP = enemy.HP;
            logModel.Looted = false;
            logModel.enemyCreated = false;

            if (character.HP <= 0 || enemy.HP <= 0)
            {
                character.isFighting = false;
                if (enemy.HP <= 0 && character.HP > 0)
                {
                    character.Exp += enemy.ExpGained;
                    logModel.returnModel = character.Name+" killed " + enemy.Name;
                    return logModel;
                }
                return Die(character);
            }
            Random random = new Random();
            CreateCharacterSkills skills = new CreateCharacterSkills();
            CreateCharacterStats stats = new CreateCharacterStats();
            _characterId = SetCharacter(character, skills, stats);
            int WeaponId = 0;
            foreach (var item in _charInventory)
                if (item.ItemOf.TypeOfItem == typeOfItem.Weapon && item.isDressed == true)
                    WeaponId = item.ItemId;
             var charWeapon = _charInventory.FirstOrDefault(u => u.ItemId == WeaponId)?.ItemOf;
            
            
            // change strength and another, to more stuff
            int charInit = random.Next(character.Intitiative, 11);
            int charHit = random.Next((stats.Strength / skills.SleightOfHand)*3+(skills.SleightOfHand + stats.Strength) + charWeapon.equipmentBonus + character.Speed/9);
            int charArm = random.Next(character.ArmorClass/3+stats.Constitution);
            int enemInit = random.Next(enemy.Intitiative, 11);
            int enemyHit = random.Next((enemy.HPMax + enemy.HP) / 4 + enemy.Speed/9);
            int enemyArm = random.Next(enemy.ArmorClass);

            
            

            if (charInit < enemInit)
            {

                if (enemyHit > charArm)
                {
                    character.HP -= enemyHit;
                    logModel.returnModel = enemy.Name + " hit " + character.Name + " by " + enemyHit.ToString();
                }
                else logModel.returnModel = enemy.Name + " tryed to attack but "+ character.Name + "'s determination is stronger";
            }
            else
            {
                if (charHit > enemyArm)
                {
                    enemy.HP -= charHit;
                    logModel.returnModel = character.Name+" hit " + enemy.Name + " by " + charHit.ToString();
                }
                else logModel.returnModel = character.Name+" just started to attack and his " + charWeapon.Name + " flew out of hand"; 
            }
            return logModel;

            /*if (character.HP > 0)
                return Walk();
            else return Die();*/
        }
        public LogModel Loot(CreateCharacterModel character, ICollection<CharacterItem> charInventory)
        {
            List<string> lootedThings = new List<string>();
            foreach (var item in enemyInventory)
            {
                _characterItemRepository.Add(new CharacterItem()
                {
                    ItemId = item.ItemId,
                    InventoryId = _characterId
                });
                lootedThings.Add(item.ItemOf.Name);

            }
            string returnString = character.Name + " looted from " + enemy.Name + ":";
            enemyLooted = true;
            foreach (var i in lootedThings)
                returnString +=( " " + i);

            logModel.enemyHP = enemy.HP;
            logModel.Looted = true;
            logModel.enemyCreated = false;
            logModel.returnModel = returnString;

            return logModel;
        }

        public LogModel Pick()
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

        public LogModel Walk(CreateCharacterModel character)
        {
            Random random = new Random();
            int enemyId = random.Next((_EnemyRepository.Get().Last().Id)+1);
            if (enemyId == 2)
                enemyId--;
            if (enemyId == 0)
                enemyId++;
            var EnemyTemp = _EnemyRepository.Get().FirstOrDefault(u => u.Id == enemyId);
            enemyInventory = _enemyInventoryRepository.Get().FirstOrDefault(u => u.Id == enemyId).EnemyItems;
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
            enemyLooted = false;
            character.isFighting = true;

            logModel.enemyHP = enemy.HP;
            logModel.Looted = false;
            logModel.enemyCreated = true;
            logModel.returnModel = character.Name + " meet " + enemy.Name + " and he look realy agressive";

            return logModel;
        }

        public LogModel GoToCity(CreateCharacterModel character)
        {
            logModel.enemyHP = enemy.HP;
            logModel.Looted = false;
            logModel.enemyCreated = false;
            logModel.returnModel = character.Name + " Going To City";
            return logModel;
        }

        
    }
}

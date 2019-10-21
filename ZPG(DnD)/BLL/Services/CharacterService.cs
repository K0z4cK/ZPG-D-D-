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
        private readonly IZPGRepository<Item> _ItemRepository;
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
            _ItemRepository = new ItemRepository(_context);
        }
        public IEnumerable<CreateCharacterModel> GetCharactersByUserId(int userId)
        {
            return _characterRepository.Get().
                  Where(t => t.UserId == userId).
                  Select(t => new CreateCharacterModel()
                  {
                      ArmorClass = t.ArmorClass,
                      Level = t.Level,
                      Exp = t.Exp,
                      Coins = t.Coins,
                      Aligment = t.Aligment,
                      Background= t.Background,
                      Class = t.Class,
                      Race= t.Race,
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
            character.HPMax = random.Next(30,150);
            character.HP = character.HPMax;
            character.Intitiative = random.Next(-6, 10);
            character.Speed = random.Next(50);
            character.ArmorClass = random.Next(40);
            character.Coins = 0;
            character.Level = 1;
            character.Exp = 0;
            character.isFighting = false;

            stats.Charisma = random.Next(-4, 7);
            stats.Constitution = random.Next(-4, 7);
            stats.Dexterity = random.Next(-4, 7);
            stats.Intelligence = random.Next(-4, 7);
            stats.Strength = random.Next(-4, 7);
            stats.Wisdom = random.Next(-4, 7);

            skills.Acrobatics = random.Next(-2, 5);
            skills.AnimalHandling = random.Next(-2, 5);
            skills.Athletics = random.Next(-2, 5);
            skills.Medicine = random.Next(-2, 5);
            skills.Persuasion = random.Next(-2, 5);
            skills.Religion = random.Next(-2, 5);
            skills.SleightOfHand = random.Next(-2, 5);
            skills.Stealth = random.Next(-2, 5);
            skills.Survival = random.Next(-2, 5);

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
            if (character.Exp >= character.Level * 12.5)
                return LevelUp(character);
            if (!character.isFighting && character.HP > 1 && enemyLooted)
                return Walk(character, charInventory);
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
            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
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
            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
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

            int charPower = skills.SleightOfHand * 2 + stats.Strength;
            if (charPower <= 0)
                charPower = 1;

            int charEloquence = skills.Persuasion * 2 + stats.Charisma;
            if (charEloquence <= 0)
                charEloquence = 1;

            int sleightOfHand = random.Next(charPower);
            int persuasion = random.Next(charEloquence);
           
            
            // change strength and another, to more stuff
            int charInit = random.Next(character.Intitiative, 11);
            int charHit = (stats.Strength / skills.SleightOfHand) + (skills.SleightOfHand + stats.Strength) * 3 + charWeapon.equipmentBonus + character.Speed / 8;
            
            int charArm = random.Next(character.ArmorClass/3+stats.Constitution);
            int enemInit = random.Next(enemy.Intitiative, 11);
            int enemyHit = random.Next((enemy.HPMax + enemy.HP) / 4 + enemy.Speed/9);
            int enemyArm = random.Next(enemy.ArmorClass);

            if (charHit <= 0)
                charHit = 1;
            else
                charHit = random.Next(charHit);



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
                if (persuasion > sleightOfHand && enemy.HP == enemy.HPMax)
                {
                    return TryToSpeak(character, skills, stats);
                }
                if (charHit > enemyArm)
                {
                    enemy.HP -= charHit;
                    logModel.enemyHP = enemy.HP;
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
                returnString +=( " " + i+",");

            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
            logModel.enemyHP = enemy.HP;
            logModel.Looted = true;
            logModel.enemyCreated = false;
            logModel.returnModel = returnString;

            return logModel;
        }

        public LogModel Pick(CreateCharacterModel character)
        {
            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
            logModel.enemyHP = enemy.HP;
            logModel.Looted = true;
            logModel.enemyCreated = false;
            


            Item pickedItem = new Item();
            Random rnd = new Random();
            int roll = rnd.Next(100);
            if (roll >= 10 && roll < 35)
                pickedItem = _ItemRepository.Get().FirstOrDefault(x => x.Id == 17);
            else if (roll >= 35)
                pickedItem = _ItemRepository.Get().FirstOrDefault(x => x.Id == 1);
            else roll = rnd.Next(_ItemRepository.Get().Last().Id + 1);

            _characterItemRepository.Add(new CharacterItem()
            {
                ItemId = pickedItem.Id,
                InventoryId = _characterId
            });

            logModel.returnModel = character.Name + " found " + pickedItem.Name + " on the way";

            return logModel;
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

        public LogModel TryToSpeak(CreateCharacterModel character, CreateCharacterSkills skills, CreateCharacterStats stats)
        {
            Random random = new Random();

            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
            logModel.enemyHP = enemy.HP;
            logModel.Looted = false;
            logModel.enemyCreated = false;
            logModel.returnModel = character.Name + " trying to speak with " + enemy.Name + "\n";
            int charSpeak = random.Next(((stats.Charisma + 2) / (skills.Persuasion + 1)) + (skills.Persuasion + stats.Charisma) * 5);
            int needToSpeak = ((stats.Charisma + 2) / (skills.Persuasion + 1)) + (skills.Persuasion + stats.Charisma) * 4;
            if (charSpeak >= needToSpeak)
            {
                character.isFighting = false;
                character.Exp += enemy.ExpGained;
                enemyLooted = true;
                logModel.returnModel += character.Name + " sayed to " + enemy.Name + " that he don't want to kill somebody, and "+ enemy.Name + " just listen, and go away";
            }
            else
                logModel.returnModel += enemy.Name + " didn't listen to " + character.Name + " and still want to kill him";
            return logModel;
        }

        public LogModel Walk(CreateCharacterModel character, ICollection<CharacterItem> charInventory)
        {
            Random random = new Random();
            int roll = random.Next(100);

            if (character.HP <= character.HPMax / 2)
                if (charInventory.FirstOrDefault(x => x.Id == 17) != default)
                    return Heal(character, charInventory);

            if (roll < 20 && enemy != null) 
                return Pick(character);
            else
            {
                int enemyId = random.Next(_EnemyRepository.Get().Last().Id + 1);
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

                logModel.enemyName = enemy.Name;
                logModel.enemyMaxHP = enemy.HPMax;
                logModel.enemyHP = enemy.HP;
                logModel.Looted = false;
                logModel.enemyCreated = true;
                logModel.returnModel = character.Name + " meet " + enemy.Name + " and he look realy agressive";
            }
            return logModel;
        }

        public LogModel GoToCity(CreateCharacterModel character)
        {
            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
            logModel.enemyHP = enemy.HP;
            logModel.Looted = false;
            logModel.enemyCreated = false;
            logModel.returnModel = character.Name + " Going To City";
            return logModel;
        }

        public LogModel LevelUp(CreateCharacterModel character)
        {
            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
            logModel.enemyHP = enemy.HP;
            logModel.Looted = false;
            logModel.enemyCreated = false;
            character.Level++;
            if (character.Level % 5 == 1)
                character.HPMax += character.Exp / 5;
            logModel.returnModel = character.Name + " has level up to "+character.Level;
            return logModel;
            
        }

        public LogModel Heal(CreateCharacterModel character, ICollection<CharacterItem> charInventory)
        {
            logModel.enemyName = enemy.Name;
            logModel.enemyMaxHP = enemy.HPMax;
            logModel.enemyHP = enemy.HP;
            logModel.Looted = true;
            logModel.enemyCreated = false;
                charInventory.Remove(charInventory.FirstOrDefault(x => x.Id == 17));
                _characterItemRepository.Delete(17);
                character.HP += 50;
                if (character.HP > character.HPMax)
                    character.HP = character.HPMax;
                logModel.returnModel = character.Name + " Use Healing Potion";
                return logModel;
        }
    }
}

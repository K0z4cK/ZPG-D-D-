using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ZPG_DnD_
{
    /// <summary>Skills
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EFContext _context = new EFContext();
        private readonly ICharacterService _characterService;
        private readonly IZPGRepository<Character> _characterRepository;
        private readonly IZPGRepository<CharacterSkills> _skillsRepository;
        private readonly IZPGRepository<CharacterStats> _statsRepository;
        private readonly IZPGRepository<CharacterInventory> _inventoryRepository;
        private readonly IZPGRepository<CharacterItem> _characterItemRepository;
        private DispatcherTimer timer;
        private CreateCharacterModel _character;
        private ICollection<CharacterItem> _charInventory;
        private int _characterId;
        public MainWindow(CreateCharacterModel character, string username)
        {
            _character = character;
            _characterService = new CharacterService(_context);
            _characterRepository = new CharacterRepository(_context);
            _skillsRepository = new CharacterSkillsRepository(_context);
            _statsRepository = new CharacterStatsRepository(_context);
            _inventoryRepository = new CharacterInventoryRepository(_context);
            _characterItemRepository = new CharacterItemRepository(_context);
            InitializeComponent();
            CreateCharacterSkills skills = new CreateCharacterSkills();
            CreateCharacterStats stats = new CreateCharacterStats();

            int characterId = _characterService.SetCharacter(character, skills, stats);
            _characterId = characterId;
            var charInventory = _inventoryRepository.Get().FirstOrDefault(u => u.Id == characterId).CharacterItems;

            _charInventory = charInventory;

            //this.DataContext = character;
            userName.Text = username;
            characterName.Text = character.Name;
            classNlevel.Text = character.Class.ToString() + " " + character.Level.ToString();
            race.Text = character.Race.ToString();
            aligment.Text = character.Aligment.ToString();
            background.Text = character.Background.ToString();
            armorClassNum.Text = character.ArmorClass.ToString();
            initiativeNum.Text = character.Intitiative.ToString();
            healthPoints.Text = character.HP.ToString() + "/" + character.HPMax.ToString();
            speedNum.Text = character.Speed.ToString();
            experiencePt.Text = character.Exp.ToString();

            acrobatics.Text = "Acrobatics    " + skills.Acrobatics.ToString();
            animalHandling.Text = "Animal Handling    " + skills.AnimalHandling.ToString();
            athletics.Text = "Athletics    " + skills.Athletics.ToString();
            medicine.Text = "Medicine    " + skills.Medicine.ToString();
            persuasion.Text = "Persuasion    " + skills.Persuasion.ToString();
            religion.Text = "Religion    " + skills.Religion.ToString();
            sleightOfHand.Text = "Sleight of hand    " + skills.SleightOfHand.ToString();
            stealth.Text = "Stealth    " + skills.Stealth.ToString();
            survival.Text = "Survival    " + skills.Survival.ToString();

            charismaNum.Text = stats.Charisma.ToString();
            constitutionNum.Text = stats.Constitution.ToString();
            dexterityNum.Text = stats.Dexterity.ToString();
            intelligenceNum.Text = stats.Intelligence.ToString();
            strengthNum.Text = stats.Strength.ToString();
            wisdomNum.Text = stats.Wisdom.ToString();

            coins.Text = "Coins: " + character.Coins.ToString();
            /*_characterItemRepository.Add(new CharacterItem()
            {
                ItemId = 1,
                InventoryId = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id

            });*/
            //LVinventory.Items.Add(_inventoryRepository.Get().FirstOrDefault(u => u.Id == characterId).CharacterItems.FirstOrDefault().ItemOf.Name);
            setEquipment();
            setInventory();

            //таймер
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            //інтервал
            timer.Interval = new TimeSpan(0, 0, 1);

            // запуск таймера
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //_charInventory = null;
            //_charInventory = _inventoryRepository.Get().FirstOrDefault(u => u.Id == _characterId).CharacterItems;
            
            LogModel situation = _characterService.CheckSituation(_character, _charInventory);
            //progressBar.Value = 50;
            Log.Items.Add(situation.returnModel);
            do {
                progressBar.Value += 20;
                progressBar.IsIndeterminate = true;
                Thread.Sleep(1000);
            } while (progressBar.Value < progressBar.Maximum);
            progressBar.Value = 0;
            //progressBar.
            //Log.ScrollIntoView(Log.Items[Log.Items.Count - 2]);
            Log.ScrollIntoView(Log.Items[Log.Items.Count - 1]);
            Log.SelectedItem = Log.Items[Log.Items.Count - 1];
            healthPoints.Text = _character.HP.ToString() + "/" + _character.HPMax.ToString();
            enemyHealthPoints.Text = situation.enemyHP.ToString() + "/" + situation.enemyMaxHP.ToString();
            enemyName.Text = situation.enemyName+" HP";
            experiencePt.Text = _character.Exp.ToString();
            if (situation.Looted)
            {
                setEquipment();
                setInventory();
            }
            if (situation.returnModel == _character.Name + " Died")
                timer.Stop();
            Log.ScrollIntoView(Log.Items[Log.Items.Count - 1]);
            Log.ScrollIntoView(Log.Items[Log.Items.Count - 1]);
        }

        public void setInventory()
        {
            LVinventory.Items.Clear();
            foreach (var item in _charInventory)
            {
                int numOfItems = 0;
                bool isAlreadyPresent = false;

                foreach (var i in _charInventory)
                    if (i.ItemId == item.ItemId)
                        numOfItems++;

                foreach (var i in LVinventory.Items)
                    if (i.ToString() == item.ItemOf.Name + "   x" + numOfItems.ToString())
                        isAlreadyPresent = true;

                if (!isAlreadyPresent && !item.isDressed)
                    LVinventory.Items.Add(item.ItemOf.Name + "   x" + numOfItems.ToString());
            }
        }
        public void setEquipment()
        {
            foreach (var item in _charInventory)
            {
                if (item.ItemOf.TypeOfItem != typeOfItem.Trash)
                {
                    bool isNeedToWear = true;
                    typeOfItem type = item.ItemOf.TypeOfItem;
                    foreach (var i in _charInventory)
                    {
                        if (i.ItemOf.TypeOfItem == type && i.isDressed == true)
                            isNeedToWear = false;
                        if (i.ItemOf.TypeOfItem == type && i.isDressed == true && item.isDressed == false && i.ItemOf.equipmentBonus < item.ItemOf.equipmentBonus)
                            isNeedToWear = true;
                    }
                    if (isNeedToWear)
                    {
                        foreach (var i in _charInventory)
                            if (i.ItemOf.TypeOfItem == type && i.isDressed == true)
                                i.isDressed = false;
                        switch (type)
                        {
                            case typeOfItem.Helmet:
                                head.Text = "Head: "+item.ItemOf.Name;
                                break;
                            case typeOfItem.Armor:
                                armor.Text = "Armor: "+item.ItemOf.Name;
                                break;
                            case typeOfItem.Gloves:
                                gloves.Text = "Gloves: "+item.ItemOf.Name;
                                break;
                            case typeOfItem.Boots:
                                boots.Text = "Boots: "+item.ItemOf.Name;
                                break;
                            case typeOfItem.Weapon:
                                weapon.Text = "Weapon: "+item.ItemOf.Name;
                                break;
                            case typeOfItem.Amulet:
                                amulet.Text = "Amulet: "+item.ItemOf.Name;
                                break;
                            default:
                                break;
                        }
                        item.isDressed = true;

                    }
                    else if (item.isDressed == true)
                        switch (type)
                        {
                            case typeOfItem.Helmet:
                                head.Text = "Head: " + item.ItemOf.Name;
                                break;
                            case typeOfItem.Armor:
                                armor.Text = "Armor: " + item.ItemOf.Name;
                                break;
                            case typeOfItem.Gloves:
                                gloves.Text = "Gloves: " + item.ItemOf.Name;
                                break;
                            case typeOfItem.Boots:
                                boots.Text = "Boots: " + item.ItemOf.Name;
                                break;
                            case typeOfItem.Weapon:
                                weapon.Text = "Weapon: " + item.ItemOf.Name;
                                break;
                            case typeOfItem.Amulet:
                                amulet.Text = "Amulet: " + item.ItemOf.Name;
                                break;
                            default:
                                break;
                        }
                }
            }
            
            _inventoryRepository.Edit(_characterId, new CharacterInventory() { CharacterItems = _charInventory});
        }
       /* public bool InventoryReset(int id, ICollection<CharacterItem> elem)
        {
            try
            {
                _context.CharInventories.FirstOrDefault(t => t.Id == id).CharacterItems.Clear();








                foreach (var item in elem)
                    _context.CharInventories.FirstOrDefault(t => t.Id == id).CharacterItems.(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }*/
    }
}

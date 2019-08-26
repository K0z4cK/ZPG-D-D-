﻿using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ZPG_DnD_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EFContext _context = new EFContext();
        private readonly IZPGRepository<Character> _characterRepository;
        private readonly IZPGRepository<CharacterSkills> _skillsRepository;
        private readonly IZPGRepository<CharacterStats> _statsRepository;
        private readonly IZPGRepository<CharacterInventory> _inventoryRepository;
        private readonly IZPGRepository<CharacterItem> _characterItemRepository;
        public MainWindow(CreateCharacterModel character, string username)
        {
            _characterRepository = new CharacterRepository(_context);
            _skillsRepository = new CharacterSkillsRepository(_context);
            _statsRepository = new CharacterStatsRepository(_context);
            _inventoryRepository = new CharacterInventoryRepository(_context);
            _characterItemRepository = new CharacterItemRepository(_context);
            InitializeComponent();
            CreateCharacterSkills skills = new CreateCharacterSkills();
            CreateCharacterStats stats = new CreateCharacterStats();
            
            int characterId = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id;

            var charInventory = _inventoryRepository.Get().FirstOrDefault(u => u.Id == characterId).CharacterItems;

            character.Aligment = _characterRepository.Get().FirstOrDefault(u => u.Id == characterId).Aligment;
            character.Background = _characterRepository.Get().FirstOrDefault(u => u.Id == characterId).Background;
            character.Class = _characterRepository.Get().FirstOrDefault(u => u.Id == characterId).Class;
            character.Race = _characterRepository.Get().FirstOrDefault(u => u.Id == characterId).Race;

            skills.Acrobatics = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).Acrobatics;
            skills.AnimalHandling = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).AnimalHandling;
            skills.Athletics = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).Athletics;
            skills.Medicine = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).Medicine;
            skills.Persuasion = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).Persuasion;
            skills.Religion = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).Religion;
            skills.SleightOfHand = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).SleightOfHand;
            skills.Stealth = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).Stealth;
            skills.Survival = _skillsRepository.Get().FirstOrDefault(u => u.Id == characterId).Survival;

            stats.Charisma = _statsRepository.Get().FirstOrDefault(u => u.Id == characterId).Charisma;
            stats.Constitution = _statsRepository.Get().FirstOrDefault(u => u.Id == characterId).Constitution;
            stats.Dexterity = _statsRepository.Get().FirstOrDefault(u => u.Id == characterId).Dexterity;
            stats.Intelligence = _statsRepository.Get().FirstOrDefault(u => u.Id == characterId).Intelligence;
            stats.Strength = _statsRepository.Get().FirstOrDefault(u => u.Id == characterId).Strength;
            stats.Wisdom = _statsRepository.Get().FirstOrDefault(u => u.Id == characterId).Wisdom;
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

            /*_characterItemRepository.Add(new CharacterItem()
            {
                ItemId = 1,
                InventoryId = _characterRepository.Get().FirstOrDefault(
                u => u.Name == character.Name).Id

            });*/
            //LVinventory.Items.Add(_inventoryRepository.Get().FirstOrDefault(u => u.Id == characterId).CharacterItems.FirstOrDefault().ItemOf.Name);
            
            setInventory(charInventory);
            setEquipment(charInventory);
        }
        public void setInventory(ICollection<CharacterItem> charInventory)
        {
            foreach (var item in charInventory)
            {
                int numOfItems = 0;
                bool isAlreadyPresent = false;

                foreach (var i in charInventory)
                    if (i.ItemId == item.ItemId)
                        numOfItems++;

                foreach (var i in LVinventory.Items)
                    if (i.ToString() == item.ItemOf.Name + "   x" + numOfItems.ToString())
                        isAlreadyPresent = true;

                if (!isAlreadyPresent)
                    LVinventory.Items.Add(item.ItemOf.Name + "   x" + numOfItems.ToString());
            }
        }
        public void setEquipment(ICollection<CharacterItem> charInventory)
        {
            foreach (var item in charInventory)
            {
                if (item.ItemOf.TypeOfItem != typeOfItem.Trash)
                {
                    bool isNeedToWear = true;
                    typeOfItem type = item.ItemOf.TypeOfItem;
                    foreach (var i in charInventory)
                        if (i.ItemOf.TypeOfItem == type && i.ItemOf.isDressed == true)
                            isNeedToWear = false;
                    if (isNeedToWear)
                    {
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
                        item.ItemOf.isDressed = true;
                    }
                }
            }
        }
    }
}

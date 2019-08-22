using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
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
using System.Windows.Shapes;
using ZPG_DnD_.Helping;

namespace ZPG_DnD_
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly int _idUser;
        private readonly ICharacterService _characterService;
        private readonly IUserService _userService;
        private readonly EFContext _context = new EFContext();
        private readonly IZPGRepository<Character> _repository;
        public UserWindow(int id)
        {
            _repository = new CharacterRepository(_context);
            _characterService = new CharacterService();
            _userService = new UserService();
            InitializeComponent();
            _idUser = id;
            User.Text = _userService.GetUser(_idUser).Login;
            var characters = _characterService.GetCharactersByUserId(_idUser);
            foreach(var c in characters)
                lvCharacters.Items.Add(c);
        }

        private void Button_Pick_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow;
            if (lvCharacters.SelectedIndex >= 0)
            {
                mainWindow = new MainWindow((CreateCharacterModel)lvCharacters.SelectedItem, User.Text);
                Close();
                mainWindow.ShowDialog();
            }
                

        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            InputBox inputBox = new InputBox();
            BuildCharacterConcept characterConcept = new BuildCharacterConcept();
            int result = -1;
            CreateCharacterModel newCharacter = new CreateCharacterModel();
            CreateCharacterStats newStats = new CreateCharacterStats();
            CreateCharacterSkills newSkills = new CreateCharacterSkills();
            inputBox.ShowDialog();
            characterConcept.ShowDialog();
            if (inputBox.Text.Text == "" || characterConcept.lvRaces.SelectedIndex < 0 || characterConcept.lvClases.SelectedIndex < 0 ||
                characterConcept.lvAlignments.SelectedIndex < 0 || characterConcept.lvBackgrounds.SelectedIndex < 0)
                MessageBox.Show("Error while add character");
            else
            {
                newCharacter.Name = inputBox.Text.Text;
                newCharacter.Race = (Races)characterConcept.lvRaces.SelectedIndex;
                newCharacter.Class = (Clases)characterConcept.lvClases.SelectedIndex;
                newCharacter.Aligment = (Aligments)characterConcept.lvAlignments.SelectedIndex;
                newCharacter.Background = (Backgrounds)characterConcept.lvBackgrounds.SelectedIndex;
                result = _characterService.Create(newCharacter, newStats, newSkills, _idUser);
                lvCharacters.Items.Add(newCharacter);
            }
        }

        private void Button_Dell_Click(object sender, RoutedEventArgs e)
        {
            if (lvCharacters.SelectedIndex >= 0)
            {
                CreateCharacterModel dell = (CreateCharacterModel)lvCharacters.SelectedItem;
                lvCharacters.Items.Remove(lvCharacters.SelectedItem);
                _repository.Delete(_repository.Get().FirstOrDefault(
                u => u.Name == dell.Name).Id);
            }
        }
    }
}

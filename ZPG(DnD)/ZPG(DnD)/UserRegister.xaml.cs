using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
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

namespace ZPG_DnD_
{
    /// <summary>
    /// Логика взаимодействия для UserRegister.xaml
    /// </summary>
    public partial class UserRegister : Window
    {
        private readonly IUserService _userService;
        public UserRegister()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int res = _userService.Register(new UserRegisterModel()
            {
                Login = UserName.Text,
                Password = Password.Password,
                RePassword = RePassword.Password
            });
            if (res > 0)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Login or Password (Password must be 6 or more simbols)");
            }
        }
    }
}

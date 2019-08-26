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
    /// Логика взаимодействия для UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        private readonly IUserService _userService;
        public UserLogin()
        {
            InitializeComponent();
            _userService = new UserService();
        }
        private void Button_Log_Click(object sender, RoutedEventArgs e)
        {
            int res = _userService.Login(new UserLoginModel()
            {
                Login = Login.Text,
                Password = Password.Password
            });
            if (res > 0)
            {
                this.Visibility = Visibility.Hidden;
                UserWindow window = new UserWindow(res);
                if (window.ShowDialog() == true)
                {
                    this.Visibility = Visibility.Visible;
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Error Login or Password");
            }
        }



        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            UserRegister registration = new UserRegister();
            registration.ShowDialog();
        }
    }
}

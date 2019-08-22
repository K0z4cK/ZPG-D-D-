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

namespace ZPG_DnD_.Helping
{
    /// <summary>
    /// Interaction logic for BuildCharacterConcept.xaml
    /// </summary>
    public partial class BuildCharacterConcept : Window
    {
        public BuildCharacterConcept()
        {
            InitializeComponent();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            lvRaces.SelectedIndex = -1;
            lvClases.SelectedIndex = -1;
            lvAlignments.SelectedIndex = -1;
            lvBackgrounds.SelectedIndex = -1;
            Close();
        }

        private void Button_Set_Click(object sender, RoutedEventArgs e)
        {
            if(lvRaces.SelectedIndex >=0 && lvClases.SelectedIndex >= 0 && lvAlignments.SelectedIndex >= 0 && lvBackgrounds.SelectedIndex >= 0)
                Close();
            else MessageBox.Show("Error: Some Settings not setted!");
        }
    }
}

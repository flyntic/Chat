using Microsoft.Win32;
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

namespace WpfAppListUsers.View
{
    /// <summary>
    /// Логика взаимодействия для AboutUser.xaml
    /// </summary>
    public partial class AboutUser : Window
    {
        public AboutUser()
        {
            InitializeComponent();
        }
        private void Avatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                //int i = MainVM.ListUsers.Users.IndexOf(MainVM.SelectUser);

                //MainVM.ListUsers.Users[i].AvatarFileAbsolute = op.FileName;
                //MainVM.SelectUser = MainVM.ListUsers.Users[i];

            }


        }
    }
}

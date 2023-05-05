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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.AspNetCore.SignalR.Client;


namespace WpfAppListUsers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
                      
        }

        // обработчик загрузки окна
        public async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await MainVM.connection.StartAsync();
            }
            catch (Exception ex)
            {
                MainVM.ConnectionState=ex.Message;
                //MessageBox.Show(ex.Message);
            }
        }

        public void AnyWindowDrag(object sender, MouseButtonEventArgs e)
        {
           Application.Current.MainWindow.DragMove();
        }

        private void OnSearchTextChange(object sender, TextChangedEventArgs e)
        {
            MainVM.SearchTextInUsers();
        }
    }
}

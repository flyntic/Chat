using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppListUsers.Model;
using WpfAppListUsers.Infrastructure.Commands;
using WpfAppListUsers.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Media.TextFormatting;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Threading;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Collections.ObjectModel;
//using System.Windows.Forms;
//using Application = System.Windows.Application;

namespace WpfAppListUsers.ViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        #region Commands
        public ActionCommand WindowMinimizeCommand => new ActionCommand(x => Application.Current.MainWindow.AnyWindowMinimize(x, null));
        public ActionCommand WindowMaximizeCommand => new ActionCommand(x => Application.Current.MainWindow.AnyWindowMaximize(x, null));
        public ActionCommand AppCloseCommand  => new ActionCommand(x =>   Application.Current.MainWindow.AppClose());
        public ActionCommand CloseWindowCommand => new ActionCommand(x => Application.Current.MainWindow.AnyWindowClose(x,null));

      
        public ActionCommand AddSearchUserCommand => new ActionCommand(x => AddSearchUserInFriends());
        #endregion

        #region Property
        private ListUsers listUsers;
        public ListUsers ListUsers
        {
            get => listUsers;
            set
            {
                listUsers = value;
                OnPropertyChanged();
            }
        }
       

        private TemplateListUsers templateListUsers=new TemplateListUsers();
        public TemplateListUsers TemplateListUsers
        {
            get
            {
                templateListUsers.Users=new ObservableCollection<TemplateSelectUser>();
                ListUsers.Users.ForEach(u => { if (!LoginUser.FriendList.Friends.Contains(u)) templateListUsers.Users.Add(new TemplateSelectUser(u, null)); });
                return templateListUsers;
            }
            set
            {
                templateListUsers = value;
                OnPropertyChanged();
            }
        }

       
        private TemplateSelectUser templateSearchUser;
        public TemplateSelectUser TemplateSearchUser
        {
            get => templateSearchUser;
            set
            {
                templateSearchUser = value;
                //if (templateSearchUser == null) return;
                //
                //SelectUser = ListUsers.findById(templateSelectUser.Id);
                //TemplateSelectUserMessages = new TemplateListMessages(selectUser, loginUser);

                OnPropertyChanged();
            }
        }

        private User loginUser;
        public User LoginUser
        {
            get => loginUser;
            set
            {
                loginUser = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public SearchViewModel(ListUsers list, User loginUser)
         {
                 listUsers = list;
                 LoginUser = loginUser;
             
         }

        //public void SearchTextInUsers()
        //{
        //    LoginUser = loginUser;//Сброс поиска
        //    if (string.IsNullOrEmpty(SearchText))
        //    {
        //        //LoginUser = loginUser; 
        //      return;
        //    } 
        //    ObservableCollection<TemplateSelectUser> Users = new ObservableCollection<TemplateSelectUser>();

        //    TemplateListSelectUsers.Users.ToList().FindAll(u => u.FirstName.Contains(SearchText) || u.LastName.Contains(SearchText)).ForEach(u=>Users.Add(u));

        //    templateListSelectUsers.Users.Clear();
        //    templateListSelectUsers.Users = Users;
        //    TemplateListSelectUsers = templateListSelectUsers;

        //}
      
        //private void ShowSearchUser()
        //{
        //    var windowSearch = new SearchUser();
        //    windowSearch.DataContext = Application.Current.MainWindow.DataContext;
        //    windowSearch.ShowDialog();
        //    if (windowSearch.DialogResult == true)
        //    {
               
        //        User searchUser=listUsers.Users.FirstOrDefault(u=>u.Id == TemplateSearchUser.Id);

        //        LoginUser?.FriendList.Friends.Add(searchUser);
        //        LoginUser = loginUser; //обновление списка
        //    }
        //}
        private void AddSearchUserInFriends()
        {
            Application.Current.Windows.OfType<SearchUser>().First().DialogResult = true;
        }

        
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}

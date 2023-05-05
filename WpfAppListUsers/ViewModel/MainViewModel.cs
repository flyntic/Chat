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
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public HubConnection connection;
//        public string connectionState="Есть соединение с сервером"; 
        #region Commands
        public ActionCommand WindowMinimizeCommand => new ActionCommand(x => Application.Current.MainWindow.AnyWindowMinimize(x, null));
        public ActionCommand WindowMaximizeCommand => new ActionCommand(x => Application.Current.MainWindow.AnyWindowMaximize(x, null));
        public ActionCommand AppCloseCommand  => new ActionCommand(x =>   Application.Current.MainWindow.AppClose());
        public ActionCommand CloseWindowCommand => new ActionCommand(x => Application.Current.MainWindow.AnyWindowClose(x,null));

      
        public ActionCommand AboutUserCommand => new ActionCommand(x => ShowAboutUser(),x=>(TemplateSelectUser!=null));
        public ActionCommand AddUserWindowCommand => new ActionCommand(x => ShowAddUser(), x => ( TemplateSelectUser?.IsNoFriend ==true));
        public ActionCommand SendMessageCommand => new ActionCommand(async x => await SendMessage(),x=>(TemplateSelectUser !=null) );
        public ActionCommand LogOutCommand => new ActionCommand(x => LogOut());
        public ActionCommand LoginCommand  => new ActionCommand(x => Login());
        public ActionCommand AddUserCommand => new ActionCommand(x => AddUserInFriends()); 
        
        public ActionCommand NewUserOpenDialogCommand => new ActionCommand(x => NewUserWindow());
        public ActionCommand NewRegUserCommand => new ActionCommand(x => RegistrationUser());
        public ActionCommand AboutLoginUserCommand => new ActionCommand(x => ShowAboutLoginUser());

        public ActionCommand SearchUserShowCommand => new ActionCommand(x => ShowSearchUser());
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
        private TemplateListUsers templateListSelectUsers;
        public TemplateListUsers TemplateListSelectUsers
        {
            get => templateListSelectUsers;
            set
            {
                templateListSelectUsers = value;
                templateListSelectUsers.Users.OrderBy(u => u.DateTimeLastMessage); //(Сортировка по дате сообщений)
                OnPropertyChanged();
            }
        }

        //private TemplateListUsers templateListUsers=new TemplateListUsers();
        //public TemplateListUsers TemplateListUsers
        //{
        //    get
        //    {
        //        templateListUsers.Users=new ObservableCollection<TemplateSelectUser>();
        //        ListUsers.Users.ForEach(u => templateListUsers.Users.Add(new TemplateSelectUser(u,null)));
        //        return templateListUsers;
        //    }
        //    set
        //    {
        //        templateListUsers = value;
        //        OnPropertyChanged();
        //    }
        //}

        private TemplateSelectUser templateSelectUser;
        public TemplateSelectUser TemplateSelectUser
        {
            get => templateSelectUser;
            set
            {
                templateSelectUser = value;
                if (templateSelectUser == null) return;

                SelectUser = ListUsers.findById(templateSelectUser.Id);
                TemplateSelectUserMessages = new TemplateListMessages(selectUser,loginUser);
       
                OnPropertyChanged();
            }
        }

        //private TemplateSelectUser templateSearchUser;
        //public TemplateSelectUser TemplateSearchUser
        //{
        //    get => templateSearchUser;
        //    set
        //    {
        //        templateSearchUser = value;
        //        //if (templateSearchUser == null) return;
        //        //
        //        //SelectUser = ListUsers.findById(templateSelectUser.Id);
        //        //TemplateSelectUserMessages = new TemplateListMessages(selectUser, loginUser);

        //        OnPropertyChanged();
        //    }
        //}
        private TemplateListMessages templateSelectUserMessages;
        public TemplateListMessages TemplateSelectUserMessages
        {
            get => templateSelectUserMessages;
            set
            {
                templateSelectUserMessages = value;
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
                User.Login(loginUser);
               
                TemplateListSelectUsers = new TemplateListUsers(loginUser);
                OnPropertyChanged();
            }
        }
        private User selectUser;
        public User SelectUser
        {
            get => selectUser;
            set
            {
                selectUser = value;               
                OnPropertyChanged();
            }
        }
        private User newRegUser;
        public User NewRegUser
        {
            get => newRegUser;
            set
            {
                newRegUser = value;
                OnPropertyChanged();
            }
        }

        private string newText;
        public string NewText
        {
            get => newText;
            set
            {
                newText= value;
                OnPropertyChanged();
            }

        }
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }

        }

        private string connectionState;
        public string ConnectionState
        {
            get => connectionState;
            set
            {
                connectionState = value;
                OnPropertyChanged();
            }

        }
        private string loginName;

        public string LoginName
        { 
          get=> loginName;
            set 
            {
                loginName = value;
                OnPropertyChanged();
            }
        }
        private string loginPassword;

        public string LoginPassword
        {
            get => loginPassword;
            set
            {
                loginPassword = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public MainViewModel()
            {
              listUsers = new ListUsers();
              for (int i = 0; i < 10; i++)
              {
                  listUsers.Users.Add(new User() { FirstName = $"Name{i}" , LastName = $"Last{i}" , 
                                             Phone = $"8912345678{i}", AvatarFile = $"file{i}.png",
                                             Messages=new ObservableCollection<Message>(),
                                             FriendList=new FriendList() { Friends=new List<User>()},
                                             LoginName=i.ToString(), Password=i.ToString()});
                 
                  for (int j = 0; j < i; j++)
                  {
                      listUsers.Users[i].FriendList.Friends.Add(  listUsers.Users[j]);                
                      listUsers.Users[j].Messages.Add(new Message(listUsers.Users[i], listUsers.Users[j], $"Message from {j} to {i}" ));
                      listUsers.Users[i].Messages.Add(new Message(listUsers.Users[i], listUsers.Users[j], $"Message from {j} to {i}" ));
                      listUsers.Users[j].Messages.Add(new Message(listUsers.Users[j], listUsers.Users[i], $"Message from {i} to {j}" ));
                      listUsers.Users[i].Messages.Add(new Message(listUsers.Users[j], listUsers.Users[i], $"Message from {i} to {j}" ));
                  }
              }
            ConnectionState = "Попытка подключения";
        // создаем подключение к хабу
        connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7019/chat")
                .Build();


            // регистрируем функцию Receive для получения данных
            connection.On<string, string>("Receive",(type, message) =>
            {
                Dispatcher.CurrentDispatcher.Invoke(new Action(() => AddMessage(type, message)));
            });         
        }
        
        private void AddMessage(string type,string message)
        {
            if (Message.getStaticTypeName() == type)
            {
                Message m = JsonSerializer.Deserialize<Message>(message);
                if (m == null) return;
                ListUsers.findById(m.idUserSender).Messages.Add(m);
                ListUsers.findById(m.idUserReceiver).Messages.Add(m);
                TemplateSelectUser = templateSelectUser; //без этой строки нет обновления сообщений
                
               //Была идея сортировать пользователей по дате последнего сообщения 
               //TemplateListSelectUsers.Users.OrderBy(u => u.DateTimeLastMessage); 
            }

        }
        private async Task SendMessage()
        {
            User SelectUser = ListUsers.Users.FirstOrDefault(u => u.Id == TemplateSelectUser.Id);//!
            Message message = new Message(LoginUser, SelectUser, NewText);

            try
            {
                // отправка сообщения
                await connection.InvokeAsync("Send", Message.getStaticTypeName(), JsonSerializer.Serialize(message));
            }
            catch (Exception ex)
            {
                connectionState =ex.Message;
            }
            
            NewText = string.Empty;
        }

        public void SearchTextInUsers()
        {
            LoginUser = loginUser;//Сброс поиска
            if (string.IsNullOrEmpty(SearchText))
            {
                //LoginUser = loginUser; 
              return;
            } 
            ObservableCollection<TemplateSelectUser> Users = new ObservableCollection<TemplateSelectUser>();

            TemplateListSelectUsers.Users.ToList().FindAll(u => u.FirstName.Contains(SearchText) || u.LastName.Contains(SearchText)).ForEach(u=>Users.Add(u));

            templateListSelectUsers.Users.Clear();
            templateListSelectUsers.Users = Users;
            TemplateListSelectUsers = templateListSelectUsers;

        }
        private void ShowAboutUser()
        {
            var windowAbout = new AboutUser();
            windowAbout.DataContext = Application.Current.MainWindow.DataContext;
            windowAbout.ShowDialog();
        }
        private void ShowAboutLoginUser()
        {
            var windowAbout = new AboutUser();
            SelectUser = LoginUser;
            windowAbout.DataContext = Application.Current.MainWindow.DataContext;
            windowAbout.ShowDialog();
        }
        private void ShowSearchUser()
        {
            var windowSearch = new SearchUser();
            var SearchVM=new SearchViewModel(ListUsers,LoginUser);
            windowSearch.DataContext = SearchVM;
            windowSearch.ShowDialog();
            if (windowSearch.DialogResult == true)
            {
               
                User searchUser=listUsers.Users.FirstOrDefault(u=>u.Id == SearchVM.TemplateSearchUser.Id);

                LoginUser?.FriendList.Friends.Add(searchUser);
                LoginUser = loginUser; //обновление списка
            }
        }
        private void AddSearchUserInFriends()
        {
            Application.Current.Windows.OfType<SearchUser>().First().DialogResult = true;
        }

        private void ShowAddUser()
        {
            var windowAdd = new AddUser();
            windowAdd.DataContext = Application.Current.MainWindow.DataContext;
            windowAdd.ShowDialog();
            if (windowAdd.DialogResult == true)
            {
                LoginUser?.FriendList.Friends.Add(SelectUser);
                LoginUser=loginUser; //обновление списка
            }
       }

        private void AddUserInFriends()
        {
            Application.Current.Windows.OfType<AddUser>().First().DialogResult = true;
       }

        private void NewUserWindow()
        {
             NewRegUser = new User();
             var windowNew = new NewUser();
             windowNew.DataContext = Application.Current.MainWindow.DataContext;
             windowNew.ShowDialog();
            if (windowNew.DialogResult == true)
            {
                ListUsers.Users.Add(NewRegUser);
            }
        }
       private void RegistrationUser()
        { 
    
            Application.Current.Windows.OfType<NewUser>().First().DialogResult = true;
         }

        private void LogOut()
        {
            LoginUser = null;
  
            var windowLogin = new LoginWindow();
            windowLogin.DataContext = Application.Current.MainWindow.DataContext;
            windowLogin.Show();

        }

        private void Login()
        {
            User _user=null;
            listUsers.Users.ForEach(user => { if (user.CheckLogin(LoginName, LoginPassword))
                { 
                    _user = user;                   
                }
            });

            if (_user != null) 
            { 

              Application.Current.Windows.OfType<LoginWindow>().Last().Close(); 
              TemplateSelectUser = new TemplateSelectUser(_user.FriendList?.Friends.FirstOrDefault(), _user);
              TemplateSelectUserMessages = new TemplateListMessages(_user.FriendList?.Friends.FirstOrDefault(), _user);
              LoginUser= _user;
            }
            else
              MessageBox.Show("Неудачная попытка входа");           
        }

        private bool CanSendMessage()
        {         
            return !string.IsNullOrWhiteSpace(NewText);
        }

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{
    [Serializable]
    public class User: ISerialized
    {   private static User LoginUser { get; set; }
        private static int ID { get; set; } = 0;
        public int Id { get; set; }
        public static void Login(User user) { LoginUser = user; }
        public enum level { Admin,User};
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
        public bool Active { get; set; }
        public bool IsLogin { get => this == LoginUser; }
        public level LevelUser { get; set; } 
        public string AvatarFile { get ;  set; }


        public string AvatarFileAbsolute { get => Path.Combine(Directory.GetCurrentDirectory(),
                                                               @"..\..\", "Avatars", AvatarFile);
                                           set 
                                           { AvatarFile = Path.GetFileName(value);
                                             if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(),
                                                                           @"..\..\", "Avatars", AvatarFile)))
                                             {
                                                File.Copy(value, Path.Combine(Directory.GetCurrentDirectory(), 
                                                                               @"..\..\", "Avatars", AvatarFile));
                                              }
                                           } 
                                          }

        public FriendList FriendList { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
    
        public ObservableCollection<Message> MessagesIsLogin 
        { get
            {
                var list = new ObservableCollection<Message>();
                int idIsLoginUser = LoginUser.Id;
                Messages.ToList().ForEach(m => { if (m.idUserSender==idIsLoginUser || m.idUserReceiver==idIsLoginUser) list.Add(m); });
                return list;
            }
           
        }

        public bool CheckLogin(string login, string password)
        {
            return (login == LoginName && password == Password);
        }

        public string getTypeName()
        {
            return GetType().Name; 
        }

        public User() 
        {
            Id = ID++;
        }


    }
}

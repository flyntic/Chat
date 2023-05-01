using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{
    public class TemplateSelectUser
    {
       // public enum level { Admin, User };
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateTimeLastMessage { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public bool IsNoFriend { get; set; }
        public User.level LevelUser { get; set; }
        public string AvatarFile { get; set; }
        public string AvatarFileAbsolute
        {
            get
            {  if (AvatarFile==null) return
                        Path.Combine(Directory.GetCurrentDirectory(),
                                                        @"..\..\", "Avatars", "avatar-png.png");
            else return
                Path.Combine(Directory.GetCurrentDirectory(),
                                                        @"..\..\", "Avatars", AvatarFile);
            }
            set
            {
                AvatarFile = Path.GetFileName(value);
                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(),
                                              @"..\..\", "Avatars", AvatarFile)))
                {
                    File.Copy(value, Path.Combine(Directory.GetCurrentDirectory(),
                                                   @"..\..\", "Avatars", AvatarFile));
                }
            }
        }

        //public TemplateListUsers FriendList { get; set; }
        // public List<TemplateMessage> Messages { get; set; }=new List<TemplateMessage>();


        public TemplateSelectUser(User selectUser, User loginUser)
        {
            Id = selectUser.Id;
            FirstName = selectUser.FirstName;
            LastName = selectUser.LastName;
            Phone = selectUser.Phone;
            Active = selectUser.Active;
            LevelUser = selectUser.LevelUser;
            AvatarFile = selectUser.AvatarFile;
            IsNoFriend = false;

            DateTimeLastMessage = selectUser.Messages.Max(u => u.DateOfSend);
            //FriendList = new TemplateListUsers(loginUser);
            // listMessages = new List<TemplateMessage>();

        }
            public TemplateSelectUser(int idSelectUser,DateTime date)
            {
                Id = idSelectUser;
                 IsNoFriend = true;
                 FirstName = "Нет в контактах";
                LastName = "-";
                Phone = "-";
                Active = false;
                LevelUser = User.level.User;
                AvatarFile = null;
                DateTimeLastMessage = date;
                //FriendList = new TemplateListUsers(loginUser);
                // listMessages = new List<TemplateMessage>();

            }
     }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{
    public class TemplateListUsers
    {
        public ObservableCollection<TemplateSelectUser> Users { get; set; }
        public TemplateSelectUser findById(int id)
        {
            return Users.ToList().Find(x => x.Id == id);
        }
        public TemplateListUsers(User loginUser)
        {
            Users = new ObservableCollection<TemplateSelectUser>();
            if (loginUser == null) return;

            loginUser.FriendList?.Friends.ToList().ForEach(u => Users.Add(new TemplateSelectUser(u, loginUser)));
            loginUser.Messages?.ToList().ForEach(m =>
            {
                if ((m.idUserReceiver == loginUser.Id) && (Users.ToList().Find(f => f.Id == m.idUserSender) == null))
                    Users.Add(new TemplateSelectUser(m.idUserSender, m.DateOfSend));
                if ((m.idUserSender == loginUser.Id) && (Users.ToList().Find(f => f.Id == m.idUserReceiver) == null))
                    Users.Add(new TemplateSelectUser(m.idUserReceiver, m.DateOfSend));

            });

            Users?.OrderBy(u => u.DateTimeLastMessage);

        }
        public TemplateListUsers()
        {
            Users = new ObservableCollection<TemplateSelectUser>();
        }
    }
}

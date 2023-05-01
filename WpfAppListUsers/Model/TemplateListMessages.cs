using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{
    public class TemplateListMessages
    {
        public ObservableCollection<TemplateMessage> messages { get; set; }
        public DateTime LastMessageTime { get; set; }
        public TemplateListMessages(User selectUser, User loginUser) 
        {
            messages = new ObservableCollection<TemplateMessage>();
            if (loginUser == null) return;
            if(selectUser == null) return;

            loginUser.Messages.ToList().ForEach(message =>
            {
                if (message.idUserSender == selectUser.Id)   messages.Add(new TemplateMessage(message, selectUser, loginUser)); ;
                if (message.idUserReceiver == selectUser.Id) messages.Add(new TemplateMessage(message, loginUser, selectUser));
            });

            LastMessageTime=messages.Max(u => u.DateOfSend);             
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WpfAppListUsers.Model
{
    public class TemplateMessage
    {       
        public string Text { get; set; }
        public DateTime DateOfSend { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string PhotoOfUser { get; set; }        
        public bool isSend { get; set; }
        public bool isReceive { get; set; }
        public bool isRead { get; set; }
        public TemplateMessage() { }
        public TemplateMessage(User sender, User receiver, string text)
        {
            Sender = sender;
            Receiver = receiver;
            Text = text;
            DateOfSend = DateTime.Now;
            isRead = false;
            isReceive = false;
            isSend = false;
        }
        public TemplateMessage(Message message,List<User> users)
        {
            Sender = users.Find(u=> u.Id==message.idUserSender);
            Receiver = users.Find(u=>u.Id==message.idUserReceiver);
            Text = message.Text;
            DateOfSend = message.DateOfSend;
            isRead = message.isRead;
            isReceive = message.isReceive;
            isSend = message.isSend;
        }
        public TemplateMessage(Message message, User senderUser, User receiverUser)
        {
            Sender =senderUser;
            Receiver = receiverUser;
            Text = message.Text;
            DateOfSend = message.DateOfSend;
            isRead = message.isRead;
            isReceive = message.isReceive;
            isSend = message.isSend;
        }
    }
    }

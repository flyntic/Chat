using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{
    [Serializable]
    public class Message: ISerialized
    {
        private static int ID { get; set; } = 0;
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateOfSend { get; set; }
        public int idUserSender { get; set; }        
        public int idUserReceiver { get; set; }
        public bool isSend { get; set; }
        public bool isReceive { get; set; }
        public bool isRead { get; set; }
       
        public string getTypeName()
        {
            return GetType().Name;
        }

        public static string getStaticTypeName()
        {
            return "Message";
        }

        public Message() { }
        public Message(User sender, User receiver, string text)
        {
            idUserSender = sender.Id;
            idUserReceiver = receiver.Id;
            Text = text;
            DateOfSend = DateTime.Now;
            Id = ID++;
            isRead = false;
            isReceive = false;
            isSend = false;
        }

    }
}

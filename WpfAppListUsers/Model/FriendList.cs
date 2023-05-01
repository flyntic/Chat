using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{
    [Serializable]
    public class FriendList: ISerialized
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Friends { get; set; }

        public string getTypeName()
        {
            return GetType().Name;
        }
    }
}

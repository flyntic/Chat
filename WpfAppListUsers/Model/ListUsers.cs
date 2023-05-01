using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{
    public class ListUsers
    {
        public List<User> Users { get; set; }=new List<User>();
        public User findById(int id)
        { 
            return Users.Find(x => x.Id == id);
        }
    }
}

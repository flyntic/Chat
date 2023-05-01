using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppListUsers.Model
{

    public interface ISerialized
    {
        string getTypeName();
    }

    public static class  ISerializeExtension
    {   
         public static string getStaticTypeName(this ISerialized i)
         {         
            return i.GetType().Name;
         }
    }

}

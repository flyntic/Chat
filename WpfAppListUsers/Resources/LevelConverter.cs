using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfAppListUsers.Model;
namespace WpfAppListUsers.Resources
{

    public class LevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((User.level)value)
            {
                case User.level.User: return (string)("User");
                case User.level.Admin: return (string)("Admin");
            }
            return (string)"Nodef";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

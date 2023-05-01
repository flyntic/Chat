using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppListUsers.View
{
    public static class WindowExtension
    {
        public static void AnyWindowDrag(this Window w,object sender, MouseButtonEventArgs e)
        {
             w.DragMove();
        }

        public static void AnyWindowMaximize(this Window w,object sender, RoutedEventArgs a)
        {           
            if (w.WindowState == WindowState.Normal)
            {
                w.WindowState = WindowState.Maximized;
            }
            else
            {
                w.WindowState = WindowState.Normal;
            }
        }

        public static void AnyWindowMinimize(this Window w, object sender, RoutedEventArgs a)
        {
            w.WindowState = WindowState.Minimized;
        }

        public static void AnyWindowClose(this Window window, object sender,RoutedEventArgs a)
        {         
            window.Close();
        }

        public static void AppClose(this Window window)
        {
            Application.Current.Shutdown();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Attendance
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : UserControl
    {
        public static bool ISListShow = true;
        public Setting()
        {
            InitializeComponent();
        }

        private void AboutUs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.changePage(new AboutUS());
        }

        private void Help_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.changePage(new Help());
        }

        private void ListShow_Checked(object sender, RoutedEventArgs e)
        {
            ISListShow = true;
        }

        private void ListShow_Unchecked(object sender, RoutedEventArgs e)
        {
            ISListShow = false;
        }
    }
}

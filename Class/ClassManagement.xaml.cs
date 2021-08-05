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

namespace Attendance.Class
{
    /// <summary>
    /// Interaction logic for ClassManagement.xaml
    /// </summary>
    public partial class ClassManagement : UserControl
    {
        public ClassManagement()
        {
            InitializeComponent();
        }

        private void NewClass_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.changePage(new AddnewClass());
        }

        private void DeleteClass_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.changePage(new DeleteClass());
        }

        private void Edite_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("این ویژگی در نسخه های آینده فعال خواهد شد");
        }
    }
}

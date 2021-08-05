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
    /// Interaction logic for ClassItem.xaml
    /// </summary>
    public partial class ClassItem : UserControl
    {
        public ClassItem(string classname)
        {
            InitializeComponent();
            ClassName.Text = classname;
        }

    }
}

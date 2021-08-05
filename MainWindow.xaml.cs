using Attendance.Class;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
namespace Attendance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate void GotoMainPage(object sender, MouseButtonEventArgs e);
    public delegate void ChangePage(UIElement uIElement);

    public partial class MainWindow : Window
    {
        Check ch;
        AddnewClass newclass;
        DeleteClass deletemun;
        DataBase db;
        Setting setting;
        ClassManagement classManagement;
        public static GotoMainPage gotoMainPage;
        public static ChangePage changePage;

        public MainWindow()
        {
            InitializeComponent();
            db = new DataBase();

            ch = new Check(db);
            setting = new Setting();
            Body.Children.Add(ch);
            gotoMainPage += Main_MouseLeftButtonUp;
            changePage += ChangePage;
        }
        void ChangePage(UIElement ui)
        {
            Body.Children.Clear();
            Body.Children.Add(ui);
        }
        private void AddClass_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            newclass = new AddnewClass();
            ChangePage(newclass);
        }
        private void Main_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ch = new Check(db);
            ChangePage(ch);
        }

        private void Setting_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(setting);
        }

        private void ManageClass_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            classManagement = new ClassManagement();
            ChangePage(classManagement);

        }
    }
}

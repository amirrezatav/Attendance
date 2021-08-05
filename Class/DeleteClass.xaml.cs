using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
namespace Attendance
{
    /// <summary>
    /// Interaction logic for DeleteClass.xaml
    /// </summary>
    public partial class DeleteClass : UserControl
    {
        public DeleteClass()
        {
            InitializeComponent();
            LoadClass();
        }
        public void LoadClass()
        {
            try
            {
                List<Student> allstudent = new List<Student>();
                string cs = "Data Source=Students.db";
                using (SQLiteConnection dbconnection = new SQLiteConnection(cs))
                {
                    dbconnection.Open();


                    string stm = "SELECT DISTINCT ClassName FROM Student";

                    var cmd = new SQLiteCommand(stm, dbconnection);

                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ComboBoxItem cmit = new ComboBoxItem();
                        cmit.Content = rdr.GetString(0);
                        cmit.HorizontalContentAlignment = HorizontalAlignment.Right;
                        Class0.Items.Add(cmit);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "102 Error");
            }
        }
        public void DeleteData(string classname)
        {

            try
            {
                string cs = "Data Source=Students.db";
                using (SQLiteConnection dbconnection = new SQLiteConnection(cs))
                {
                    dbconnection.Open();

                    var cmd = new SQLiteCommand(dbconnection);
                    cmd.CommandText = "DELETE FROM Student WHERE ClassName == @class;";
                    cmd.Parameters.AddWithValue("@class", classname);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "102 Error");
            }

        }



        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (Class0.SelectedItem != null)
            {
                try
                {
                    string class0 = Class0.SelectedItem != null ? ((ComboBoxItem)Class0.SelectedItem).Content.ToString() : "";
                    var res = MessageBox.Show( " آیا مطمئن هستید که کلاس "+ class0 + " حذف شود ؟ \n قابل توجه است که تمامی دانش آموزان در این کلاس در صورت تایید حذف می شوند .", "حذف کلاس", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        DeleteData(class0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "102 Error");
                }
                MainWindow.gotoMainPage(null, null);
            }
            else
                MessageBox.Show("جهت حذف باید یک کلاس را انتخاب کنید.", "کلاس انتخاب نشده" , MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

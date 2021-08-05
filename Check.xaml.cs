using Attendance.Class;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Documents;
using System.Windows.Input;


namespace Attendance
{
    /// <summary>
    /// Interaction logic for Check.xaml
    /// </summary>
    public partial class Check : UserControl
    {
        DataBase db;
        List<ClassItem> AllClass = new List<ClassItem>();
        public Check(DataBase db)
        {
            InitializeComponent();
            this.db = db;
            SetUI();
            LoadClass();
        }
        void SetUI()
        {
            if (Setting.ISListShow == true)
            {
                ClassPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                Class0.Visibility = Visibility.Collapsed;
                ClassBar.Orientation = Orientation.Vertical;
            }
        }
        public void LoadClass()
        {
            try
            {
                var res = db.LoadClass();
                if (Setting.ISListShow == true)
                {
                    foreach (var item in res)
                    {
                        ComboBoxItem cmit = new ComboBoxItem();
                        cmit.Content = item;
                        cmit.HorizontalContentAlignment = HorizontalAlignment.Right;
                        Class0.Items.Add(cmit);
                    }
                }
                else
                {
                    foreach (var item in res)
                    {
                        var newclass = new ClassItem(item);
                        AllClass.Add(newclass);
                        ClassPanel.Children.Add(newclass);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "102 Error");
                CheckBTN.IsEnabled = true;
            }
        }
        string[] ConvertRichTextBoxContentsToString(RichTextBox rtb)

        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart,

                rtb.Document.ContentEnd);
            return textRange.Text.Replace("\r", "").Replace("\t", "").Split('\n');

        }
        private void SearchCheckBox()
        {
            bool SelectedClass = false;
           
            
            foreach (var item in AllClass)
            {
                if(item.ClassBox.IsChecked == true)
                {
                    SelectedClass = true;
                    var allstudent = db.LoadData(item.ClassName.Text);
                    var student = ConvertRichTextBoxContentsToString(Students);
                    foreach (var target in student)
                    {
                        for (int i = 0; i < allstudent.Count; i++)
                        {
                            if (target == allstudent[i].Name || target.Replace("‌", "").Replace(" ", "") == allstudent[i].Name.Replace("‌", "").Replace(" ", ""))
                                allstudent.RemoveAt(i);
                        }
                    }
                    if (allstudent.Count == 0)
                    {
                        MessageBox.Show("هیچ دانش آموزی غایب نیست", "بدون غایب", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        foreach (var target in allstudent)
                        {
                            Absents.Items.Add(target);
                        }
                }
            }
            
            if (!SelectedClass)
                MessageBox.Show("یک کلاس را انتخاب کنید");
        }
        void SearchList()
        {
           
            if (Class0.SelectedItem == null)
            {
                MessageBox.Show("یک کلاس را انتخاب کنید");
                return;
            }
            CheckBTN.IsEnabled = false;
            this.Cursor = Cursors.Wait;
            string class0 = Class0.SelectedItem != null ? ((ComboBoxItem)Class0.SelectedItem).Content.ToString() : "";

            var allstudent = db.LoadData(class0);
            var student = ConvertRichTextBoxContentsToString(Students);
            foreach (var item in student)
            {
                for (int i = 0; i < allstudent.Count; i++)
                {
                    if (item == allstudent[i].Name || item.Replace("‌", "").Replace(" ", "") == allstudent[i].Name.Replace("‌", "").Replace(" ", ""))
                        allstudent.RemoveAt(i);
                }
            }
            if (allstudent.Count == 0)
            {
                MessageBox.Show("هیچ دانش آموزی غایب نیست", "بدون غایب", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                foreach (var item in allstudent)
                {
                    Absents.Items.Add(item);
                }
        }
        private void CheckBTN_Click(object sender, RoutedEventArgs e)
        {
            CheckBTN.Cursor = Cursors.Wait;
            Cursor = Cursors.Wait;
            CheckBTN.IsEnabled = false;
            Absents.Items.Clear();

            if (Setting.ISListShow == true)
                SearchList();
            else
                SearchCheckBox();

            this.Cursor = Cursors.Arrow;
            CheckBTN.Cursor = Cursors.Hand;
            CheckBTN.IsEnabled = true;
        }

        private void Absents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
            if (list.SelectedItem != null)
            {
                Student st = (Student)list.SelectedItem;
                var res = MessageBox.Show(st.Name + " از لیست افراد غایب حذف شود ؟" , "حذف از لیست غایب ها" , MessageBoxButton.YesNo , MessageBoxImage.Question);
                if(res == MessageBoxResult.Yes)
                {
                    list.Items.Remove(list.SelectedItem);
                }
            }
        }
    }
}

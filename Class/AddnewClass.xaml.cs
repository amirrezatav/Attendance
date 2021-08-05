using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.SQLite;
namespace Attendance
{

    /// <summary>
    /// Interaction logic for AddnewClass.xaml
    /// </summary>
    public partial class AddnewClass : UserControl
    {
        public AddnewClass()
        {
            InitializeComponent();
        }
        void TrunOff()
        {
            this.Cursor = Cursors.Wait;
            Save.Cursor = Cursors.Wait;
            enterinfo.Cursor = Cursors.Wait; 
            enterinfo.IsEnabled = false;
            GetExcel.IsEnabled = false;
            classname.IsEnabled = false;
            Save.IsEnabled = false;
        }
        void TrunON()
        {
            enterinfo.IsEnabled = true;
            GetExcel.IsEnabled = true;
            classname.IsEnabled = true;
            Save.IsEnabled = true;
            this.Cursor = Cursors.Arrow;
            Save.Cursor = Cursors.Hand;
            enterinfo.Cursor = Cursors.Hand;
        }
        public void SaveData(Student item)
        {
            try
            {
                string cs = "Data Source=Students.db";
                using (SQLiteConnection dbconnection = new SQLiteConnection(cs))
                {
                    dbconnection.Open();
                    var cmd = new SQLiteCommand(dbconnection);
                    cmd.CommandText = "INSERT INTO Student (Name,HNO,FNO,MNO,SNO,NID,ClassName)VALUES(@name,@hNO,@fNO,@mNO,@sNO,@nID,@className); ";
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@hNO", item.HNo);
                    cmd.Parameters.AddWithValue("@fNO", item.FNo);
                    cmd.Parameters.AddWithValue("@mNO", item.MNo);
                    cmd.Parameters.AddWithValue("@sNO", item.SNo);
                    cmd.Parameters.AddWithValue("@nID", item.NID);
                    cmd.Parameters.AddWithValue("@className", item.Class);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "102 Error");
            }
        }
        void AddStudents(string path)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook excelWorkbook = xlApp.Workbooks.Open(path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets excelSheets = excelWorkbook.Worksheets;
            string currentSheet = "Sheet1";
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);
            int i = 2;
            while (true)
            {
                Student st = new Student();
                st.Name = (string)(excelWorksheet.Cells[i, 1] as Excel.Range).Value + " " + (string)(excelWorksheet.Cells[i, 2] as Excel.Range).Value;
                st.HNo = Convert.ToString((excelWorksheet.Cells[i, 3] as Excel.Range).Value);
                st.FNo = Convert.ToString((excelWorksheet.Cells[i, 4] as Excel.Range).Value);
                st.MNo = Convert.ToString((excelWorksheet.Cells[i, 5] as Excel.Range).Value);
                st.SNo = Convert.ToString((excelWorksheet.Cells[i, 6] as Excel.Range).Value);
                st.NID = Convert.ToString((excelWorksheet.Cells[i, 7] as Excel.Range).Value);
                st.Class = classname.Text;
                if (st.MNo == null && st.HNo == null && (st.Name == " " || st.Name == null) && st.FNo == null)
                    break;
                SaveData(st);
                i++;
            }
            Marshal.ReleaseComObject(excelSheets);
            Marshal.ReleaseComObject(excelWorkbook);
            Marshal.ReleaseComObject(xlApp);
        }
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TrunOff();
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "Excel 2003 (*.xls)|*.xls";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Title = "انتخاب لیست دانشجویان";
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePath.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "103 Error");
            }
            finally
            {
                TrunON();
            }
        }
        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            TrunOff();
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "ذخیره سازی قالب ورود اطلاعات";
                saveFileDialog.Filter = "Excel 2003 (*.xls)|*.xls";
                if (saveFileDialog.ShowDialog() == true)
                {
                    Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                    if (xlApp == null)
                    {
                        MessageBox.Show("Excel is not properly installed!");
                        return;
                    }
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Cells[1, 1] = "نام";
                    xlWorkSheet.Cells[1, 2] = "نام خانوادگی";
                    xlWorkSheet.Cells[1, 3] = "تلفن منزل";
                    xlWorkSheet.Cells[1, 4] = "تلفن پدر";
                    xlWorkSheet.Cells[1, 5] = "تلفن مادر";
                    xlWorkSheet.Cells[1, 6] = "تلفن دانش آموز";
                    xlWorkSheet.Cells[1, 7] = "کد ملی دانش آموز";
                    xlWorkBook.SaveAs(saveFileDialog.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);
                    MessageBox.Show("ذخیره سازی با موفقیت بود", "موفقییت", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "100 Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                TrunON();
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TrunOff();
            try
            {
                if (classname.Text == "")
                    throw new Exception("نام کلاس نباید خالی باشد");
                if (FilePath.Text == "")
                    throw new Exception("فایلی انتخاب نشده است.");
                AddStudents(FilePath.Text);
                MessageBox.Show("کلاس ذخیره شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.gotoMainPage(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "1001 Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                TrunON();
            }
        }
    }
}

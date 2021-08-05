using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows;

namespace Attendance
{
    public class DataBase
    {
        public DataBase()
        {
            DataBaseConfig();
        }
        private void DataBaseConfig()
        {
            if (!System.IO.File.Exists("Students.db"))
            {
                SQLiteConnection.CreateFile(".\\Students.db");

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Students.db;Version=3;");
                m_dbConnection.Open();

                string sql = "CREATE TABLE [Student]([Name] TEXT NOT NULL, [HNO] TEXT,  [FNO] TEXT,   [MNO] TEXT,   [SNO] TEXT,   [NID] TEXT,   [ClassName] NOT NULL);";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                m_dbConnection.Close();
            }
        }
        public List<Student> LoadData(params string[] classname)
        {
            List<Student> allstudent = new List<Student>();
            string cs = "Data Source=Students.db";
            using (SQLiteConnection dbconnection = new SQLiteConnection(cs))
            {
                dbconnection.Open();

                var cmd = new SQLiteCommand(dbconnection);
                cmd.CommandText = "SELECT * FROM Student Where ";
                for (int i = 0; i < classname.Length; i++)
                {
                    cmd.CommandText += "ClassName == '" + classname[0] + "'";
                    if (i + 1 != classname.Length)
                        cmd.CommandText += " OR ";
                }

                cmd.Prepare();

                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student newstudent = new Student();
                    try { newstudent.Name = rdr.GetString(0); } catch { }
                    try { newstudent.HNo = rdr.GetString(1); } catch { }
                    try { newstudent.FNo = rdr.GetString(2); } catch { }
                    try { newstudent.SNo = rdr.GetString(4); } catch { }
                    try { newstudent.MNo = rdr.GetString(3); } catch { }
                    try { newstudent.NID = rdr.GetString(5); } catch { }
                    try { newstudent.Class = rdr.GetString(6); } catch { }
                    allstudent.Add(newstudent);
                }
            }
            return allstudent;
        }
        public List<string> LoadClass()
        {
            List<string> res = new List<string>();
            string cs = "Data Source=Students.db";
            using (SQLiteConnection dbconnection = new SQLiteConnection(cs))
            {
                dbconnection.Open();

                string stm = "SELECT DISTINCT ClassName FROM Student";

                var cmd = new SQLiteCommand(stm, dbconnection);

                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    res.Add(rdr.GetString(0));
                return res;
            }
        }

    }
}

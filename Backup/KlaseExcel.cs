using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace SMS_Shkolla_Manager
{
    class KlaseExcel
    {
        private string connString;
        private OleDbConnection conn = new OleDbConnection();
        public string excelPath = Application.StartupPath + "\\Excel";
        OleDbCommand cmd = new OleDbCommand();
        public int gabim = 0;

        public  KlaseExcel(string file, string[] fushat, string[] lloji)
        {
            try
            {
                connString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                   @"Data Source="+excelPath + "\\" + file  +
                   @";Extended Properties=""Excel 8.0;HDR=YES""";
                conn.ConnectionString = connString;

                if (!Directory.Exists(excelPath))
                {
                    Directory.CreateDirectory(excelPath);
                }
                if (File.Exists(excelPath + "\\" + file))
                    File.Delete(excelPath + "\\" + file);
                
                conn.Open();
                cmd.Connection = conn;
                string str = "";
                str = "CREATE TABLE " + file.Remove(file.Length - 4) + "(";
                for (int i = 0; i < fushat.Length; i++)
                {
                    str += fushat[i] + " " + lloji[i] + ",";
                }
                str = str.Remove(str.Length - 1);
                str += ")";
                cmd.CommandText = str;
                cmd.ExecuteNonQuery();
                gabim = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ndodhi një gabim gjatë konvertimit në Excel!" 
                    + Environment.NewLine  + "Nqs keni të hapur ndonjë skedar Excel me emër " + file + 
                    Environment.NewLine + "mbylleni dhe riprovoni të bëni konvertimin në Excel.", "Konvertimi në Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gabim = 1;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ShkruajTabele(string file,
            string[] fushat, string[] llojet, DataSet dsDergesa)
        {
            try
            {
                
                connString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                       @"Data Source=" + excelPath + "\\" + file +
                       @";Extended Properties=""Excel 8.0;HDR=YES""";
                conn.ConnectionString = connString;
                conn.Open();
                cmd.Connection = conn;
                foreach(DataRow dr in dsDergesa.Tables[0].Rows)
                {
                    string str = "INSERT INTO [" + file.Remove(file.Length - 4) + "$] (";
                    for (int j = 0; j < fushat.Length; j++)
                        str += fushat[j] + ",";
                    str = str.Remove(str.Length - 1);
                    str += ") VALUES ( ";
                    for (int j = 0; j < fushat.Length; j++)
                    {
                        if (llojet[j] == "char(255)")
                            str += "'" + dr[fushat[j]].ToString().Replace("'", "`") + "',";
                        else if (llojet[j] == "float")
                            str += dr[fushat[j]].ToString() + ",";
                        else
                            str += dr[fushat[j]].ToString() + ",";

                    }
                    str = str.Remove(str.Length - 1);
                    str += ")";
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                    gabim = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ndodhi një gabim gjatë konvertimit në Excel!"
                    + Environment.NewLine + "Nqs keni të hapur ndonjë skedar Excel me emër " + file +
                    Environment.NewLine + "mbylleni dhe riprovoni të bëni konvertimin në Excel.", "Konvertimi në Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gabim = 1;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

        }
    }
}

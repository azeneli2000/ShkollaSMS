using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace SMS_Shkolla_Manager
{
    public partial class NgarkoSkedare : Form
    {
        public bool ngarkuar = false;
        public NgarkoSkedare()
        {
            InitializeComponent();
        }

        private void LexoTeDhenaPerShkollen()
        {
            try
            {
                WebClient client = new WebClient();
                System.IO.Stream shkolla = client.OpenRead("http://www.notat.info//public//Databases//MyDatabase//shkolla.txt");
                //System.IO.Stream shkolla = client.OpenRead("http://localhost//notat1.info//public//Databases//MyDatabase//shkolla.txt");

                StreamReader readerShkolla = new StreamReader(shkolla);
                while (!readerShkolla.EndOfStream)
                {
                    string sShkolla = readerShkolla.ReadLine();
                    string[] strV = sShkolla.Split('#');
                    if (strV[0] == Main.serial)
                    {
                        Main.emriShkolla = strV[1];
                        Main.shkurtimiShkolla = strV[3];
                        break;
                    }
                }
                if (Main.emriShkolla.Trim() == "")
                {
                    MessageBox.Show("Shkolla juaj nuk rezulton të jetë e regjistruar për shërbimin e " +
                       Environment.NewLine + "Konsultimit të Notave Online dhe të Dërgimit të notave me sms!", Text,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                progressBar.Value = 5;
            }
            catch (WebException webEx)
            {
                throw new Exception("Nuk mund të kryhet lidhja me internetin! Nr 3");
            }
        }

        private void MbushDataSetMeNxenesitEShkolles()
        {
            try
            {
                dsTelefon.Tables[0].Rows.Clear();
                WebClient client = new WebClient();
                client.DownloadFile("http://www.notat.info//public//Databases//MyDatabase//user.txt", "user.txt");
                //client.DownloadFile("http://localhost//notat1.info//public//Databases//MyDatabase//user.txt", "user.txt");

                progressBar.Value = 15;
                string[] array = File.ReadAllLines("user.txt");                
                int i = 0;
                int count = 0;
                int step = array.Length / 80;

                foreach(string sUser in array)
                {
                    if (i < 3)
                    {
                        i++;
                        continue;
                    }
                    string[] strV = sUser.Split('#');
                    //nqs nxenesi i perket shkolles
                    if (strV[1] == Main.serial)
                    {
                        try
                        {
                            DataRow newR = dsTelefon.Tables[0].NewRow();
                            newR["AMZA"] = strV[4];
                            newR["CIKLI"] = Convert.ToBoolean(strV[5]);
                            newR["TELEFON"] = strV[6];
                            dsTelefon.Tables[0].Rows.Add(newR);
                            count++;
                        }
                        catch (Exception ii)
                        {
                            int y = 0;
                        }
                    }
                    dsTelefon.AcceptChanges();
                    if (count % (step + 1) == 0 && count != 0)
                    {
                        progressBar.Value++;
                    }
                    i++;
                }
                progressBar.Value = 100;
                ngarkuar = true;
            }
            catch (WebException webEx)
            {
                throw new Exception("Nuk mund të kryhet lidhja me internetin! Nr 3");
            }
        }

        #region Event Handler
        private void btnDil_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LexoTeDhenaPerShkollen();
                MbushDataSetMeNxenesitEShkolles();
                btnOk.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}
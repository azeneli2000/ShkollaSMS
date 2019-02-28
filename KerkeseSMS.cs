using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Web.Mail;

namespace SMS_Shkolla_Manager
{
    public partial class KerkeseSMS : Form
    {
        private ConnDbase conObject;

        public KerkeseSMS()
        {
            InitializeComponent();
        }

        #region PrivateMethods

        public static bool isEmail(string inputEmail)
        {
            if (inputEmail == null || inputEmail.Length == 0)
            {
                return false;
            }

            const string expression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex regex = new Regex(expression);
            return regex.IsMatch(inputEmail);
        }

        private void KonvertoSinkronizoNeWebKerkesa()
        {
            string[] fusha = new string[4];
            string[] llojet = new string[4];
            fusha[0] = "ID_KERKESA";
            fusha[1] = "DATA_KERKESA";
            fusha[2] = "SASIA";
            fusha[3] = "ALG";

            llojet[0] = "char(255)";
            llojet[1] = "char(255)";
            llojet[2] = "char(255)";
            llojet[3] = "char(255)";
            KlaseExcel excel = new KlaseExcel("KERKESA.xls", fusha, llojet);
            DataSet dsKerkesa = conObject.ExecuteQuery("SELECT * FROM KERKESA WHERE YEAR(DATA_KERKESA) = YEAR(GETDATE())");
            excel.ShkruajTabele("KERKESA.xls", fusha, llojet, dsKerkesa);

        }

        private void DergoEmail(int idKerkesa,DateTime dtTani,int nrSMS,int alg)
        {
            try
            {
                EnhancedMailMessage msg = new EnhancedMailMessage();

                msg.From = "kerkesesms@gmail.com";
                msg.FromName = "Kerkese SMS";
                msg.To = "info@visioninfosolution.com";
                msg.Subject = Main.emriShkolla;
                string body = "Të dhënat për kërkësën: " + Environment.NewLine;
                body += "ID KERKESA:  " + idKerkesa.ToString() + Environment.NewLine;
                body += "DATA/ORA:  " + dtTani.ToString("dd.MM.yyyy HH:mm") + Environment.NewLine;
                body += "NR SERIAL SHKOLLA:  " + Main.serial + Environment.NewLine;
                body += "NR SMS:  " + nrSMS.ToString() + Environment.NewLine;
                body += "ALGORITMI:  " + alg.ToString() + Environment.NewLine;
                body += Environment.NewLine;

                body += "Koment shtesë" + Environment.NewLine;
                body += txtKoment.Text;

                msg.Body = body;

                msg.SMTPServerName = "smtp.gmail.com";
                msg.SMTPUserName = "kerkesesms@gmail.com";
                msg.SMTPUserPassword = "vision12";
                msg.SMTPServerPort = 465;
                msg.SMTPSSL = true;
                msg.Send();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        private void btnDergo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Random rnd = new Random();
                int alg = rnd.Next(5);
                alg = alg + 1;
                //regjistro kerkese ne bazen e te dhenave
                DateTime dtTani = DateTime.Now;
                string strSql = "INSERT INTO KERKESA VALUES('" + dtTani.ToString("yyyy-MM-dd HH:mm") + "'," + numSMS.Value.ToString() + ", " + alg.ToString() + ")";
                conObject.ExecuteNonQuery(strSql);
                KonvertoSinkronizoNeWebKerkesa();
                strSql = "SELECT MAX(ID_KERKESA) AS MAX FROM KERKESA";
                DataSet dsMax = conObject.ExecuteQuery(strSql);
                int idKerkesa = Convert.ToInt32(dsMax.Tables[0].Rows[0][0]);


                //dergo email
                DergoEmail(idKerkesa, dtTani, Convert.ToInt32(numSMS.Value), alg);

                MessageBox.Show("Kërkesa juaj u dërgua!" + Environment.NewLine +
                    "Do të njoftoheni me email për mënyrën se si mund ta aktivizoni kërkesën e sapodërguar!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void KerkeseSMS_Load(object sender, EventArgs e)
        {
            conObject = new ConnDbase();
        }

        private void btnDil_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
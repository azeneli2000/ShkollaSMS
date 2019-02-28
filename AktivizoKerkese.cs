using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMS_Shkolla_Manager
{
    public partial class AktivizoKerkese : Form
    {
        public int idKerkesa;
        public DateTime dataKerkesa;
        public int nrSMS;
        public int alg;
        public bool sukses = false;
        public AktivizoKerkese()
        {
            InitializeComponent();
        }

        private void btnDil_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            error.SetError(txtNrAktivizimi, "");
            if (txtNrAktivizimi.Text.Trim() == "")
            {
                error.SetError(txtNrAktivizimi, "Shkruani numrin e aktivizimit për kërkesën!");
                return;
            }
            string strFillestar = idKerkesa.ToString();
            strFillestar += dataKerkesa.ToString("dd.MM.yyyy HH:mm");
            strFillestar += Main.serial.ToString();
            strFillestar += nrSMS.ToString();
            string strAlg = "";
            switch (alg)
            {
                case 1:
                    strAlg = "SHA1";
                    break;
                case 2:
                    strAlg = "SHA256";
                    break;
                case 3:
                    strAlg = "SHA384";
                    break;
                case 4:
                    strAlg = "SHA512";
                    break;
                case 5:
                    strAlg = "MD5";
                    break;
            }
            byte[] salt = new byte[5];
            salt[0] = Convert.ToByte("8");
            salt[0] = Convert.ToByte("7");
            salt[0] = Convert.ToByte("3");
            salt[0] = Convert.ToByte("6");
            salt[0] = Convert.ToByte("9");
            string hashedString = Hash.ComputeHash(strFillestar, strAlg, salt);

            string inputString = txtNrAktivizimi.Text;
            if (inputString == hashedString)
            {

                MessageBox.Show("Kërkesa e zgjedhur u aktivizua!" + 
                    Environment.NewLine + "Llogaria juaj u shtua me " + nrSMS.ToString() + " SMS.", Text, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                sukses = true;
                Close();
            }
            else
            {
                MessageBox.Show("Numri që keni shkruar nuk është i saktë." +
                    Environment.NewLine + "Sigurohuni që keni zgjedhur kërkesën e duhur!", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sukses = false;
            }
            textBox1.Text = hashedString;
        }

       
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Janus.Windows.GridEX;
using System.Net;
using System.IO;

namespace SMS_Shkolla_Manager
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private ConnDbase conObject;
        private string vitiShkollor;

        public static string shkurtimiShkolla = "";
        public static string emriShkolla = "";
        public static string serial = "";


        [DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, UInt32 VolumeNameSize, ref UInt32 VolumeSerialNumber, ref UInt32 MaximumComponentLength, ref UInt32 FileSystemFlags, StringBuilder FileSystemNameBuffer, UInt32 FileSystemNameSize);

        #region CheckedChanged
        private void rbNotaMomentale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNotaMomentale.Checked)
            {
                dtpFillimi.Enabled = true;
                dtpMbarimi.Enabled = true;
            }
            else
            {
                dtpFillimi.Enabled = false;
                dtpMbarimi.Enabled = false;
            }
            
        }

        private void rbSemestri1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSemestri1.Checked)
            {
                dtpFillimi.Enabled = false;
                dtpMbarimi.Enabled = false;
            }
            else
            {
                dtpFillimi.Enabled = true;
                dtpMbarimi.Enabled = true;
            }
        }

        private void rbSemestri2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSemestri2.Checked)
            {
                dtpFillimi.Enabled = false;
                dtpMbarimi.Enabled = false;
            }
            else
            {
                dtpFillimi.Enabled = true;
                dtpMbarimi.Enabled = true;
            }
        }

        private void rbVjetore_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVjetore.Checked)
            {
                dtpFillimi.Enabled = false;
                dtpMbarimi.Enabled = false;
            }
            else
            {
                dtpFillimi.Enabled = true;
                dtpMbarimi.Enabled = true;
            }
        }

        private void rbMungesa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMungesa.Checked)
            {
                dtpFillimi.Enabled = false;
                dtpMbarimi.Enabled = false;
                cbPaNota.Enabled = false;
            }
            else
            {
                dtpFillimi.Enabled = true;
                dtpMbarimi.Enabled = true;
                cbPaNota.Enabled = true;
            }
        }

        private void rbTekst_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTekst.Checked)
            {
                dtpFillimi.Enabled = false;
                dtpMbarimi.Enabled = false;
                cbPaNota.Enabled = false;
                txtSMS.Enabled = true;
            }
            else
            {
                dtpFillimi.Enabled = true;
                dtpMbarimi.Enabled = true;
                cbPaNota.Enabled = true;
                txtSMS.Enabled = false;
            }
        }

        #endregion

        #region PrivateMethods
        private string GjejVitinAktual()
        {
            if (DateTime.Now.Month <= 7)
                return Convert.ToString(DateTime.Now.Year - 1) + '-' + DateTime.Now.Year.ToString();
            else
                return DateTime.Now.Year.ToString() + '-' + Convert.ToString(DateTime.Now.Year + 1);
        }

        private int semestriAktual()
        {
            if (DateTime.Now.Month == 9 || DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12 || DateTime.Now.Month == 1)
                return 1;
            else return 2;
        }

        /// <summary>
        /// kthen nje dataset me notat e nxenesve per klasat e zgjedhura
        /// ne varesi te kritereve te kerkimit
        /// </summary>
        /// <returns></returns>
        private DataSet KerkoNotat()
        {
            string strKushti = " AND NT_VITI_SHKOLLOR = '" + vitiShkollor + "' AND NT_RIPROVIM = 0 ";
            int semestri = semestriAktual();
            if (rbNotaMomentale.Checked)
            {
                string fillRev, mbarRev;
                fillRev = dtpFillimi.Value.ToString("yyyy/MM/dd");
                mbarRev = dtpMbarimi.Value.ToString("yyyy/MM/dd");
                strKushti += " AND NT_MOMENTALES" + semestri + " = 1 AND NT_SEMESTRI1= 0 AND" +
                    " NT_SEMESTRI2 = 0 AND NT_VJETORE = 0 AND NT_DATA BETWEEN '" + fillRev + "' AND '" + mbarRev + "' AND NT_MUNGESE_ME = 0 AND NT_MUNGESE_PA = 0 ";
            }
            else if (rbMungesa.Checked)
            {
                DateTime dtSot = DateTime.Now;
                string sotRev = dtSot.ToString("yyyy/MM/dd");
                strKushti += " AND lower(NT_VLERESIMI) = 'm' AND NT_MOMENTALES" + semestri + " = 1 AND NT_DATA = '" + sotRev + "' ";
            }
            else if (rbSemestri1.Checked)
            {
                strKushti += " AND NT_SEMESTRI1 = 1 AND NT_MUNGESE_ME = 0 AND NT_MUNGESE_PA = 0 ";
            }
            else if (rbSemestri2.Checked)
            {
                strKushti += " AND NT_SEMESTRI2 = 1 AND NT_MUNGESE_ME = 0 AND NT_MUNGESE_PA = 0 ";
            }
            else if (rbVjetore.Checked)
            {
                strKushti += " AND NT_VJETORE = 1 AND NT_MUNGESE_ME = 0 AND NT_MUNGESE_PA = 0 ";
            }
            string strKushtiKlasat = " AND (";
            int count = 0;
            foreach (Control c in panelExKlasat.Controls)
            {
                CheckBox cb = c as CheckBox;
                if (cb.Checked)
                {
                    if (cb.Name == "cbAll")
                        break;
                    else
                    {
                        count++;
                        if (count == 1)
                            strKushtiKlasat += "NT_KLASA = " + cb.Tag;
                        else
                            strKushtiKlasat += " OR NT_KLASA = " + cb.Tag;
                    }
                }
            }
            strKushtiKlasat += ")";
            if (count > 0)
                strKushti = strKushti + strKushtiKlasat + " ORDER BY CONVERT(INT, NT_KLASA), "  + 
                    "NT_INDEKSI, NT_NR_AMZA, NT_CIKLI, NT_EMRI_LENDA, NT_DATA";
            string strSql = "SELECT TBL_NOTA.*, AMZA_EMRI, AMZA_MBIEMRI FROM TBL_NOTA, TBL_AMZA WHERE NT_NR_AMZA = AMZA_NR_AMZA AND NT_CIKLI = AMZA_CIKLI " +
                strKushti;
            return conObject.ExecuteQuery(strSql);
        }

        /// <summary>
        /// kthen nje dataset me te dhenat e nxenesve te klasave te zgjedhura
        /// </summary>
        /// <returns></returns>
        private DataSet KerkoNxenesit()
        {
            string strSql = "SELECT AMZA_EMRI, AMZA_MBIEMRI, AMZA_NR_AMZA, AMZA_CIKLI, AMZA_KLASA, AMZA_INDEKSI FROM TBL_AMZA " + 
                " WHERE AMZA_VITI_SHKOLLOR = '" + vitiShkollor + "' AND (";
            int count = 0;
            foreach (Control c in panelExKlasat.Controls)
            {
                CheckBox cb = c as CheckBox;
                if (cb.Checked)
                {
                    if (cb.Name == "cbAll")
                        continue;
                    else
                    {
                        count++;
                        if (count == 1)
                            strSql += "AMZA_KLASA = " + cb.Tag;
                        else
                            strSql += " OR AMZA_KLASA = " + cb.Tag;
                    }
                }
            }

            strSql += " ) ORDER BY CONVERT(INT, AMZA_KLASA), AMZA_INDEKSI, AMZA_EMRI, AMZA_MBIEMRI ";
            return conObject.ExecuteQuery(strSql);
        }

        public string GetVolumeSerial(string strDriveLetter)
        {
            uint serNum = 0;
            uint maxCompLen = 0;
            StringBuilder VolLabel = new StringBuilder(256); // Label
            UInt32 VolFlags = new UInt32();
            StringBuilder FSName = new StringBuilder(256); // File System Name
            strDriveLetter += ":\\"; // fix up the passed-in drive letter for the API call
            long Ret = GetVolumeInformation(strDriveLetter, VolLabel, (UInt32)VolLabel.Capacity, ref serNum, ref maxCompLen, ref VolFlags, FSName, (UInt32)FSName.Capacity);
            return Decode(serNum.ToString());
        }

        public string Decode(string str)
        {
            double d = Convert.ToDouble(str);
            d = d - 333333333;
            d = d / 2;
            d = d * 7;
            int i = Convert.ToInt32(Math.Truncate(d / 13));
            return i.ToString();
        }

        private void AddConditionalFormatting()
        {
            GridEXFormatCondition fc = new GridEXFormatCondition(
                this.gridEXSMS.RootTable.Columns["BOSH"],
                ConditionOperator.Equal, true);
            fc.FormatStyle.ForeColor = Color.Navy;
            this.gridEXSMS.RootTable.FormatConditions.Add(fc);

            fc = new GridEXFormatCondition(
                this.gridEXSMS.RootTable.Columns["TELEFON"],
                ConditionOperator.Equal, "");
            fc.FormatStyle.ForeColor = Color.FromArgb(192, 0, 0);
            this.gridEXSMS.RootTable.FormatConditions.Add(fc);

        }

        /// <summary>
        /// Dergon sms te nje numer telefoni 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="sms"></param>
        /// <param name="from"></param>
        /// <returns>raportin e dergimit te SMS</returns>
        private string DergoMesazh(string to, string sms, string from)
        {
            try
            {
                sms = sms.Replace("ë", "e");
                sms = sms.Replace("ç", "c");
                sms = sms.Replace("Ç", "C");
                sms = sms.Replace("Ë", "E");
                WebClient client = new WebClient();
                //Add a user agent header in case the requested URI contains a query.

                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)");
                client.QueryString.Add("user", "besa");
                client.QueryString.Add("password", "BEMZhxz8");
                client.QueryString.Add("api_id", "2936194");
                client.QueryString.Add("to", to);
                client.QueryString.Add("from", from);
                client.QueryString.Add("deliv_ack", "1");
                client.QueryString.Add("text", sms);
                if (sms.Length <= 160)
                    client.QueryString.Add("concat", "1");
                else 
                    client.QueryString.Add("concat", "3");

                String baseurl = "http://api.clickatell.com/http/sendmsg";
                System.IO.Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                String s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return s;
            }
            catch (WebException ex)
            {
                return "Linja e internetit është e shkëputur";
            }
        }

        private void ShfaqSMS()
        {
            dsSMS.Tables[0].Rows.Clear();
            this.Cursor = Cursors.WaitCursor;
            DataSet dsNotat = KerkoNotat();
            DataSet dsAmza = KerkoNxenesit();
            int step = dsAmza.Tables[0].Rows.Count / 100;
            int i = 0;
            toolStripProgressBar1.Value = 0;
            error.SetError(txtSMS, "");
            if (rbTekst.Checked && txtSMS.Text.Length > 320)
            {
                error.SetError(txtSMS, "Teksti i mesazhit është shumë i gjatë!");
                return;
            }
            foreach (DataRow dr in dsAmza.Tables[0].Rows)
            {

                string sms = dr["AMZA_EMRI"].ToString().ToUpper().Trim() + " " +
                    dr["AMZA_MBIEMRI"].ToString().ToUpper().Trim();
                string amza = dr["AMZA_NR_AMZA"].ToString();
                bool cikli = Convert.ToBoolean(dr["AMZA_CIKLI"].ToString());
                object[] celes = new object[2];
                celes[0] = amza;
                celes[1] = cikli;
                DataRow drSearch = dsTelefon.Tables[0].Rows.Find(celes);
                //nqs nuk eshte i regjistruar ne www.notat.info mos e shfaq nqs nuk eshte uncheck alternativa
                if (cbPaRegjistruar.Checked && drSearch == null)
                    continue;
                //nqs nuk ka numer telefoni te regjistruar mos e shfaq
                if (cbPaRegjistruar.Checked && drSearch["TELEFON"].ToString().Trim() == "")
                    continue;
                if (rbMungesa.Checked || rbTekst.Checked)
                {
                    DataRow newSMS = dsSMS.Tables[0].NewRow();
                    if (rbTekst.Checked)
                    {
                        sms = txtSMS.Text;
                    }
                    else
                    {
                        DataRow[] mungesat = dsNotat.Tables[0].Select("NT_NR_AMZA = '" + amza + "' AND " +
                         " NT_CIKLI = " + Convert.ToInt32(cikli) + " AND NT_DATA = '" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                        if (mungesat.Length == 0)
                            continue;
                        sms = "Nxenesi " + sms + " nuk eshte paraqitur sot ne shkolle. Ju lutem na merrni ne telefon!" +
                            DateTime.Now.ToString("dd.MM.yyyy");
                    }
                    newSMS["AMZA"] = amza;
                    newSMS["CIKLI"] = cikli;
                    newSMS["KLASA"] = Convert.ToInt32(dr["AMZA_KLASA"].ToString());
                    newSMS["INDEKSI"] = dr["AMZA_INDEKSI"].ToString();
                    if (Convert.ToInt32(dr["AMZA_KLASA"]) >= 10)
                        newSMS["KLASA_STR"] = dr["AMZA_KLASA"].ToString() + " - " + dr["AMZA_INDEKSI"].ToString();
                    else
                        newSMS["KLASA_STR"] = "0" + dr["AMZA_KLASA"].ToString() + " - " + dr["AMZA_INDEKSI"].ToString();
                    
                    newSMS["SMS"] = sms;
                    if (drSearch != null)
                    {
                        newSMS["TELEFON"] = drSearch["TELEFON"].ToString();
                        newSMS["CHECKED"] = true;
                    }
                    else
                    {
                        newSMS["TELEFON"] = "";
                        newSMS["CHECKED"] = false;

                    }
                    newSMS["EMRI"] = dr["AMZA_EMRI"].ToString().Trim();
                    newSMS["MBIEMRI"] = dr["AMZA_MBIEMRI"].ToString().Trim();
                    newSMS["GJATESIA"] = sms.Length.ToString();
                    newSMS["BOSH"] = false;
                    newSMS["CHECKED"] = true;
                    dsSMS.Tables[0].Rows.Add(newSMS);
                    dsSMS.AcceptChanges();
                }
                //nota
                else
                {
                    DataRow[] notat = dsNotat.Tables[0].Select("NT_NR_AMZA = '" + amza + "' AND " +
                     " NT_CIKLI = " + Convert.ToInt32(cikli));
                    if (notat.Length == 0 && cbPaNota.Checked)
                        continue;
                    if (rbSemestri1.Checked)
                        sms += " Notat semestri 1";
                    else if (rbSemestri2.Checked)
                        sms += " Notat semestri 2";
                    else if (rbVjetore.Checked)
                        sms += " Notat vjetore";
                    string lenda = "";
                    DataRow newSMS = dsSMS.Tables[0].NewRow();
                    if (notat.Length != 0)
                    {
                        foreach (DataRow rNota in notat)
                        {
                            if (lenda == rNota["NT_EMRI_LENDA"].ToString())
                            {
                                sms += " " + rNota["NT_VLERESIMI"].ToString();
                            }
                            else
                            {
                                lenda = rNota["NT_EMRI_LENDA"].ToString();
                                sms += Environment.NewLine + this.FormatoLende(lenda) + " " + rNota["NT_VLERESIMI"].ToString();
                            }
                        }
                    }
                    newSMS["AMZA"] = amza;
                    newSMS["CIKLI"] = cikli;
                    newSMS["KLASA"] = Convert.ToInt32(dr["AMZA_KLASA"].ToString());
                    newSMS["INDEKSI"] = dr["AMZA_INDEKSI"].ToString();
                    if (Convert.ToInt32(dr["AMZA_KLASA"]) >= 10)
                        newSMS["KLASA_STR"] = dr["AMZA_KLASA"].ToString() + " - " + dr["AMZA_INDEKSI"].ToString();
                    else
                        newSMS["KLASA_STR"] = "0" + dr["AMZA_KLASA"].ToString() + " - " + dr["AMZA_INDEKSI"].ToString();
                    if (dr["AMZA_KLASA"].ToString() == "1" || dr["AMZA_KLASA"].ToString() == "2")
                    {
                        sms = sms.Replace(" 4", " D");
                        sms = sms.Replace(" 5", " K");
                        sms = sms.Replace(" 6", " K");
                        sms = sms.Replace(" 7", " M");
                        sms = sms.Replace(" 8", " M");
                        sms = sms.Replace(" 9", " S");
                        sms = sms.Replace(" 10", " S");
                    }
                    newSMS["SMS"] = sms;
                    if (drSearch != null)
                    {
                        newSMS["TELEFON"] = drSearch["TELEFON"].ToString();
                        newSMS["CHECKED"] = true;
                    }
                    else
                    {
                        newSMS["TELEFON"] = "";
                        newSMS["CHECKED"] = false;

                    }
                    newSMS["EMRI"] = dr["AMZA_EMRI"].ToString().Trim();
                    newSMS["MBIEMRI"] = dr["AMZA_MBIEMRI"].ToString().Trim();
                    newSMS["GJATESIA"] = sms.Length.ToString();
                    if (notat.Length == 0)
                    {
                        newSMS["BOSH"] = true;
                        newSMS["CHECKED"] = false;
                    }
                    else
                    {
                        newSMS["BOSH"] = false;
                        if (newSMS["TELEFON"].ToString() != "")
                            newSMS["CHECKED"] = true;
                    }
                    dsSMS.Tables[0].Rows.Add(newSMS);
                    dsSMS.AcceptChanges();
                }

                if (i % (step + 1) == 0 && toolStripProgressBar1.Value != 100)
                {
                    this.toolStripProgressBar1.Value++;
                }
                i++;
            }
            this.toolStripProgressBar1.Value = 100;
            gridEXSMS.DataSource = dsSMS.Tables[0];
        }

        private void ShfaqDergesa()
        {
            string strSQL;
            if (rbNotaMomentale.Checked)
                strSQL = "SELECT *, DERGUAR + PADERGUAR  AS TOTAL FROM DERGESA WHERE DATA_DERGESA BETWEEN '" +
                    dtpFillimi.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpMbarimi.Value.ToString("yyyy-MM-dd") + "' ORDER BY DATA_DERGESA";
            else
                strSQL = "SELECT *, DERGUAR + PADERGUAR  AS TOTAL FROM DERGESA ORDER BY DATA_DERGESA";
            DataSet dsDergesa = conObject.ExecuteQuery(strSQL);
            gridEXDergesa.DataSource = dsDergesa.Tables[0];
        }

        private void ShfaqBlerje()
        {
            string strSql;
            if (rbNotaMomentale.Checked)
            strSql = "SELECT *, SASIA - KONSUMUAR AS GJENDJE FROM BLERJE WHERE DATA_BLERJE BETWEEN '" +
                dtpFillimi.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpMbarimi.Value.ToString("yyyy-MM-dd") + "' ORDER BY DATA_BLERJE";
            else
                strSql = "SELECT *, SASIA - KONSUMUAR AS GJENDJE FROM BLERJE ORDER BY DATA_BLERJE";

            DataSet dsBlerje = conObject.ExecuteQuery(strSql);
            gridEXBlerje.DataSource = dsBlerje.Tables[0];
        }

        private void RegjistroDergese(int derguar, int paderguar)
        {
            //regjistrimi i dergeses
            string strSql = "INSERT INTO DERGESA VALUES('" + DateTime.Now.ToString("yyyy-MM-dd") + "', " + derguar + ", " + paderguar + ",'Shkolla Manager')";
            conObject.ExecuteNonQuery(strSql);

            //modifikimi i llogarise
            int nr = derguar;
            int i = 0;
            strSql = "SELECT *, (SASIA - KONSUMUAR) AS MBETUR FROM BLERJE WHERE (SASIA - KONSUMUAR) > 0";
            DataSet dsBlerje = conObject.ExecuteQuery(strSql);
            while (nr > 0)
            {
                int sasiaMod = Convert.ToInt32(dsBlerje.Tables[0].Rows[i]["MBETUR"]);
                int idBlerja = Convert.ToInt32(dsBlerje.Tables[0].Rows[i]["ID_BLERJE"]);
                if (nr >= sasiaMod)
                {
                    strSql = "UPDATE BLERJE SET KONSUMUAR = SASIA WHERE ID_BLERJE = " + idBlerja;
                    conObject.ExecuteNonQuery(strSql);
                }
                else
                {
                    strSql = "UPDATE BLERJE SET KONSUMUAR = KONSUMUAR + " + nr + " WHERE ID_BLERJE = " + idBlerja;
                    conObject.ExecuteNonQuery(strSql);
                }
                nr = nr - sasiaMod;
                i++;
            }

            KonvertoSinkronizoNeWebDergesa();
            KonvertoSinkronizoNeWebBlerje();
        }

        private void KonvertoSinkronizoNeWebDergesa()
        {
            string[] fusha = new string[5];
            string[] llojet = new string[5];
            fusha[0] = "ID_DERGESA";
            fusha[1] = "DATA_DERGESA";
            fusha[2] = "DERGUAR";
            fusha[3] = "PADERGUAR";
            fusha[4] = "PROGRAMI";

            llojet[0] = "char(255)";
            llojet[1] = "char(255)";
            llojet[2] = "char(255)";
            llojet[3] = "char(255)";
            llojet[4] = "char(255)";
            KlaseExcel excel = new KlaseExcel("DERGESA.xls", fusha, llojet);
            DataSet dsDergesa = conObject.ExecuteQuery("SELECT * FROM DERGESA WHERE YEAR(DATA_DERGESA) = YEAR(GETDATE())");
            excel.ShkruajTabele("DERGESA.xls", fusha, llojet, dsDergesa);

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

        private void KonvertoSinkronizoNeWebBlerje()
        {
            string[] fusha = new string[4];
            string[] llojet = new string[4];
            fusha[0] = "ID_BLERJE";
            fusha[1] = "DATA_BLERJE";
            fusha[2] = "SASIA";
            fusha[3] = "KONSUMUAR";

            llojet[0] = "char(255)";
            llojet[1] = "char(255)";
            llojet[2] = "char(255)";
            llojet[3] = "char(255)";
            KlaseExcel excel = new KlaseExcel("BLERJE.xls", fusha, llojet);
            DataSet dsKerkesa = conObject.ExecuteQuery("SELECT * FROM BLERJE WHERE YEAR(DATA_BLERJE) = YEAR(GETDATE())");
            excel.ShkruajTabele("BLERJE.xls", fusha, llojet, dsKerkesa);

        }

        private void ShfaqKerkesa()
        {
            string strSql;
            if (rbNotaMomentale.Checked)
                strSql = "SELECT * FROM KERKESA WHERE (DATA_KERKESA BETWEEN '" +
                    dtpFillimi.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpMbarimi.Value.ToString("yyyy-MM-dd") + "') " +
                    " OR (DATEDIFF(dd, DATA_KERKESA, '" + dtpFillimi.Value.ToString("yyyy-MM-dd") + "')= 0 ) OR " +
                    " (DATEDIFF(dd, DATA_KERKESA, '" + dtpMbarimi.Value.ToString("yyyy-MM-dd") + "')= 0 )" + 
                    " ORDER BY DATA_KERKESA";
            else
                strSql = "SELECT * FROM KERKESA ORDER BY DATA_KERKESA";

            DataSet dsKerkesa = conObject.ExecuteQuery(strSql);
            this.gridEXKerkesa.DataSource = dsKerkesa.Tables[0];
        }

        private int GjejGjendjenSMS()
        {
            string strSql = "SELECT SUM(SASIA - KONSUMUAR)AS GJENDJA FROM BLERJE";
            DataSet dsGjendje = conObject.ExecuteQuery(strSql);
            if (dsGjendje.Tables[0].Rows[0]["GJENDJA"].ToString() == "")
                return 0;
            else
                return Convert.ToInt32(dsGjendje.Tables[0].Rows[0]["GJENDJA"].ToString());
        }

        private string FormatoLende(string lenda)
        {
            string strLenda = "";
            char[] c = new char[1];
            c[0] = ' ';
            string[] arrL = lenda.Split(c, 3, StringSplitOptions.None);
            if (arrL.Length == 1)
                strLenda = arrL[0].Substring(0, 4);

            if (arrL.Length == 2)
                strLenda = arrL[0].Substring(0, 4) + " " + arrL[1].Substring(0, 4);

            if (arrL.Length == 3)
                strLenda = arrL[0].Substring(0, 4) + " " + arrL[2].Substring(0, 4);

            strLenda = strLenda.Substring(0, 1).ToUpper() + strLenda.Substring(1);
            return strLenda;
        }

        private void Dergo()
        {
            try
            {
                FTPFactory ff = new FTPFactory();
                ff.setDebug(true);
                ff.setRemoteHost("www.notat.info");
                ff.setRemoteUser("433681@aruba.it");
                ff.setRemotePass("276cfa697c");
                ff.login();

                ff.chdir("notat.info");
                ff.chdir("public");
                ff.chdir("Databases");
                ff.chdir(Main.serial);

                ff.setBinaryMode(true);
                ff.upload(Application.StartupPath + @"\\Excel\SMSExcel.zip");
                ff.close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nuk mund të lidheni me serverin!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                   
        #endregion

        #region Event Handlers
        private void btnShfaqDergesa_Click(object sender, EventArgs e)
        {
            try
            {
                error.SetError(dtpFillimi, "");
                if (dtpFillimi.Value.Date > dtpMbarimi.Value.Date)
                {
                    error.SetError(dtpFillimi, "Data e fillimit nuk duhet të jetë më e madhe se data e mbarimit!");
                    return;
                }
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        ShfaqSMS();
                        break;
                    case 1:
                        ShfaqDergesa();
                        break;
                    case 2:
                        ShfaqBlerje();
                        break;
                    case 3:
                        ShfaqKerkesa();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }
       
        private void Main_Load(object sender, EventArgs e)
        {
            //provo lidhjen me bazen
            try
            {
                conObject = new ConnDbase();
                Main.serial = GetVolumeSerial("C");
                //Main.serial = "4995274";

                NgarkoSkedare frm = new NgarkoSkedare();
                frm.ShowDialog();
                if (!frm.ngarkuar)
                    Application.Exit();
                //dsTelefon mban te gjithe nr e telefonit
                dsTelefon = frm.dsTelefon.Copy();
                gridEXSMS.DataSource = dsSMS.Tables[0];
                AddConditionalFormatting();
                
            }            
            catch (Exception ex)
            {
                if (ex.Message == "Nuk mund të kryhet lidhja me bazën e të dhënave!")
                {
                    MessageBox.Show(ex.Message + Environment.NewLine
                        + "Programi do të mbyllet!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            //bej inicializimet e duhura
            dtpMbarimi.Value = DateTime.Now;
            dtpFillimi.Value = DateTime.Now.AddDays(-6);
            vitiShkollor = GjejVitinAktual();
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked)
            {
                cb1.Checked = true;
                cb2.Checked = true;
                cb3.Checked = true;
                cb4.Checked = true;
                cb5.Checked = true;
                cb6.Checked = true;
                cb7.Checked = true; 
                cb8.Checked = true; 
                cb9.Checked = true; 
                cb10.Checked = true;
                cb11.Checked = true;
                cb12.Checked = true;
            }
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (!cb.Checked)
                cbAll.Checked = false;
        }

        private void lnTeGjithe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataRow dr in dsSMS.Tables[0].Rows)
            {
                dr["CHECKED"] = true;
            }
            dsSMS.AcceptChanges();
        }

        private void lnAsnje_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataRow dr in dsSMS.Tables[0].Rows)
            {
                dr["CHECKED"] = false;
            }
            dsSMS.AcceptChanges();
        }

        private void btnDergo_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Jeni të sigurt që doni të dërgoni të gjithë sms e zgjedhur?", Text,
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
                return;
            int derguar = 0;
            int paderguar = 0;
            int extra = 0;
            try
            {
                Cursor = Cursors.WaitCursor;
                if (gridEXSMS.RowCount == 0)
                {
                    MessageBox.Show("Nuk keni të afishuar asnjë sms!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (gridEXSMS.GetTotalRow().Cells["CHECKED"].Text == "0")
                {
                    MessageBox.Show("Nuk keni zgjedhur asnje prej nxënësve për t'i dërguar sms!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int gjendja = GjejGjendjenSMS();
                if (gjendja < Convert.ToInt32(gridEXSMS.GetTotalRow().Cells["CHECKED"].Text))
                {
                    MessageBox.Show("Numri i krediteve që keni në dispozicion nuk mjafton për të dërguar të gjithë SMS të zgjedhur!" + 
                        Environment.NewLine + "Gjendja aktuale e krediteve është " + gjendja.ToString() + " SMS", 
                        Text,MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int step = Convert.ToInt32(gridEXSMS.GetTotalRow().Cells["CHECKED"].Text) / 100;
                int i = 0;
                toolStripProgressBar1.Value = 0;
                int increment = 100 / Convert.ToInt32(gridEXSMS.GetTotalRow().Cells["CHECKED"].Text);
                string from = Main.shkurtimiShkolla;
                foreach (DataRow dr in dsSMS.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["CHECKED"]) == true && dr["TELEFON"].ToString() != "")
                    {
                        string to = dr["TELEFON"].ToString();
                        string sms = dr["SMS"].ToString();
                        string raporti = DergoMesazh(to, sms, from);
                        //string raporti = "ID:";
                        //System.Threading.Thread.Sleep(20);
                        if (raporti.StartsWith("ID"))
                        {
                            derguar++;
                            if (sms.Length > 160)
                                extra++;
                            else if (sms.Length > 320)
                                extra = extra + 2;
                            else
                            {
                                extra = extra + Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(sms.Length / 160)));
                            }
                        }
                        else
                            paderguar++;
                        if (step > 0)
                        {
                            if (i % (step + 1) == 0 && toolStripProgressBar1.Value != 100)
                            {
                                this.toolStripProgressBar1.Value++;
                            }
                        }
                        else
                        {
                            this.toolStripProgressBar1.Value += increment;
                        }
                        i++;
                    }
                }
                toolStripProgressBar1.Value = 100;
                int gjithe = derguar + paderguar;

                MessageBox.Show("Nga " + gjithe.ToString() + " mesazhe " + derguar + " u arrit që të dërgoheshin kurse " + paderguar + " të tjerë nuk u dërguan!",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                RegjistroDergese(derguar + extra, paderguar);
            }
        }

        private void gridEXSMS_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "CHECKED")
            {
                int i = gridEXSMS.Row;
                if (gridEXSMS.GetRow(i).Cells["TELEFON"].Text == "")
                {
                    if (Convert.ToBoolean(gridEXSMS.GetRow(i).Cells["CHECKED"].Value) == true)
                    {
                        DialogResult r = MessageBox.Show("Nuk mund të dërgoni sms për nxënësin nqs nuk është ruajtur për të një numër telefoni.",
                            Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (r == DialogResult.OK)
                        {
                            gridEXSMS.GetRow(i).Cells["CHECKED"].Value = false;
                            gridEXSMS.Refetch();
                        }
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    rbNotaMomentale.Checked = true;
                    rbNotaMomentale.Text = "Nota momentale";
                    rbMungesa.Show();
                    rbSemestri1.Show();
                    rbSemestri2.Show();
                    rbVjetore.Show();
                    rbTekst.Show();
                    txtSMS.Show();
                    panelExKlasat.Show();
                    cbPaNota.Show();
                    cbPaRegjistruar.Show();
                    btnShfaqDergesa.Text = "Shfaq SMS";
                    panelEx3.Show();
                    gridEXSMS.DataSource = null;
                    break;
                case 1:
                    rbNotaMomentale.Checked = true;
                    rbNotaMomentale.Text = "Dërgesat midis datave";
                    rbMungesa.Hide();
                    rbSemestri1.Hide();
                    rbSemestri2.Hide();
                    rbVjetore.Hide();
                    panelExKlasat.Hide();
                    cbPaNota.Hide();
                    cbPaRegjistruar.Hide();
                    btnShfaqDergesa.Text = "Shfaq dërgesa";
                    panelEx3.Hide();
                    gridEXDergesa.DataSource = null;
                    rbNotaMomentale.Checked = false;
                    rbTekst.Hide();
                    txtSMS.Hide();
                    ShfaqDergesa();
                    break;
                case 2:
                    rbNotaMomentale.Checked = true;
                    rbNotaMomentale.Text = "Blerjet midis datave";
                    rbMungesa.Hide();
                    rbSemestri1.Hide();
                    rbSemestri2.Hide();
                    rbVjetore.Hide();
                    panelExKlasat.Hide();
                    cbPaNota.Hide();
                    cbPaRegjistruar.Hide();
                    btnShfaqDergesa.Text = "Shfaq blerjet";
                    panelEx3.Hide();
                    gridEXBlerje.DataSource = null;
                    rbNotaMomentale.Checked = false;
                    rbTekst.Hide();
                    txtSMS.Hide();
                    ShfaqBlerje();
                    break;
                case 3:
                    rbNotaMomentale.Checked = true;
                    rbNotaMomentale.Text = "Kërkesat midis datave";
                    rbMungesa.Hide();
                    rbSemestri1.Hide();
                    rbSemestri2.Hide();
                    rbVjetore.Hide();
                    panelExKlasat.Hide();
                    cbPaNota.Hide();
                    cbPaRegjistruar.Hide();
                    btnShfaqDergesa.Text = "Shfaq kërkesa";
                    panelEx3.Hide();
                    rbTekst.Hide();
                    txtSMS.Hide();
                    gridEXKerkesa.DataSource = null;
                    rbNotaMomentale.Checked = false;
                    ShfaqKerkesa();
                    break;
            }
        }

        private void btnKerkeseRe_Click(object sender, EventArgs e)
        {
            KerkeseSMS frm = new KerkeseSMS();
            frm.ShowDialog();
            ShfaqKerkesa();
        }

        private void btnRidergo_Click(object sender, EventArgs e)
        {
            int row = gridEXKerkesa.Row;
            if (row > 0 || row == gridEXKerkesa.RowCount - 1)
            {
                MessageBox.Show("Duhet të zgjidhni më parë një prej kërkesave!", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //ridergo email

        }

        private void btnAktivizo_Click(object sender, EventArgs e)
        {
            try
            {
                int row = gridEXKerkesa.Row;
                if (row < 0 || row == gridEXKerkesa.RowCount - 1)
                {
                    MessageBox.Show("Duhet të zgjidhni më parë një prej kërkesave!", Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //aktivizo kërkesë
                AktivizoKerkese frm = new AktivizoKerkese();
                frm.idKerkesa = Convert.ToInt32(gridEXKerkesa.GetRow(row).Cells["ID_KERKESA"].Text);
                frm.dataKerkesa = Convert.ToDateTime(gridEXKerkesa.GetRow(row).Cells["DATA_KERKESA"].Value);
                frm.nrSMS = Convert.ToInt32(gridEXKerkesa.GetRow(row).Cells["SASIA"].Text);
                frm.alg = Convert.ToInt32(gridEXKerkesa.GetRow(row).Cells["ALG"].Text);
                frm.ShowDialog();
                //kthe kerkesen ne blerje
                if (frm.sukses)
                {
                    int idKerkesa = frm.idKerkesa;
                    int sasia = frm.nrSMS;
                    string strSql = "DELETE FROM KERKESA WHERE ID_KERKESA = " + idKerkesa.ToString();
                    conObject.ExecuteNonQuery(strSql);
                    strSql = "INSERT INTO BLERJE VALUES('" + DateTime.Now.ToString("yyyy-MM-dd") + "'," + sasia + " , 0)";
                    conObject.ExecuteNonQuery(strSql);
                    KonvertoSinkronizoNeWebBlerje();
                    KonvertoSinkronizoNeWebKerkesa();
                }
                ShfaqKerkesa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Text = this.Text + "--Ju lutemi prisni sa të përfundojë sinkronizimi me serverin...";
            Cursor = Cursors.WaitCursor;
            ZippClass zipObject = new ZippClass();
            string[] source = new string[3];
            source[0] = Application.StartupPath + "\\Excel\\DERGESA.xls";
            source[1] = Application.StartupPath + "\\Excel\\KERKESA.xls";
            source[2] = Application.StartupPath + "\\Excel\\BLERJE.xls";
            if (File.Exists(source[0]) && File.Exists(source[1]) && File.Exists(source[2]))
            {
                string local = Application.StartupPath + "\\Excel\\SMSExcel.zip";
                zipObject.Zip(local, source);
                try
                {
                    //File.Copy(local, 
                    //    @"C:\Program Files\xampp\htdocs\notat1.info\public\Databases\39942344\SMSExcel.zip", true);
                    Dergo();
                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}
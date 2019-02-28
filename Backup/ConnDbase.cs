using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SMS_Shkolla_Manager
{
    public class ConnDbase
    {
        private SqlConnection conn;

        public ConnDbase()
        {
            string Server = EmerServer();
            string cnStr = "Data Source=" + Server + @"\SHM;Initial Catalog=DProgNec;Integrated Security=True";
            conn = new SqlConnection();
            conn.ConnectionString = cnStr;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Nuk mund të kryhet lidhja me bazën e të dhënave! Nr 1");
            }

        }

        #region Methods
        private string EmerServer()
        {
            Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey hkSoftware = hkcu.OpenSubKey("Software", true);
            Microsoft.Win32.RegistryKey hkShkolla = hkSoftware.OpenSubKey("ShkollaManager");
            return hkShkolla.GetValue("Server").ToString();
        }

        /// <summary>
        /// Ben konvertimin e nje datareaderi ne nje dataset me nje datatable
        /// </summary>
        /// <param name="reader">DataReader-i qe do te konvertohet</param>
        /// <returns>DataSetin qe merret nga konvertimi</returns>
        private DataSet ConvertDataReaderToDataSet(SqlDataReader reader)
        {
            try
            {
                DataSet dataSet = new DataSet();
                do
                {
                    // Krijo nje dataTable te ri

                    DataTable schemaTable = reader.GetSchemaTable();
                    DataTable dataTable = new DataTable();

                    if (schemaTable != null)
                    {
                        // A query returning records was executed

                        for (int i = 0; i < schemaTable.Rows.Count; i++)
                        {
                            DataRow dataRow = schemaTable.Rows[i];
                            // Create a column name that is unique in the data table
                            string columnName = (string)dataRow["ColumnName"]; //+ "<C" + i + "/>";
                            // Add the column definition to the data table
                            DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                            dataTable.Columns.Add(column);
                        }

                        dataSet.Tables.Add(dataTable);

                        // Fill the data table we just created

                        while (reader.Read())
                        {
                            DataRow dataRow = dataTable.NewRow();

                            for (int i = 0; i < reader.FieldCount; i++)
                                dataRow[i] = reader.GetValue(i);

                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    else
                    {
                        // No records were returned

                        DataColumn column = new DataColumn("RowsAffected");
                        dataTable.Columns.Add(column);
                        dataSet.Tables.Add(dataTable);
                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = reader.RecordsAffected;
                        dataTable.Rows.Add(dataRow);
                    }
                }
                while (reader.NextResult());
                return dataSet;
            }
            catch (Exception ex)
            {
                DataSet ds = null;
                return ds;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet ExecuteQuery(string strSql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSql;
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return ConvertDataReaderToDataSet(reader);
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw new Exception("Gabim në aksesimin e bazës së të dhënave! Nr 2");
            }
        }

        public int ExecuteNonQuery(string strSql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSql;
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                int r = cmd.ExecuteNonQuery();
                return r;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw new Exception("Gabim në aksesimin e bazës së të dhënave! Nr 3");
            }
        }
        #endregion
    }
}

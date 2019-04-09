using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Segment01

{
    public partial class Manage : Form
    {
        string conn = @"Data Source=NGOCTHE\SQLEXPRESS;Initial Catalog=QuanLyThuVien;Integrated Security=True";
        SqlConnection sqlConn;
        SqlCommand sqlComm;
        SqlDataReader dataReader;

        public Manage()
        {
            InitializeComponent();
            addDataToComboBox();
            showData();
        }

        private void addDataToComboBox()
        {
            sqlConn = new SqlConnection(conn);
            sqlConn.Open();
            string sqlQuery = "SELECT type_name " +
                                "FROM TypeBook";

            sqlComm = new SqlCommand(sqlQuery, sqlConn);
            dataReader = sqlComm.ExecuteReader();

            while (dataReader.Read())
            {
                comboBox.Items.Add(dataReader["type_name"]);
            }

            dataReader.Close();
        }

        private void showData()
        {
            try
            {
               
                sqlConn = new SqlConnection(conn);
                sqlConn.Open();

                string sqlQuery = "SELECT B.id_book, B.name, B.author, B.number, T.type_name " +
                    "FROM Book as B, TypeBook as T " +
                    "WHERE B.id_type = T.id_type";

                sqlComm = new SqlCommand(sqlQuery, sqlConn);
                dataReader = sqlComm.ExecuteReader();

                DataTable db = new DataTable();
                db.Load(dataReader);

                dataGridView.DataSource = db;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            showData();
        }
    }
}

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
        int index = 0;
        int indexSearch = 0;
        string conn = @"Data Source=NGOCTHE\SQLEXPRESS;Initial Catalog=QuanLyThuVien;Integrated Security=True";
        SqlConnection sqlConn;
        SqlCommand sqlComm;
        SqlDataReader dataReader;
        SqlDataAdapter dataAdapter;
        
        public Manage()
        {
            InitializeComponent();
            addDataToComboBox();
            showData();
        }

        private void comboBoxSelected_Index(object sender, EventArgs e)
        {
            index = comboBox.SelectedIndex;
        }

        private void Selected_Search(object sender, EventArgs e)
        {
            indexSearch = comboxSearch.SelectedIndex;
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
                comboxSearch.Items.Add(dataReader["type_name"]);
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

            finally
            {
                sqlConn.Close();
            }
        }

        private void addData()
        {
            try
            {
                sqlConn = new SqlConnection(conn);
                sqlConn.Open();

                string idBook = tbIdBook.Text.Trim();
                string name = tbName.Text.Trim();
                string author = tbAuthor.Text.Trim();
                string amount = tbAmount.Text.Trim();
                string type = (index + 1).ToString();

                string sqlQuery = "INSERT INTO Book VALUES(@id_book, @name, @author, @number, @id_type)";

                sqlComm = new SqlCommand(sqlQuery, sqlConn);

                sqlComm.Parameters.AddWithValue("id_book", idBook);
                sqlComm.Parameters.AddWithValue("name", name);
                sqlComm.Parameters.AddWithValue("author", author);
                sqlComm.Parameters.AddWithValue("number", amount);
                sqlComm.Parameters.AddWithValue("id_type", type);
                sqlComm.ExecuteNonQuery();
                showData();
            }
            catch (Exception ex)
            {

            }

            finally
            {
                sqlConn.Close();
            }
        }

        private void updateData()
        {
            try
            {
                sqlConn = new SqlConnection(conn);
                sqlConn.Open();

                string idBook = tbIdBook.Text.Trim();
                string name = tbName.Text.Trim();
                string author = tbAuthor.Text.Trim();
                string amount = tbAmount.Text.Trim();
                string type = (index + 1).ToString();
                
                string sqlQuery = "UPDATE Book SET (id_book = @idBook, name = @name, " +
                    "author = @author, number = @number, id_type = @id_type WHERE id_book = @id_book)";

                sqlComm = new SqlCommand(sqlQuery, sqlConn);

                sqlComm.Parameters.AddWithValue("id_book", idBook);
                sqlComm.Parameters.AddWithValue("name", name);
                sqlComm.Parameters.AddWithValue("author", author);
                sqlComm.Parameters.AddWithValue("number", amount);
                sqlComm.Parameters.AddWithValue("id_type", type);
                sqlComm.ExecuteNonQuery();
                showData();
            }
            catch (Exception ex)
            {

            }

            finally
            {
                sqlConn.Close();
            }
        }

        private void searchBook()
        {
            try
            {
                sqlConn = new SqlConnection(conn);
                sqlConn.Open();

                string sql = "SELECT B.id_book, B.name, B.author, B.number, T.type_name FROM Book as B, TypeBook as T WHERE B.id_type = T.id_type AND B.id_type =" +" '" + (indexSearch + 1) + "'";
                sqlComm = new SqlCommand(sql, sqlConn);
                dataReader = sqlComm.ExecuteReader();

                DataTable db = new DataTable();
                db.Load(dataReader);

                dataGridView.DataSource = db;
            }
            catch (Exception ex)
            {

            }

            finally
            {
                sqlConn.Close();
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            showData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Main main = new Main();
            main.Show();
        }

        private void Cell_Click(object sender, DataGridViewCellEventArgs dt)
        {
            tbIdBook.Text = dataGridView.Rows[dt.RowIndex].Cells["id_book"].FormattedValue.ToString();
            tbName.Text = dataGridView.Rows[dt.RowIndex].Cells["name"].FormattedValue.ToString();
            tbAuthor.Text = dataGridView.Rows[dt.RowIndex].Cells["author"].FormattedValue.ToString();
            tbAmount.Text = dataGridView.Rows[dt.RowIndex].Cells["number"].FormattedValue.ToString();
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchBook();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateData();
        }
    }
}

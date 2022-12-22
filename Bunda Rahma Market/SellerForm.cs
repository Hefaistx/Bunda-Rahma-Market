using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bunda_Rahma_Market
{
    public partial class SellerForm : Form
    {
        DBConnect DBCon = new DBConnect();
        public SellerForm()
        {
            InitializeComponent();
        }
        private void getTable()
        {
            string selectQuerry = "SELECT * FROM Sellers";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void clear()
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            ageTextBox.Clear();
            phoneTextBox.Clear();
            passwordTextBox.Clear();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Sellers VALUES(" + idTextBox.Text + ",'" + nameTextBox.Text + "','" + ageTextBox.Text + "','" + phoneTextBox.Text + "','" + passwordTextBox.Text + "')";
                SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBCon.CloseCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            try
            {
                if (idTextBox.Text == "" || nameTextBox.Text == "" || ageTextBox.Text == "" || phoneTextBox.Text == "" || passwordTextBox.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    string updateQuery = "UPDATE Sellers SET sellerName='" + nameTextBox.Text + "',sellerAge='" + ageTextBox.Text + "',sellerPhone='" + phoneTextBox.Text + "',sellerPass='" + passwordTextBox.Text + "'WHERE sellerId=" + idTextBox.Text + "";
                    SqlCommand command = new SqlCommand(updateQuery, DBCon.GetCon());
                    DBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DBCon.CloseCon();
                    getTable();
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click_1(object sender, EventArgs e)
        {
            idTextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            ageTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            phoneTextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            passwordTextBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            try
            {
                if (idTextBox.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if ((MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        string deleteQuery = "DELETE FROM Sellers WHERE sellerId=" + idTextBox.Text + "";
                        SqlCommand command = new SqlCommand(deleteQuery, DBCon.GetCon());
                        DBCon.OpenCon();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Seller Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DBCon.CloseCon();
                        getTable();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

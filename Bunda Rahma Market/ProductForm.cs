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
    public partial class ProductForm : Form
    {
        DBConnect DBCon = new DBConnect();
        public ProductForm()
        {
            InitializeComponent();
        }

        private void button_category_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }


        private void getCategory()
        {
            string selectQuerry = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            categoryComboBox.DataSource = table;
            categoryComboBox.ValueMember = "CatName";
            searchComboBox.DataSource = table;
            searchComboBox.ValueMember = "CatName";
        }

        private void getTable()
        {
            string selectQuerry = "SELECT * FROM Products";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void ProductForm_Load_1(object sender, EventArgs e)
        {
            getCategory();
            getTable();
        }
        private void clear()
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            priceTextBox.Clear();
            quantityTextBox.Clear();
            categoryComboBox.SelectedIndex = 0;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Products VALUES(" + idTextBox.Text + ",'" + nameTextBox.Text + "'," + priceTextBox.Text + "," + quantityTextBox.Text + ",'" + categoryComboBox.Text + "')";
                SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBCon.CloseCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (idTextBox.Text == "" || nameTextBox.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    string updateQuery = "UPDATE Products SET prodName='" + nameTextBox.Text + "',prodPrice=" + priceTextBox.Text + ",prodQty=" + quantityTextBox.Text + ",prodCat='" + categoryComboBox.Text + "'WHERE prodId=" + idTextBox.Text + "";
                    SqlCommand command = new SqlCommand(updateQuery, DBCon.GetCon());
                    DBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            idTextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            priceTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            quantityTextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            categoryComboBox.SelectedValue = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (idTextBox.Text == "")
                {
                    MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string deleteQuery = "DELETE FROM Products WHERE prodId=" + idTextBox.Text + "";
                    SqlCommand command = new SqlCommand(deleteQuery, DBCon.GetCon());
                    DBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void refreshButton_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void sellerButton_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }

        private void categoryButton_Click(object sender, EventArgs e)
        {
            CategoryForm categoryz = new CategoryForm();
            categoryz.Show();
            this.Hide();
        }

        private void sellingButton_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }

        private void dataGridView1_Click_1(object sender, EventArgs e)
        {
            idTextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            priceTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            quantityTextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            categoryComboBox.SelectedValue = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void searchComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuerry = "SELECT * FROM Products WHERE prodCat='" + searchComboBox.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}

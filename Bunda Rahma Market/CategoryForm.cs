using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bunda_Rahma_Market
{
    public partial class CategoryForm : Form
    {
        DBConnect DBCon = new DBConnect();
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void getTable()
        {
            string selectQuerry = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Category VALUES("+idTextBox.Text+", '"+nameTextBox.Text+"', '"+descriptionTextBox.Text+"')";
                SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category added succesfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBCon.CloseCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            try
            {
                if (idTextBox.Text == "" || nameTextBox.Text == "" || descriptionTextBox.Text == "")
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "UPDATE Category SET catName='" + nameTextBox.Text + "', catDesc='" + descriptionTextBox.Text + "', catId='" + idTextBox.Text + "'WHERE catId=" + idTextBox.Text + "";
                    SqlCommand command = new SqlCommand(updateQuery, DBCon.GetCon());
                    DBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Category Updated Successfully", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            descriptionTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void clear()
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            descriptionTextBox.Clear();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            try
            {
                string deleteQuery = "DELETE FROM Category WHERE catId=" + idTextBox.Text + "";
                SqlCommand command = new SqlCommand(deleteQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category Deleted Successfully", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBCon.CloseCon();
                getTable();
                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sellerButton_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }

        private void productButton_Click(object sender, EventArgs e)
        {
            ProductForm product = new ProductForm();
            product.Show();
            this.Hide();
        }

        private void sellingButton_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void sellerButton_MouseEnter(object sender, EventArgs e)
        {
            sellerButton.ForeColor = Color.Pink;
        }
        private void sellerButton_MouseLeave(object sender, EventArgs e)
        {
            sellerButton.ForeColor = Color.FromArgb(254, 153, 0);
        }

        private void productButton_MouseEnter(object sender, EventArgs e)
        {
            productButton.ForeColor = Color.Pink;
        }
        private void productButton_MouseLeave(object sender, EventArgs e)
        {
            productButton.ForeColor = Color.FromArgb(254, 153, 0);
        }

        private void sellingButton_MouseEnter(object sender, EventArgs e)
        {
            sellingButton.ForeColor = Color.Pink;
        }
        private void sellingButton_MouseLeave(object sender, EventArgs e)
        {
            sellingButton.ForeColor = Color.FromArgb(254, 153, 0);
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Pink;
        }
        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Red;
        }
        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Red;
        }
        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
        }
    }
}

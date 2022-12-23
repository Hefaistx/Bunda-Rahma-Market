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
    public partial class LoginForm : Form
    {
        DBConnect DBCon = new DBConnect();
        public static string sellerName;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void labelClear_MouseEnter(object sender, EventArgs e)
        {
            labelClear.ForeColor = Color.Red; 
        }

        private void labelClear_MouseLeave(object sender, EventArgs e)
        {
            labelClear.ForeColor = Color.FromArgb(254, 153, 0);
        }


        private void labelClear_Click(object sender, EventArgs e)
        {
            textboxUsername.Clear();
            textboxPassword.Clear();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (textboxUsername.Text == "" || textboxPassword.Text == "")
            {
                MessageBox.Show("Please Enter Username and Password", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (comboBox1.SelectedIndex > -1)
                {
                    if (comboBox1.SelectedItem.ToString() == "ADMIN")
                    {
                        if (textboxUsername.Text == "Admin" && textboxPassword.Text == "Adminhaha123")
                        {
                            ProductForm product = new ProductForm();
                            product.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are Admin, Please Enter the corret Id and Password", "Miss Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        string selectQuery = "SELECT * FROM Sellers WHERE sellerName='" + textboxUsername.Text + "' AND sellerPass='" + textboxPassword.Text + "'";

                        SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, DBCon.GetCon());
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            sellerName = textboxUsername.Text;
                            SellingForm selling = new SellingForm();
                            selling.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please Select Role", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

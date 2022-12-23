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
using DGVPrinterHelper;

namespace Bunda_Rahma_Market
{
    public partial class SellingForm : Form
    {
        DBConnect DBCon = new DBConnect();
        DGVPrinter printer = new DGVPrinter();
        public SellingForm()
        {
            InitializeComponent();
        }
        private void getCategory()
        {
            string selectQuerry = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuerry,DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            categoryComboBox.DataSource = table;
            categoryComboBox.ValueMember = "catName";

        }

        private void getTable()
        {
            string selectQuerry = "SELECT prodName, prodPrice FROM Products";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable tabled = new DataTable();
            adapter.Fill(tabled);
            productDataGridView.DataSource = tabled;
        }
        private void getSellTable()
        {
            string selectQuerry = "SELECT * FROM Bill";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            sellDataGridView.DataSource = table;
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Today.ToShortDateString();
            label6.Text = LoginForm.sellerName;
            getTable();
            getCategory();
            getSellTable();
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            printer.Title = "Bunda Rahma market sell list";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "foxlearn";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(sellDataGridView);
        }

  

        int grandTotal = 0;
        int n = 0;
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == "" || priceTextBox.Text == "")
            {
                MessageBox.Show("Missing Information", "Information Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int Total = Convert.ToInt32(nameTextBox.Text) * Convert.ToInt32(priceTextBox.Text);
                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(DataGridView1);
                addRow.Cells[0].Value = n + 1;
                addRow.Cells[1].Value = idTextBox.Text;
                addRow.Cells[2].Value = nameTextBox.Text;
                addRow.Cells[3].Value = priceTextBox.Text;
                addRow.Cells[4].Value = Total;
                DataGridView1.Rows.Add(addRow);
                grandTotal += Total;
                label3.Text = grandTotal + " Ks";
            }
        }


        private void productDataGridView_Click(object sender, EventArgs e)
        {
            idTextBox.Text = productDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = productDataGridView.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Bill VALUES(" + billIDTextBox.Text + ",'" + label6.Text + "','" + labelDate.Text + "'," + grandTotal.ToString() + ")";
                SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Order Added Successfully", "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBCon.CloseCon();
                getSellTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void logoutButton_MouseEnter(object sender, EventArgs e)
        {
            logoutButton.ForeColor = Color.HotPink;
        }

        private void logoutButton_MouseLeave(object sender, EventArgs e)
        {
            logoutButton.ForeColor = Color.FromArgb(254, 153, 0);
        }

        private void deleteButton_MouseEnter(object sender, EventArgs e)
        {
            deleteButton.ForeColor = Color.Pink;
        }

        private void deleteButton_MouseLeave(object sender, EventArgs e)
        {
            deleteButton.ForeColor = Color.White;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Pink;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void addButton_MouseEnter(object sender, EventArgs e)
        {
            addButton.ForeColor = Color.Pink;
        }
        private void addButton_MouseLeave(object sender, EventArgs e)
        {
            addButton.ForeColor = Color.White;
        }

        private void updateButton_MouseEnter(object sender, EventArgs e)
        {
            updateButton.ForeColor = Color.Pink;
        }
        private void updateButton_MouseLeave(object sender, EventArgs e)
        {
            updateButton.ForeColor = Color.White;
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
            idTextBox.Text = DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            priceTextBox.Text = DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            categoryComboBox.SelectedValue = DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void categoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuerry = "SELECT prodName, prodPrice FROM Products WHERE prodCat='" + categoryComboBox.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            productDataGridView.DataSource = table;
        }

    }
}

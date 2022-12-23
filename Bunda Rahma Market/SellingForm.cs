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
            DataTable table = new DataTable();
            adapter.Fill(table);
            productDataGridView.DataSource = table;
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

        private void productDataGridView_Click(object sender, EventArgs e)
        {
            idTextBox.Text = productDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = productDataGridView.SelectedRows[0].Cells[1].Value.ToString();
        }
        int grandTotal = 0, n = 0;

        private void updateButton_Click(object sender, EventArgs e)
        {
            printer.Title = "Mdemy MiniMarket Sell Lists";
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

        private void categoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuerry = "SELECT prodName, prodPrice FROM Products WHERE prodCat='" + categoryComboBox.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(selectQuerry, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            productDataGridView.DataSource = table;
        }

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
                addRow.CreateCells(dataGridView1);
                addRow.Cells[0].Value = ++n;
                addRow.Cells[1].Value = idTextBox.Text;
                addRow.Cells[2].Value = nameTextBox.Text;
                addRow.Cells[3].Value = priceTextBox.Text;
                addRow.Cells[4].Value = Total;
                dataGridView1.Rows.Add(addRow);
                grandTotal += Total;
                label3.Text = grandTotal + " Ks";
            }
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


    }
}

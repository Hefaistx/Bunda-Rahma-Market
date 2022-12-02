using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bunda_Rahma_Market
{
    public partial class loginForm : Form
    {
        public loginForm()
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

        private void buttonDecor_MouseEnter(object sender, EventArgs e)
        {
            buttonDecor.ForeColor = Color.FromArgb(254, 153, 0);
        }

        private void labelClear_Click(object sender, EventArgs e)
        {
            textboxUsername.Clear();
            textboxPassword.Clear();
        }
    }
}

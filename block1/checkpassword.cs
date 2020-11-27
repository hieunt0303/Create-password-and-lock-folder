using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace block_folder
{
    public partial class checkpassword : Form
    {
        public string pass;
        public bool status;
        public checkpassword()
        {
            status = false;
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(pass))
            {
                status = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Password!!",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
            }
        }
    }
}

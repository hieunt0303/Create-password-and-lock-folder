using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace block_folder
{
    public partial class frmPassword : Form
    {
        public string path;
        public frmPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(txtReenter.Text) && txtPassword.Text.Length != 0)
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem;
                XmlNode xmlnode;
                XmlText xmltext;
                xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmldoc.AppendChild(xmlnode);
                xmlelem = xmldoc.CreateElement("", "ROOT", "");
                xmltext = xmldoc.CreateTextNode(txtPassword.Text);
                xmlelem.AppendChild(xmltext);
                xmldoc.AppendChild(xmlelem);
                xmldoc.Save(path + "\\p.xml");
                this.Close();
            }
            else
            {
                MessageBox.Show("Two text do not match or Blank Password", "Error");
                txtPassword.Clear();
                txtReenter.Clear();
                txtPassword.Focus();
            }
        }
    }
}

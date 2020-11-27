using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace block_folder
{
    public partial class Form1 : Form
    {
        public string status= ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}";
        //bool flag = true;
        string[] arr;
        
        
        private string _pathkey;
        public Form1()
        {
            InitializeComponent();
            arr = new string[1];
            arr[0] = ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}";
        }
        public string pathkey
        {
            get { return _pathkey; }
            set { _pathkey = value; }
        }
        FolderBrowserDialog fldrDialog = new FolderBrowserDialog();
        private void btnSet_Click(object sender, EventArgs e)
        {

            if (fldrDialog.ShowDialog() == DialogResult.OK)
            {

                DirectoryInfo d = new DirectoryInfo(fldrDialog.SelectedPath);
                string selectedpath = d.Parent.FullName + d.Name;
                if (fldrDialog.SelectedPath.LastIndexOf(".{") == -1)
                {
                    if (chkPasswd.Checked)
                        setpassword(fldrDialog.SelectedPath);
                    if (!d.Root.Equals(d.Parent.FullName))
                        d.MoveTo(d.Parent.FullName + "\\" + d.Name + status);
                    else d.MoveTo(d.Parent.FullName + d.Name + status);
                    txtPath.Text = fldrDialog.SelectedPath;

                }
                else
                {
                    status = getstatus(status);
                    bool s = checkpassword();
                    if (s)
                    {
                        File.Delete(fldrDialog.SelectedPath + "\\p.xml");
                        d.MoveTo(fldrDialog.SelectedPath.Substring(0, fldrDialog.SelectedPath.LastIndexOf(".")));

                        txtPath.Text = fldrDialog.SelectedPath.Substring(0, fldrDialog.SelectedPath.LastIndexOf("."));


                    }
                }
            }
        }
        private bool checkpassword()
        {
            XmlTextReader read;
            if (pathkey == null)
                read = new XmlTextReader(fldrDialog.SelectedPath + "\\p.xml");
            else
                read = new XmlTextReader(pathkey + "\\p.xml");
            if (read.ReadState == ReadState.Error)
                return true;
            else
            {
                try
                {
                    while (read.Read())
                        if (read.NodeType == XmlNodeType.Text)
                        {
                            checkpassword c = new checkpassword();
                            c.pass = read.Value;
                            if (c.ShowDialog() == DialogResult.OK)
                            {
                                read.Close();
                                return c.status;
                            }

                        }
                }
                catch { return true; }

            }
            read.Close();
            return false;
        }
        private Boolean setpassword(string path)
        {
            frmPassword p = new frmPassword();
            p.path = path;
            p.ShowDialog();
            return true;
        }
        private string getstatus(string stat)
        {
            for (int i = 0; i < 1; i++)
                if (stat.LastIndexOf(arr[i]) != -1)
                    stat = stat.Substring(stat.LastIndexOf("."));
            return stat;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.pathkey != null)
            {

                DirectoryInfo d = new DirectoryInfo(pathkey);
                string selectedpath = d.Parent.FullName + d.Name;
                if (pathkey.LastIndexOf(".{") == -1)
                {
                    txtPath.Text = pathkey;
                    DialogResult r;
                    r = MessageBox.Show("Do You want to set password ? ",
                             "Question?", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                    {
                        setpassword(pathkey);
                    }
                    status = arr[0];
                    if (!d.Root.Equals(d.Parent.FullName))
                        d.MoveTo(d.Parent.FullName + "\\" + d.Name + status);
                    else d.MoveTo(d.Parent.FullName + d.Name + status);

                }
                else
                {
                    status = getstatus(status);
                    bool s = checkpassword();
                    if (s)
                    {
                        File.Delete(pathkey + "\\p.xml");
                        d.MoveTo(pathkey.Substring(0, pathkey.LastIndexOf(".")));
                        txtPath.Text = pathkey.Substring(0, pathkey.LastIndexOf("."));

                    }
                }
            }
        }
    }
}
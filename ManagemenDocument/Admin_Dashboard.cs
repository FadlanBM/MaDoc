using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenDocument
{
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var fAdd_Doc=new Admin_Add_Doc();
            fAdd_Doc.StartPosition = FormStartPosition.CenterScreen;
            fAdd_Doc.MdiParent = this;
            fAdd_Doc.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var fManagementIden=new Admin_ManagementIdentitas();
            fManagementIden.StartPosition = FormStartPosition.CenterScreen;
            fManagementIden.MdiParent= this;
            fManagementIden.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var fManagementUser=new Admin_ManagementUser();
            fManagementUser.MdiParent = this;
            fManagementUser.StartPosition = FormStartPosition.CenterScreen;
            fManagementUser.Show();
        }
    }
}

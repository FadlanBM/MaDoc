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

        

        private void idnetitasTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fManagement_Identitas=new Admin_ManagementIdentitas();  
            fManagement_Identitas.StartPosition=FormStartPosition.CenterScreen;
            fManagement_Identitas.MdiParent = this;
            fManagement_Identitas.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void documentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FmanagementDoc = new Admin_Management_Doc();
            FmanagementDoc.StartPosition=FormStartPosition.CenterScreen;
            FmanagementDoc.MdiParent = this;
            FmanagementDoc.Show();
        }

        private void belumVerifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FmanagemntUser = new Admin_ManagementUserBelumVerify();
            FmanagemntUser.StartPosition = FormStartPosition.CenterScreen;
            FmanagemntUser.MdiParent = this;
            FmanagemntUser.Show();
        }

        private void verifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FmanagementUserVerify=new Admin_Management_User_Verify();
            FmanagementUserVerify.StartPosition=FormStartPosition.CenterScreen;
            FmanagementUserVerify.MdiParent=this;   
            FmanagementUserVerify.Show();
        }
    }
}

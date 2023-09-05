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
            timer1.Interval = 1000;
            timer1.Tick += timerLoad;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timerLoad(object sender, EventArgs e) {
            toolStripLabel2.Text = DateTime.Now.ToString("dd/MM/yyyy | HH:mm:ss");
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var FaddDokumen = new Admin_Management_Doc();
            FaddDokumen.StartPosition=FormStartPosition.CenterScreen;
            FaddDokumen.MdiParent=this;
            FaddDokumen.Show(); 
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var FverifyUser=new Admin_Management_User_Verify();
            FverifyUser.StartPosition = FormStartPosition.CenterScreen;
            FverifyUser.MdiParent=this;
            FverifyUser.Show();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            new FLogin().Show();
            this.Close();
        }

        private void settingAccuntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Fsettings = new AdminEditAccunt();
            Fsettings.StartPosition = FormStartPosition.CenterScreen;
            Fsettings.MdiParent=this;
            Fsettings.Show();
        }

        private void resetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FresetPass = new FResetPassword();
            FresetPass.StartPosition = FormStartPosition.CenterScreen;
            FresetPass.MdiParent=this;
            FresetPass.Show();  
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda yakin ingin Logout ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
            {
                new FLogin().Show();
                this.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Apakah anda yakin ingin keluar dari applikasi ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
                Environment.Exit(0);
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda yakin ingin Logout ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
            {
                new FLogin().Show();
                this.Close();
            }                
        }

        private void toolStripLabel3_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda yakin ingin keluar dari applikasi ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(DialogResult.Yes==result)
                Environment.Exit(0);
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            var FMangenetDoc = new Admin_Management_Doc();
            FMangenetDoc.MdiParent=this;
            FMangenetDoc.StartPosition= FormStartPosition.CenterParent;
            FMangenetDoc.Show();
        }
    }
}

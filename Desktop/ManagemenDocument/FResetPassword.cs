using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenDocument
{
    public partial class FResetPassword : Form
    {
        AppDbContextDataContext context;
        public FResetPassword()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_passwordOld.Text.Length==0||tb_passwordNew.Text.Length==0||tb_confrimPass.Text.Length==0)
            {
                MessageBox.Show("Form yang anda masukkan belum lengkap", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pass=context.tb_users.Where(p=>p.id_user==Helper.helper.id.id_user).FirstOrDefault();
            if (pass.password==getSHA(tb_passwordOld.Text))
            {
                if (tb_passwordNew.Text == tb_confrimPass.Text) { 
                    pass.password=getSHA(tb_passwordNew.Text);
                    context.SubmitChanges();
                    MessageBox.Show("Berhasil Ubah password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Anda harus login lagi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new FLogin().Show();
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Password comfirm tidak sama", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Password tidak tidak sama dengan yang lama", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
        }


        private string getSHA(string p) { 
            StringBuilder sb=new StringBuilder();
            using (var sha=SHA256.Create())
            {
                var baytes=sha.ComputeHash(Encoding.UTF8.GetBytes(p));
                for (int i=0; i<baytes.Length; i++)
                {
                    sb.Append(baytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

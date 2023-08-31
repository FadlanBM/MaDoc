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
    public partial class FLogin : Form
    {
        private AppDbContextDataContext context;
        public FLogin()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_username.Text.Length == 0 || tb_password.Text.Length == 0) { 

                MessageBox.Show("Form yang an◘da masukkan belum lengkap","Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var user = context.tb_users.Where(u =>u.level==0&&u.username == tb_username.Text ).FirstOrDefault();
            if (user == null) { 
                MessageBox.Show("Username tidak di temukan","Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (user.password==getSHA(tb_password.Text))
            {
                new Admin_Dashboard().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Password yang anda masukkan salah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private string getSHA(string s) {
            StringBuilder sb = new StringBuilder();
            using (var sha=SHA256.Create()) { 
                var baytes=sha.ComputeHash(Encoding.UTF8.GetBytes(s));
                for (int i = 0; i < baytes.Length; i++)
                {
                    sb.Append(baytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }
        
        private void getChapca() { 
            Random rd=new Random();
            var num=rd.Next(6,8);
            var chapca = string.Empty;
            var i= 0;
            do
            {
                var chp = rd.Next(48, 123);
                if ((chp>48&&chp<57)||(chp>57&&chp<90)||(chp>92&&chp<122))
                {
                    chapca = chapca + (char)chp;
                    i++;
                    if (num==i)
                    {
                        break;
                    }
                }
            } while (true);
            lb_chapca.Text = chapca;
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            getChapca();
        }
    }
}

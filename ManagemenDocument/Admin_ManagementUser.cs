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
    public partial class Admin_ManagementUser : Form
    {
        AppDbContextDataContext context;
        public Admin_ManagementUser()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fAddUser=new Admin_AddUser(this.MdiParent);
            fAddUser.StartPosition = FormStartPosition.CenterScreen;
            fAddUser.FormClosing+=(object ss,FormClosingEventArgs ass)=>
            {
                if (DialogResult.OK==fAddUser.DialogResult)
                {
                    
                }
            };
            fAddUser.Show();
        }

        private void loadData() {
            int i = 0;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            var data = (from c in context.tb_users
                        join idn in context.tb_identitas
                        on c.id_identitas equals idn.id_identitas
                        orderby c.name ascending
                        select new
                        {
                            id=c.id_user,
                            name = c.name,
                            noidentitas = c.no_identitas,
                            nameIdentitas = idn.nameIdentitas,
                            alamat = c.alamat,
                            phoneNumber = c.phoneNumber,
                            verify = c.verify == 0 ? "TIdak di verify" : "verify",
                            level = c.level == 0 ? "Admin" : "User",
                        }).ToList() ;

            if (tb_search.Text!="")
            {
                data = data.Where(d => d.name.Contains(tb_search.Text)).ToList();
            }
            foreach (var item in data)
            {
                i++;
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = i;
                dataGridView1.Rows[num].Cells[1].Value = item.id;
                dataGridView1.Rows[num].Cells[2].Value = item.name;
                dataGridView1.Rows[num].Cells[3].Value = item.noidentitas;
                dataGridView1.Rows[num].Cells[4].Value = item.nameIdentitas;
                dataGridView1.Rows[num].Cells[5].Value = item.alamat;
                dataGridView1.Rows[num].Cells[6].Value = item.phoneNumber;
                dataGridView1.Rows[num].Cells[7].Value = item.verify;
                dataGridView1.Rows[num].Cells[8].Value = item.level;
            }

        }

        private void Admin_ManagementUser_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = string.Empty;
            if (e.ColumnIndex == 9)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var fAddUser = new Admin_AddUser(this.MdiParent);
                fAddUser.id=int.Parse(id);
                fAddUser.StartPosition = FormStartPosition.CenterParent;
                fAddUser.FormClosing += (object sa, FormClosingEventArgs sad) =>
                {
                    if (DialogResult.OK==fAddUser.DialogResult)
                    {
                        loadData();
                    }
                };
                fAddUser.Show();
            }
            if (e.ColumnIndex==10)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                DialogResult result = MessageBox.Show(null, "Apakah anda yakin ingin menghapus data ini ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var data =context.tb_users.Where(c=>c.id_user==int.Parse(id)).FirstOrDefault();
                    context.tb_users.DeleteOnSubmit(data);
                    context.SubmitChanges();
                    MessageBox.Show(null, "Berhasil menghapus data", "Informtaion", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    loadData();
                    return;
                }
            }
        }
    }
}

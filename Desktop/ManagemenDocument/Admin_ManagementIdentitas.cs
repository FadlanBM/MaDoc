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
    public partial class Admin_ManagementIdentitas : Form
    {
        AppDbContextDataContext context;
        public Admin_ManagementIdentitas()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fAddIden=new Admin_AddIdentitas(this.MdiParent);
            fAddIden.StartPosition = FormStartPosition.CenterScreen;
            this.Enabled = false;
            fAddIden.FormClosing += (object asd, FormClosingEventArgs asa) =>
            {
                if (DialogResult.OK == fAddIden.DialogResult)
                {
                    this.Enabled = true;
                    LoadData();
                }
                if (DialogResult.Cancel==fAddIden.DialogResult)
                {
                    this.Enabled = true;
                }
            };
            fAddIden.Show();
        }

        private void Admin_ManagementIdentitas_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData() {
            int i = 0;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear(); 
            var data = (from c in context.tb_identitas
                        orderby c.nameIdentitas ascending
                        select new {
                            id=c.id_identitas,
                            name=c.nameIdentitas
                        }).ToList();
            foreach ( var c in data )
            {
                i++;    
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value =i;
                dataGridView1.Rows[num].Cells[1].Value =c.id;
                dataGridView1.Rows[num].Cells[2].Value =c.name;                
            }
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id=string.Empty;
            if (e.ColumnIndex == 3) {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var fAddIden = new Admin_AddIdentitas(this.MdiParent);
                fAddIden.id=int.Parse(id);
                fAddIden.StartPosition= FormStartPosition.CenterParent;
                fAddIden.FormClosing += (object c, FormClosingEventArgs da) =>
                {
                    if (DialogResult.OK == fAddIden.DialogResult)
                    {
                        LoadData();
                    }
                };
                fAddIden.Show();
            }
            if (e.ColumnIndex==4)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                DialogResult dialog = MessageBox.Show(null, "Apakah anda yakin ingin menghapus data ini?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    var data=context.tb_identitas.Where(idn=>idn.id_identitas==int.Parse(id)).FirstOrDefault();
                    if (data != null)
                    {
                        context.tb_identitas.DeleteOnSubmit(data);
                        context.SubmitChanges();
                        MessageBox.Show(null, "Berhasasil Delete data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadData();
                        return;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            var data = (from c in context.tb_identitas
                        orderby c.nameIdentitas ascending
                        select new
                        {
                            id = c.id_identitas,
                            name = c.nameIdentitas
                        }).ToList();
            if (tb_search.Text!="")
            {
                data = data.Where(d => d.name.Contains(tb_search.Text.ToUpper())).ToList();
            }
            foreach (var c in data)
            {
                i++;
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = i;
                dataGridView1.Rows[num].Cells[1].Value = c.id;
                dataGridView1.Rows[num].Cells[2].Value = c.name;
            }
        }
    }
}

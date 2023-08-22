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
    public partial class Admin_Management_Doc : Form
    {

        AppDbContextDataContext context;
        public Admin_Management_Doc()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void Admin_Management_Doc_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData() { 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            var data = (from d in context.tb_dokumens
                        join u in context.tb_users
                        on d.id_pemilik equals u.id_user
                        join p in context.tb_users
                        on d.id_penerima equals p.id_user
                        select new
                        {
                            id=d.id_dokumen,
                            nameDoc=d.nameDokumen,
                            pengirimDoc=d.pengirimDokumen,
                            pemilik=u.name,
                            penerima=p.name,
                            uraianDoc=d.uraianDokumen,
                            tgl_diterima=d.tgl_diterima,
                            tgl_doc=d.tgl_dokumen,
                            agendaAwal=d.tgl_agendaAwal,
                            agendaAkhir=d.tgl_agendaAkhir,
                        });
            int i = 0;
            foreach (var item in data)
            {
                i++;
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = i;
                dataGridView1.Rows[num].Cells[1].Value = item.id;
                dataGridView1.Rows[num].Cells[2].Value = item.nameDoc;
                dataGridView1.Rows[num].Cells[3].Value = item.pengirimDoc;
                dataGridView1.Rows[num].Cells[4].Value = item.pemilik;
                dataGridView1.Rows[num].Cells[5].Value = item.penerima;
                dataGridView1.Rows[num].Cells[6].Value = item.uraianDoc;
                dataGridView1.Rows[num].Cells[7].Value = item.tgl_diterima;
                dataGridView1.Rows[num].Cells[8].Value = item.tgl_doc;
                dataGridView1.Rows[num].Cells[9].Value = item.agendaAwal;
                dataGridView1.Rows[num].Cells[10].Value = item.agendaAkhir;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var FaddDokumen = new Admin_Add_Doc(this.MdiParent);
            FaddDokumen.StartPosition = FormStartPosition.CenterScreen;
            FaddDokumen.FormClosing += (object adsd, FormClosingEventArgs sad) =>
            {
                if (DialogResult.OK == FaddDokumen.DialogResult)
                {
                    loadData();
                }
            };
            FaddDokumen.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = string.Empty;
            if (e.ColumnIndex==12)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var FaddDoc = new Admin_Add_Doc(this.MdiParent);
                FaddDoc.StartPosition = FormStartPosition.CenterScreen;
                MessageBox.Show(id);
                FaddDoc.getId = int.Parse(id);
                FaddDoc.FormClosing += (object sa, FormClosingEventArgs ads) =>
                {
                    if (DialogResult.OK == FaddDoc.DialogResult)
                    {
                        loadData();
                    }
                };
                FaddDoc.Show();
            }
        }
    }
}

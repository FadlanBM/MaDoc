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
            this.Enabled = false;
            FaddDokumen.FormClosing += (object adsd, FormClosingEventArgs sad) =>
            {
                if (DialogResult.OK == FaddDokumen.DialogResult)
                {
                    loadData();
                    this.Enabled=true;  
                }
                if (DialogResult.Cancel == FaddDokumen.DialogResult)
                    this.Enabled = true;
            };
            FaddDokumen.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = string.Empty;
            if (e.ColumnIndex==11)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var FDetailDoc = new Admin_DetailDoc(this.MdiParent);
                FDetailDoc.StartPosition= FormStartPosition.CenterScreen;
                FDetailDoc.getId=int.Parse(id);
                FDetailDoc.Show();
            }
            if (e.ColumnIndex==12)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var FaddDoc = new Admin_Add_Doc(this.MdiParent);
                FaddDoc.StartPosition = FormStartPosition.CenterScreen;
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
            if (e.ColumnIndex==13)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var data =context.tb_dokumens.Where(d=>d.id_dokumen==int.Parse(id)).FirstOrDefault();
                var penerima=context.tb_histories.Where(d=>d.id_dokumen==data.id_dokumen).ToList();
                if (data==null||penerima==null)
                {
                    MessageBox.Show("data tidak di temukan");
                    return;
                }
                DialogResult dialog = MessageBox.Show(null, "Apakah Anda yakin ingin menghapus data ini?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.Yes==dialog)
                {
                    var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\image\";
                    var nameImage = path + data.imagePath;
                    if (File.Exists(nameImage))
                    {
                        File.Delete(nameImage);
                    }
                    context.tb_dokumens.DeleteOnSubmit(data);
                    context.tb_histories.DeleteAllOnSubmit(penerima);                    
                    context.SubmitChanges();
                    MessageBox.Show(null, "Berhasil delete data dokumen", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData(); 
                    return;
             }
            }
        }
    }
}

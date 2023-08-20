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
    public partial class Admin_Add_Doc : Form
    {
        AppDbContextDataContext context;
        public Admin_Add_Doc()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_nameDoc.Text==""||tb_pemilik.Text==""||tb_pengirim.Text==""||tb_penerima.Text==""||tb_perihalDoc.Text==""||tb_agendaDoc.Text==""||tbUraianDoc.Text==""||dt_agendastart.Text==""||dt_agendafinish.Text==""||dt_tgldocumen.Text==""||dt_tglPenerima.Text=="")
            {
                MessageBox.Show(null, "Form belum di isi semua", "WWarning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tb_dokumen dokumen=new tb_dokumen();
            var pemilik = context.tb_users.Where(p => p.name == tb_pemilik.Text&&p.verify==1).FirstOrDefault();
            if (pemilik==null)
            {
                MessageBox.Show(null, "Accunt Pemilik tidak terdaftar, Daftarkan terlebuh Dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            var penerima=context.tb_users.Where(u=>u.name==tb_penerima.Text&&u.verify==1).FirstOrDefault();
            if (penerima==null)
            {
                MessageBox.Show(null, "Accunt Penerima tidak terdaftar Daftarkan terlebuh Dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tb_penerima penerimainput=new tb_penerima();
            dokumen.nameDokumen= tb_nameDoc.Text;
            dokumen.pengirimDokumen=tb_pengirim.Text;
            dokumen.id_penerima = penerima.id_user;
            dokumen.id_pemilik = penerima.id_user;
            dokumen.penerimaDokumen = penerima.name;
            dokumen.pemilikDokumen= penerima.name;
            dokumen.uraianDokumen=tbUraianDoc.Text;
            dokumen.tgl_diterima = dt_tglPenerima.Value;
            dokumen.tgl_dokumen=dt_tgldocumen.Value;
            dokumen.tgl_agendaAwal=dt_agendastart.Value;
            dokumen.tgl_agendaAkhir=dt_agendafinish.Value;
            dokumen.tgl_createdAt=DateTime.Now;
            penerimainput.id_user=penerima.id_user;
            penerimainput.id_dokumen = dokumen.id_dokumen;
            penerimainput.namaPenerima = penerima.name;
            context.tb_dokumens.InsertOnSubmit(dokumen);
            context.tb_penerimas.InsertOnSubmit(penerimainput);
            context.SubmitChanges();
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}

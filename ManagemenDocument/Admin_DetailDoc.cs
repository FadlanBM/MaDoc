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
    public partial class Admin_DetailDoc : Form
    { 
        AppDbContextDataContext context;
        public int? getId { get; set; }
        public Admin_DetailDoc(Form mdi)
        {
            this.MdiParent = mdi;
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Admin_DetailDoc_Load(object sender, EventArgs e)
        {
            if (getId!=null)
            {
                loadData();
            }
        }

        private void loadData()
        {
            var data = context.tb_dokumens.Where(d => d.id_dokumen == getId).FirstOrDefault();
            var penerima = context.tb_users.Where(a => a.id_user == data.id_penerima).FirstOrDefault();
            var pemilik = context.tb_users.Where(k => k.id_user == data.id_pemilik).FirstOrDefault();

            lb_namaDoc.Text = data.nameDokumen;
            lb_agendaDoc.Text = data.agendaDokumen;
            lb_perihalDoc.Text = data.perihalDokumen;
            lb_pemilikDoc.Text = pemilik.name;
            lb_penerima.Text = penerima.name;
            lb_pengirim.Text = data.pengirimDokumen;
            lb_uraianDoc.Text = data.uraianDokumen;
            lb_tglDiterima.Text = data.tgl_diterima.ToString();
            lb_tglDoc.Text = data.tgl_dokumen.ToString();
            lb_agendaStart.Text = data.tgl_agendaAwal.ToString();
            lb_agendaFinis.Text = data.tgl_agendaAkhir.ToString();
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\image\";
            var nameimage = path + data.imagePath;
            if (File.Exists(nameimage))
            {
                using (var stram = File.OpenRead(nameimage))
                {
                    pictureBox1.Image = new Bitmap(stram);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

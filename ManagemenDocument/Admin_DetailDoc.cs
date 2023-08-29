using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        PrintDocument printDocument;
        SaveFileDialog saveFileDialog;
        Image cusImage;
        public Admin_DetailDoc(Form mdi)
        {
            printDocument=new PrintDocument();
            saveFileDialog = new SaveFileDialog();
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
            var history=context.tb_histories.Where(h=>h.id_user==data.id_penerima).FirstOrDefault();
            var penerima = context.tb_users.Where(a => a.id_user ==history.id_user ).FirstOrDefault();
            var pemilik = context.tb_users.Where(k => k.id_user == data.id_pemilik).FirstOrDefault();

            lb_namaDoc.Text = data.nameDokumen;
            lb_agendaDoc.Text = data.agendaDokumen;
            lb_perihalDoc.Text = data.perihalDokumen;
            lb_pemilikDoc.Text = pemilik.name;
            lb_penerima.Text = penerima.name;
            lb_pengirim.Text = data.pengirimDokumen;
            tb_penerimaAwal.Text = data.penerima_pertama;
            lb_uraianDoc.Text = data.uraianDokumen;
            lb_tglDiterima.Text = data.tgl_diterima.ToString();
            lb_tglDoc.Text = data.tgl_dokumen.ToString();
            lb_agendaStart.Text = data.tgl_agendaAwal.ToString();
            lb_agendaFinis.Text = data.tgl_agendaAkhir.ToString();
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\image\";
            var nameimage = path + data.imagePath;
            var nameQrImage = path + data.imageQrCode;
            if (File.Exists(nameimage))
            {               
                if (File.Exists(nameQrImage))
                {
                    using (var stram = File.OpenRead(nameimage))
                    {
                        pictureBox1.Image = new Bitmap(stram);
                    }
                    using (var strams = File.OpenRead(nameQrImage))
                    {
                        pictureBox2.Image = new Bitmap(strams);
                        cusImage=new Bitmap(strams);
                    }
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FilterIndex = 1;
            if (DialogResult.OK==saveFileDialog.ShowDialog())
            {
                cusImage.Save(Path.GetDirectoryName(saveFileDialog.FileName + "\\" + Path.GetFileName(saveFileDialog.FileName) + ".jpg"));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            printDocument.PrintPage += printDokumen_printPage;
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (DialogResult.OK==printDialog.ShowDialog())
            {
                printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                printDocument.Print();
            }
        }

        private void printDokumen_printPage(object seder,PrintPageEventArgs e) {
            e.Graphics.DrawImage(cusImage, new Point(0, 0));
        }

        private void imageToPrint(object sass,PrintPageEventArgs e) {
            e.Graphics.DrawImage(cusImage, 0, 0, e.PageBounds.Width, e.PageBounds.Height);
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           Admin_ListPenerima FlistPenerima=new Admin_ListPenerima(this.MdiParent);
            FlistPenerima.StartPosition = FormStartPosition.CenterScreen;
            FlistPenerima.MdiParent = this.MdiParent;
            FlistPenerima.getId = getId;
            this.Enabled = false;
            FlistPenerima.FormClosing += (object asa, FormClosingEventArgs aaa) =>
            {
                if (DialogResult.OK==FlistPenerima.DialogResult)
                {
                    this.Enabled = true;                    
                }
            };
            FlistPenerima.Show();
        }
    }
}

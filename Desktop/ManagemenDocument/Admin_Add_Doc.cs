using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenDocument
{
    public partial class Admin_Add_Doc : Form
    {
        AppDbContextDataContext context;
        OpenFileDialog openFileDialog;
        private string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\image\";       
        public int? getId { get; set; }

        public Admin_Add_Doc(Form mdi)
        {
            openFileDialog = new OpenFileDialog();
            this.MdiParent = mdi;
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (tb_nameDoc.Text == "" || text3.Text == "" || tb_pengirim.Text == "" || tb_penerima.Text == "" || tb_perihalDoc.Text == "" || tb_agendaDoc.Text == "" || tbUraianDoc.Text == "" || dt_agendastart.Text == "" || dt_agendafinish.Text == "" || dt_tgldocumen.Text == "" || dt_tglPenerima.Text == ""||pictureBox1.Image==null)
            {
                MessageBox.Show(null, "Form belum di isi semua", "WWarning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pemilik = context.tb_users.Where(p => p.name == tb_pemilik.Text).FirstOrDefault();
            if (pemilik == null)
            {
                MessageBox.Show(null, "Accunt Pemilik tidak terdaftar, Daftarkan terlebuh Dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var penerima = context.tb_users.Where(u => u.name == tb_penerima.Text).FirstOrDefault();
            if (penerima == null)
            {
                MessageBox.Show(null, "Accunt Penerima tidak terdaftar Daftarkan terlebuh Dahulu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (getId == null)
            {
                tb_dokumen dokumen = new tb_dokumen();
                tb_history penerimainput = new tb_history();
                dokumen.nameDokumen = tb_nameDoc.Text;
                dokumen.agendaDokumen = tb_agendaDoc.Text;
                dokumen.perihalDokumen = tb_perihalDoc.Text;
                dokumen.pengirimDokumen = tb_pengirim.Text;
                dokumen.id_penerima = penerima.id_user ;  
                dokumen.penerima_pertama=penerima.name;
                dokumen.id_pemilik = pemilik.id_user;
                dokumen.uraianDokumen = tbUraianDoc.Text;
                dokumen.tgl_diterima = dt_tglPenerima.Value;
                dokumen.tgl_dokumen = dt_tgldocumen.Value;
                var nameIamge = DateTime.Now.Ticks.ToString() + Path.GetFileName(openFileDialog.FileName);
                File.Copy(openFileDialog.FileName, path + nameIamge);
                dokumen.imagePath = nameIamge;
                dokumen.tgl_agendaAwal = dt_agendastart.Value;
                dokumen.tgl_agendaAkhir = dt_agendafinish.Value;
                dokumen.tgl_createdAt = DateTime.Now;
                dokumen.token_dokumen = createToken(random());
                context.tb_dokumens.InsertOnSubmit(dokumen);
                context.SubmitChanges();
                penerimainput.id_user = penerima.id_user;
                penerimainput.id_dokumen = dokumen.id_dokumen;
                penerimainput.createdAt = DateTime.Now;
                context.tb_histories.InsertOnSubmit(penerimainput);
                context.SubmitChanges();
                var fgenerateQr = new FGenerateCrCode(this.MdiParent);
                fgenerateQr.StartPosition = FormStartPosition.CenterParent;
                fgenerateQr.getId = dokumen.id_dokumen;
                this.Enabled = false;
                fgenerateQr.FormClosing += (object asasd, FormClosingEventArgs aaa) =>
                {
                    if (DialogResult.OK == fgenerateQr.DialogResult)
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                };
                fgenerateQr.Show();
                clearTb();                
                MessageBox.Show(null, "Berhasil insert data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                var dokumen = context.tb_dokumens.Where(p => p.id_dokumen == getId).FirstOrDefault();
                var penerimainput = context.tb_histories.Where(p => p.id_dokumen == dokumen.id_dokumen).FirstOrDefault();
                var imageOld = path + dokumen.imagePath;
                
                if (imageOld!=openFileDialog.FileName)
                {
                    if (File.Exists(imageOld))
                    {
                        File.Delete(imageOld);
                    }
                    var nameIamge = DateTime.Now.Ticks.ToString() + Path.GetFileName(openFileDialog.FileName);
                    File.Copy(openFileDialog.FileName, path + nameIamge);
                    dokumen.imagePath = nameIamge;
                }
                dokumen.nameDokumen = tb_nameDoc.Text;
                dokumen.agendaDokumen = tb_agendaDoc.Text;
                dokumen.perihalDokumen = tb_perihalDoc.Text;
                dokumen.pengirimDokumen = tb_pengirim.Text;
                dokumen.id_penerima = penerima.id_user;
                dokumen.id_pemilik = pemilik.id_user;
                dokumen.uraianDokumen = tbUraianDoc.Text;
                dokumen.tgl_diterima = dt_tglPenerima.Value;
                dokumen.tgl_dokumen = dt_tgldocumen.Value;                
                dokumen.tgl_agendaAwal = dt_agendastart.Value;
                dokumen.tgl_agendaAkhir = dt_agendafinish.Value;
                dokumen.tgl_createdAt = DateTime.Now;
                penerimainput.id_user = penerima.id_user;
                penerimainput.id_dokumen = dokumen.id_dokumen;
                penerimainput.createdAt = DateTime.Now;
                context.SubmitChanges();
                clearTb();
                MessageBox.Show(null, "Berhasil Update data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void clearTb() {
            tb_nameDoc.Clear();
            tb_pengirim.Clear();
            tb_penerima.Clear();
            tb_pemilik.Clear();
            tbUraianDoc.Clear();
            dt_tgldocumen.Value = DateTime.Now;
            dt_tglPenerima.Value = DateTime.Now;
            dt_agendastart.Value = DateTime.Now;
            dt_agendafinish.Value = DateTime.Now;
            pictureBox1.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fBrowserCus = new BrowserDataCustomer(this.MdiParent);
            fBrowserCus.StartPosition = FormStartPosition.CenterParent;
            fBrowserCus.FormClosing += (object asd, FormClosingEventArgs assd) =>
            {
                if (DialogResult.OK == fBrowserCus.DialogResult)
                {
                    var id_pemilik = fBrowserCus.getId;
                    var data = context.tb_users.Where(u => u.id_user == int.Parse(id_pemilik)).FirstOrDefault();
                    tb_penerima.Text = data.name;
                }
            };
            fBrowserCus.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fBrowserCus = new BrowserDataCustomer(this.MdiParent);
            fBrowserCus.StartPosition = FormStartPosition.CenterParent;
            fBrowserCus.FormClosing += (object asd, FormClosingEventArgs ass) =>
            {
                if (DialogResult.OK == fBrowserCus.DialogResult)
                {
                    var id_penerima = fBrowserCus.getId;
                    var data = context.tb_users.Where(u => u.id_user == int.Parse(id_penerima)).FirstOrDefault();
                    tb_pemilik.Text = data.name;
                }
            };
            fBrowserCus.Show();
        }

        private void LoadData() {
            if (getId!=null)
            {                
            var data = context.tb_dokumens.Where(d => d.id_dokumen == getId).FirstOrDefault();
            var history=context.tb_histories.Where(h=>h.id_user==data.id_penerima).FirstOrDefault();
            var pemilik = context.tb_users.Where(p => p.id_user == data.id_pemilik).FirstOrDefault();
            var penerima = context.tb_users.Where(u => u.id_user == history.id_user ).FirstOrDefault();
            tb_nameDoc.Text=data.nameDokumen;
            tb_perihalDoc.Text = data.perihalDokumen;
            tb_agendaDoc.Text = data.agendaDokumen;
            tb_pengirim.Text=data.pengirimDokumen;
            tb_penerima.Text=penerima.name;
            tb_pemilik.Text=pemilik.name;
            tbUraianDoc.Text = data.uraianDokumen;
            dt_tgldocumen.Value = data.tgl_dokumen;
            dt_tglPenerima.Value = data.tgl_diterima;
            dt_agendastart.Value = data.tgl_agendaAwal;
            dt_agendafinish.Value =data.tgl_agendaAkhir;
                var nameImage = path + data.imagePath;
                if (File.Exists(nameImage))
                {
                    using (var stream=File.OpenRead(nameImage))
                    {
                        pictureBox1.Image =new Bitmap(stream);
                    }
                    openFileDialog.FileName = nameImage;   
                }
            }
        } 

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "(File|*jpg;*png;)";
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image=Image.FromFile(openFileDialog.FileName);
            }
        }

        private void Admin_Add_Doc_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dt_tgldocumen_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dt_agendafinish_ValueChanged(object sender, EventArgs e)
        {
            if (dt_agendastart.Value.Date>dt_agendafinish.Value.Date)
            {
                MessageBox.Show(null, "Agenda akhir tidak boleh kurang dari agenda awal", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dt_agendafinish.Value=dt_agendastart.Value.Date;
                return;
            }
        }

        private void dt_agendastart_ValueChanged(object sender, EventArgs e)
        {

        }

        private string random() { 
            Random rd= new Random();
            var num = rd.Next(8, 12);
            string token = string.Empty;
            int i = 0;
            do {
                var chr = rd.Next(48, 123);
                if ((chr>48&&chr<57)||(chr>60&&chr<90)||(chr>92&&chr<122))
                {
                    token = token + (char)chr;
                    i++;
                    if (i == num)
                        break;                    
                }

            } while (true);
            return token;
        }


        private string createToken(string p) { 
            StringBuilder sb = new StringBuilder();
            using (var sha=SHA256.Create())
            {
                var baytes = sha.ComputeHash(Encoding.UTF8.GetBytes(p));
                for (int i = 0; i < baytes.Length; i++)
                {
                    sb.Append(baytes[i].ToString("x2"));
                }
            return sb.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var fgenerateQr = new FGenerateCrCode(this.MdiParent);
            fgenerateQr.StartPosition = FormStartPosition.CenterParent;
            fgenerateQr.getId = getId;
            this.Hide();
            fgenerateQr.FormClosing += (object asasd, FormClosingEventArgs aaa) =>
            {
                if (DialogResult.OK == fgenerateQr.DialogResult)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            };
            fgenerateQr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }
    }
}

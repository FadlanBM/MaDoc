using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenDocument
{
    public partial class FGenerateCrCode : Form
    {
        AppDbContextDataContext context;
        private string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\image\";
        PrintDocument printDocument;
        SaveFileDialog saveFileDialog;
        public int? getId { get; set; }
        Image cusimage;
        public FGenerateCrCode(Form mdi)
        {
            printDocument=new PrintDocument();
            saveFileDialog=new SaveFileDialog();
            context = new AppDbContextDataContext();
            this.MdiParent = mdi;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            DialogResult result = DialogResult.OK;  
            this.Close();
        }

        private void FGenerateCrCode_Load(object sender, EventArgs e)
        {
            var token=context.tb_dokumens.Where(d=>d.id_dokumen==getId).FirstOrDefault();
            if (token!=null)
            {   
                Zen.Barcode.CodeQrBarcodeDraw qrdraw = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                cusimage = qrdraw.Draw(token.token_dokumen, 200);
                pictureBox1.Image = cusimage;
            }

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var nameImage = DateTime.Now.Ticks.ToString() + ".jpg";
            var saveImage = context.tb_dokumens.Where(d => d.id_dokumen == getId).FirstOrDefault();
            cusimage.Save(path + "\\" + nameImage);
            DialogResult = DialogResult.OK;
            saveImage.imageQrCode = nameImage;
            context.SubmitChanges();
        }
        private string createToken(string p)
        {
            StringBuilder sb = new StringBuilder();
            using (var sha = SHA256.Create())
            {
                var baytes = sha.ComputeHash(Encoding.UTF8.GetBytes(p));
                for (int i = 0; i < baytes.Length; i++)
                {
                    sb.Append(baytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog.RestoreDirectory= true;
            saveFileDialog.FilterIndex = 1;
            if (DialogResult.OK==saveFileDialog.ShowDialog())
            {
                cusimage.Save(Path.GetDirectoryName(saveFileDialog.FileName) + "\\" + Path.GetFileName(saveFileDialog.FileName)+".jpg");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*printDocument.PrintPage += new PrintPageEventHandler(imagetoPrint);*/
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog print=new PrintDialog();
            print.Document = printDocument;
            if (DialogResult.OK==print.ShowDialog())
            {
                printDocument.PrinterSettings.PrinterName= "Microsoft Print to PDF";
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(cusimage,new Point(0,0));
        }

        private void imagetoPrint(object sass,PrintPageEventArgs e) {
            e.Graphics.DrawImage(cusimage, 0, 0, e.PageBounds.Width, e.PageBounds.Height);
                
        }
    }
}

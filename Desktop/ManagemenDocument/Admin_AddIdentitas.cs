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
    public partial class Admin_AddIdentitas : Form
    {
        public int id { get; set; }
        AppDbContextDataContext context;
        public Admin_AddIdentitas(Form mdi)
        {
            this.MdiParent= mdi;    
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_identitas.Text=="")
            {
                MessageBox.Show(null, "Form type identitas belum di isi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (id==0)
            {
            tb_identita tb_Identita=new tb_identita();
            tb_Identita.nameIdentitas = tb_identitas.Text.ToUpper();
            context.tb_identitas.InsertOnSubmit(tb_Identita);
            context.SubmitChanges();
            DialogResult=DialogResult.OK;
            MessageBox.Show(null, "Berhasil Input data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearTb();
            return;
            }
            else
            {
                var data=context.tb_identitas.Where(idn=>idn.id_identitas==id).FirstOrDefault();
                data.nameIdentitas=tb_identitas.Text.ToUpper();
                context.SubmitChanges();
                DialogResult=DialogResult.OK;
                MessageBox.Show(null, "Berhasil update data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearTb();
                return;
            }

        }
        private void tbload() { 
            var data=context.tb_identitas.Where(idt=>idt.id_identitas==id).FirstOrDefault();    
            tb_identitas.Text=data.nameIdentitas.ToString();
        }
        private void clearTb() {
            tb_identitas.Text = "";
        }

        private void Admin_AddIdentitas_Load(object sender, EventArgs e)
        {
            if (id!=0)
            {
            tbload();                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
            this.Close();
        }
    }
}

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
    public partial class BrowserDataCustomer : Form
    {
        public string getId { get; private set; }
        AppDbContextDataContext context;
        public BrowserDataCustomer(Form form)
        {
            this.MdiParent = form;
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void loadData() { 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            var data = (from c in context.tb_users
                        join idn in context.tb_identitas
                        on c.id_identitas equals idn.id_identitas
                        where c.verify == 1
                        select new
                        {
                            id=c.id_user,
                            name=c.name,
                            no_identitas=c.no_identitas,
                            na_identitas=idn.nameIdentitas,
                            alamat=c.alamat,
                            phone=c.phoneNumber,
                        }).ToList();
            int i = 0;
            foreach (var item in data)
            {
                i++;
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value =i;
                dataGridView1.Rows[num].Cells[1].Value =item.id;
                dataGridView1.Rows[num].Cells[2].Value =item.name;
                dataGridView1.Rows[num].Cells[3].Value =item.no_identitas;
                dataGridView1.Rows[num].Cells[4].Value =item.na_identitas;
                dataGridView1.Rows[num].Cells[5].Value =item.alamat;
                dataGridView1.Rows[num].Cells[6].Value =item.phone;
            }
        }

        private void BrowserDataCustomer_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==7)
            {
                var nama = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                DialogResult dialogResult = MessageBox.Show(null, "Apakah Anda ingin memilih data " + nama, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes) { 
                 getId = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                DialogResult = DialogResult.OK;
                this.Close();
                }
               
            }
        }
    }
}

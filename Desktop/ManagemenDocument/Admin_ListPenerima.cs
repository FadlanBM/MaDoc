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
    public partial class Admin_ListPenerima : Form
    {
        public int? getId { get; set; }
        AppDbContextDataContext context;
        public Admin_ListPenerima(Form mdi)
        {
            this.MdiParent= mdi;
            context = new AppDbContextDataContext();
            InitializeComponent();
        }
        private void loadData() {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            var data = (from h in context.tb_histories
                        join u in context.tb_users
                        on h.id_user equals u.id_user
                        where h.id_dokumen==getId
                        orderby h.createdAt descending
                        select new
                        {
                            name=u.name,
                            tanggal_diterima=h.createdAt
                        }).ToList();
            int i = 0;
            if (tb_filter.Text.Length!=0)
            {
                data = data.Where(d => d.name.Contains(tb_filter.Text)).ToList();
            }
            foreach (var item in data)
            {
                i++;
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value =i;
                dataGridView1.Rows[num].Cells[1].Value = item.tanggal_diterima;
                dataGridView1.Rows[num].Cells[2].Value = item.name;

            }
        }

        private void Admin_ListPenerima_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void tb_filter_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}

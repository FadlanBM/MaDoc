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
    public partial class FGenerateCrCode : Form
    {
        AppDbContextDataContext context;
        public int getId { get; set; }
        public FGenerateCrCode(Form mdi)
        {
            context = new AppDbContextDataContext();
            this.MdiParent = mdi;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FGenerateCrCode_Load(object sender, EventArgs e)
        {
        }
    }
}

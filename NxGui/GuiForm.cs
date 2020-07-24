using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class GuiForm : Form
    {
        public GuiForm()
        {
            InitializeComponent();
        }

        private void Equip_MouseClick(object sender, MouseEventArgs e)
        {
            Program.sw.WriteLine(this.TopId.Text);
            Program.ev.Set();
            MessageBox.Show(this.TopId.Text);
        }
    }
}

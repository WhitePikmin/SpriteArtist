using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype
{
    public partial class FRM_Intro : Form
    {
        public FRM_Intro()
        {
            InitializeComponent();
        }

        private void BTN_New_Click(object sender, EventArgs e)
        {
            FRM_New nouveau = new FRM_New();
            nouveau.ShowDialog();
            if (nouveau.DialogResult == DialogResult.OK)
            {
                this.Visible = false;
                FRM_Main main = new FRM_Main(nouveau.CanvasWidth, nouveau.CanvasHeight);
                main.ShowDialog();
                this.Close();
            }
        }

        private void BTN_Quit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

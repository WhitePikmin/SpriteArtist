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
    public partial class FRM_New : Form
    {
        public ushort CanvasWidth;
        public ushort CanvasHeight;

        public FRM_New()
        {
            InitializeComponent();
            CanvasHeight = (ushort)NUM_Height.Value;
            CanvasWidth = (ushort)NUM_Width.Value;
        }

        private void BTN_Create_Click(object sender, EventArgs e)
        {
            DialogResult =  DialogResult.OK;
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void NUM_Height_ValueChanged(object sender, EventArgs e)
        {
            CanvasHeight = (ushort)NUM_Height.Value;
        }

        private void NUM_Width_ValueChanged(object sender, EventArgs e)
        {
            CanvasWidth = (ushort)NUM_Width.Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteArtist
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

        private void BTN_Open_Click(object sender, EventArgs e)
        {
            if (DLG_Open.ShowDialog() == DialogResult.OK)
            {
                string Path = DLG_Open.FileName;
                Bitmap ImageOpened = new Bitmap(Path);
                FRM_Main main = new FRM_Main(ImageOpened);
                main.ShowDialog();
                this.Close();
            }
          
        }

        private void FRM_Intro_Load(object sender, EventArgs e)
        {

        }
    }
}

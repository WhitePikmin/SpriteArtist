using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SpriteArtist
{

    public partial class FRM_Main
    {
        private void BTN_Add_Color_Click(object sender, EventArgs e)
        {

            if (cptColor + NotColorButtons < MaxColorPalette + NotColorButtons)
            {
                TLS_Colors.Items[cptColor + NotColorButtons].Visible = true;
                TLS_Colors.Items[cptColor + NotColorButtons].BackColor = MainPen.Color;
                ++cptColor;
            }
        }

        private void BTN_Remove_Color_Click(object sender, EventArgs e)
        {
            if (cptColor + NotColorButtons > NotColorButtons)
            {
                TLS_Colors.Items[cptColor + NotColorButtons].Visible = false;
                --cptColor;
            }
        }

        private void BTN_Palette_MouseDown(object sender, MouseEventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            if (e.Button == MouseButtons.Left)
            {
                ChangeColorMainPen(button.BackColor);
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (DLG_Color.ShowDialog() == DialogResult.OK)
                {
                    ChangeColorMainPen(DLG_Color.Color);
                    TLS_Colors.Items[button.Name].BackColor = DLG_Color.Color;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                ChangeColorSecondPen(button.BackColor);
            }
        }

    }
}

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
        public void PickColor(MouseEventArgs e,bool ChangeMainPen)
        {
            bool Pick = false;
            OldPoint = AdaptPointToSelection(GetCursorLocationRelative(e));
            Bitmap img = new Bitmap(1, 1);
            if (ActiveSelection)
            {
                if (OldPoint.X < Selection.Width && OldPoint.X >= 0)
                {
                    if (OldPoint.Y < Selection.Height && OldPoint.Y >= 0)
                    {
                        img = Selection;
                        Pick = true;
                    }
                }
            }
            else
            {
                img = Sprite;
                Pick = true;
            }
            if (Pick)
            {
                Color NewCol = img.GetPixel(OldPoint.X, OldPoint.Y);
                NewCol = Color.FromArgb(255, NewCol.R, NewCol.G, NewCol.B);

                if (ChangeMainPen)
                    ChangeColorMainPen(NewCol);
                else
                    ChangeColorSecondPen(NewCol);
            }
        }
    }
}

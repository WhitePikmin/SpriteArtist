using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Prototype
{

    public partial class FRM_Main
    {
        private void DrawOnCanvas(Pen pen_, MouseEventArgs e, bool Erase)
        {
            Canvas = Graphics.FromImage(Sprite);
            CurrentPoint = GetCursorLocationRelative(e);

            if (Erase)
            {
                //Pour que l'efface puisse marcher: être capable de rendre un pixel transparent
                Canvas.CompositingMode = CompositingMode.SourceCopy;
                pen_.Color = Color.Transparent;
            }
            
            Canvas.DrawLine(pen_, OldPoint, CurrentPoint);
            OldPoint = CurrentPoint;
            PNL_Canvas.Invalidate();
        }


        private void DrawSingleDotOnCanvas(Color col_, MouseEventArgs e)
        {
            Canvas = Graphics.FromImage(Sprite);
            OldPoint = GetCursorLocationRelative(e);
            if (col_ == Color.Transparent) Canvas.CompositingMode = CompositingMode.SourceCopy;
            int PenWidthHalf = (int)Math.Ceiling(MainPen.Width / 2);
            if (MainPen.Width < 3)
                Canvas.FillRectangle(new SolidBrush(col_), OldPoint.X + 1 - PenWidthHalf, OldPoint.Y + 1 - PenWidthHalf, MainPen.Width, MainPen.Width);
            else
                Canvas.FillEllipse(new SolidBrush(col_), OldPoint.X - PenWidthHalf, OldPoint.Y - PenWidthHalf, MainPen.Width, MainPen.Width);
            PNL_Canvas.Invalidate();
        }

        private void ChangeColorMainPen(Color col)
        {
            MainPen.Color = col;
            BTN_MainColor.BackColor = col;
        }

        private void ChangeColorSecondPen(Color col)
        {
            SecondPen.Color = col;
            BTN_SecondColor.BackColor = col;
        }

        private void ChangePenSize(float size_)
        {
            MainPen.Width = size_;
            SecondPen.Width = size_;
        }

    }
}

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
        private void DrawOnCanvas(Pen pen_, MouseEventArgs e, bool Erase)
        {
            if (ActiveSelection)
                Canvas = Graphics.FromImage(Selection);
            else
                Canvas = Graphics.FromImage(Sprite);

            CurrentPoint = AdaptPointToSelection(GetCursorLocationRelative(e));
            Color col_;
            if (Erase)
            {
                //Pour que l'efface puisse marcher: être capable de rendre un pixel transparent
                Canvas.CompositingMode = CompositingMode.SourceCopy;
                col_ = Color.Transparent;
            }
            else
            {
                col_ = pen_.Color;
                Canvas.CompositingMode = CompositingMode.SourceOver;
            }

            Pen usedPen = new Pen(col_, pen_.Width);
            usedPen.SetLineCap(LineCap, LineCap, DashCap);

            Canvas.DrawLine(usedPen, OldPoint, CurrentPoint);
            OldPoint = CurrentPoint;
            PNL_Canvas.Invalidate();
            FileChanged = true;
        }


        private void DrawSingleDotOnCanvas(Color col_, MouseEventArgs e)
        {
            if(ActiveSelection)
                Canvas = Graphics.FromImage(Selection);
            else
                Canvas = Graphics.FromImage(Sprite);

            OldPoint = AdaptPointToSelection(GetCursorLocationRelative(e));

            if (col_ == Color.Transparent)
                Canvas.CompositingMode = CompositingMode.SourceCopy;
            else
                Canvas.CompositingMode = CompositingMode.SourceOver;

            int PenWidthHalf = (int)Math.Ceiling(MainPen.Width / 2);
            if (MainPen.Width < 3)
                Canvas.FillRectangle(new SolidBrush(col_), OldPoint.X + 1 - PenWidthHalf, OldPoint.Y + 1 - PenWidthHalf, MainPen.Width, MainPen.Width);
            else
                Canvas.FillEllipse(new SolidBrush(col_), OldPoint.X - PenWidthHalf, OldPoint.Y - PenWidthHalf, MainPen.Width, MainPen.Width);

            PNL_Canvas.Invalidate();
            FileChanged = true;
        }

        public Point AdaptPointToSelection(Point p_)
        {
            if (ActiveSelection)
            {
                return new Point(p_.X - SelectionZone.X,p_.Y - SelectionZone.Y);
            }
            return p_;
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

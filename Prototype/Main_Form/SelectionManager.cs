using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;

namespace SpriteArtist
{
    public partial class FRM_Main
    {
        private void DragSelectedZone(MouseEventArgs e)
        {
                if (!DraggingSelection)
                {
                    DraggingSelection = true;
                    Point StartingPoint = GetCursorLocationRelative(e);
                    SelectionZone.X = StartingPoint.X;
                    SelectionZone.Y = StartingPoint.Y;
                }
            Point CursorPoint = GetCursorLocationRelative(e);
            SelectionZone.Width = CursorPoint.X - SelectionZone.X;
            SelectionZone.Height = CursorPoint.Y - SelectionZone.Y;

            SelectionZone = FormatRectangleInbound(SelectionZone);
        }

        private void DrawSelectionZone(Graphics g_)
        {
            ActiveSelection = true;
           // SelectionZone = new Rectangle(2,2,9,9);
            if (ActiveSelection || DraggingSelection)
            {
                Pen RectanglePen = new Pen(Color.Black,2);
                Rectangle DrawnZone = FormatRectanglePositiveCoord(SelectionZone);

                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        RectanglePen.DashStyle = DashStyle.Dot;
                        RectanglePen.Color = Color.White;
                    }
                    g_.DrawRectangle(RectanglePen,
                        DrawnZone.X * Zoom,
                        DrawnZone.Y * Zoom,
                        DrawnZone.Width * Zoom,
                        DrawnZone.Height * Zoom);
                }
                PNL_Canvas.Invalidate();
            }
        }

        private Rectangle FormatRectanglePositiveCoord(Rectangle rect)
        {
            if (rect.Width < 0)
            {
                rect.X += rect.Width;
                rect.Width *= -1;
            }

            if (rect.Height < 0)
            {
                rect.Y += rect.Height;
                rect.Height *= -1;
            }
            return rect;
        }

        private Rectangle FormatRectangleInbound(Rectangle rect)
        {
            while (rect.X + rect.Width < 0) rect.Width++;
            while (rect.X + rect.Width > Sprite.Width) rect.Width--;

            while (rect.Y + rect.Height < 0) rect.Height++;
            while (rect.Y + rect.Height > Sprite.Height) rect.Height--;

            return rect;
        }
    }
}

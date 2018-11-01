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
        bool DraggingLine = false;
        Bitmap LineOverlay = new Bitmap(1, 1);
        Color LineColor = Color.Black;
        Point LineStart = new Point(0, 0);
        Point LineEnd = new Point(0, 0);

        void StartLine(Color Col, MouseEventArgs e)
        {
            if (!DraggingLine)
            {
                LineStart = AdaptPointToSelection(GetCursorLocationRelative(e));
                LineColor = Col;
                DraggingLine = true;
            }
        }

        void DragLine(ref Pen pen_,MouseEventArgs e)
        {
            if (DraggingLine)
            {
                LineEnd = AdaptPointToSelection(GetCursorLocationRelative(e));

                if (ActiveSelection)
                    ClearLineOverlay(ref Selection);
                else
                    ClearLineOverlay(ref Sprite);

                Canvas = Graphics.FromImage(LineOverlay);

                Pen usedPen = new Pen(LineColor, pen_.Width);
                usedPen.SetLineCap(LineCap, LineCap, DashCap);

                Canvas.DrawLine(usedPen, LineStart, LineEnd);
                PNL_Canvas.Invalidate();
            }
        }

        void ReleaseLine(MouseEventArgs e)
        {
            if (DraggingLine)
            {
                if (ActiveSelection)
                    PasteImagetoSelection(LineOverlay, 0, 0);
                else
                    PasteImagetoSprite(LineOverlay, 0, 0);
                DraggingLine = false;
            }
        }

        private void DrawLineOverlay(Graphics g_)
        {
            if (DraggingLine)
            {
                int X = 0;
                int Y = 0;
                if (ActiveSelection)
                {
                    X = SelectionZone.X;
                    Y = SelectionZone.Y;
                }
                    Rectangle DrawZone = new Rectangle(new Point(X,Y),new Size(LineOverlay.Width, LineOverlay.Height));
                g_.DrawImage(LineOverlay, 
                    new RectangleF(DrawZone.X * Zoom,
                    DrawZone.Y * Zoom,
                    DrawZone.Width * Zoom,
                    DrawZone.Height * Zoom),
                    new RectangleF(-0.5f, -0.5f, LineOverlay.Width, LineOverlay.Height), GraphicsUnit.Pixel);
            }
        }

        void ClearLineOverlay(ref Bitmap img)
        {
            if (LineOverlay.Width != img.Width && LineOverlay.Height != img.Height)
                LineOverlay = new Bitmap(img.Width, img.Height);
            else
                for (int i = 0; i < LineOverlay.Height; i++)//Clear LineOverlay
                {
                    for (int j = 0; j < LineOverlay.Width; j++)
                        LineOverlay.SetPixel(j, i, Color.Transparent);
                }
        }
    }
}

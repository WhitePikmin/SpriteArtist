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
        Graphics LineCanvas;

        void StartLine(Color Col, MouseEventArgs e)
        {
            if (!DraggingLine)
            {
                LineStart = AdaptPointToSelection(GetCursorLocationRelative(e));
                LineColor = Col;
                DraggingLine = true;

                if (ActiveSelection)
                    CreateLineOverlay(ref Selection);
                else
                    CreateLineOverlay(ref Sprite);

                LineCanvas = Graphics.FromImage(LineOverlay);
                LineCanvas.CompositingMode = CompositingMode.SourceCopy;


            }
        }

        void DragLine(ref Pen pen_,MouseEventArgs e)
        {
            if (DraggingLine)
            {
                Pen usedPen = new Pen(LineColor, pen_.Width);
                usedPen.SetLineCap(LineCap, LineCap, DashCap);

                ClearLineOverlay(usedPen);

                LineEnd = AdaptPointToSelection(GetCursorLocationRelative(e));
                usedPen.Color = LineColor;
                LineCanvas.DrawLine(usedPen, LineStart, LineEnd);
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

        void CreateLineOverlay(ref Bitmap img)
        {
                LineOverlay = new Bitmap(img.Width, img.Height);
        }

        void ClearLineOverlay(Pen pen_)
        {
            pen_.Color = Color.Transparent;
            pen_.Width += 1;
            LineCanvas.DrawLine(pen_, LineStart, LineEnd);
        }
    }
}

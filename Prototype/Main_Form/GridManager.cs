using System.Drawing;
using System.Windows.Forms;

namespace Prototype
{
    partial class FRM_Main
    {
        private void SetGridVisible(bool visible)
        {
            DisplayGrid = visible;
            PNL_Canvas.Invalidate();
        }

        private void DrawGrid(Graphics g_)
        {
            if (DisplayGrid && Zoom > 2)
            {
                int Offset = (int)(Zoom / 2);
                Offset = 0;
                Pen GridLine = new Pen(Color.Black,1);

                int LineWidth = (int)(Canvas_Width * Zoom) + 1;
                int LineHeight = (int)(Canvas_Height * Zoom) + 1;

                for (int i = 0; i < Canvas_Width + 1; i++)
                {
                    Point Begin = new Point((int)(i * Zoom) - Offset, 0);
                    Point End = new Point((int)(i * Zoom) - Offset, LineHeight);
                    g_.DrawLine(GridLine,Begin,End);
                }
                for (int i = 0; i < Canvas_Height + 1; i++)
                {
                    Point Begin = new Point(0, (int)(i * Zoom) - Offset);
                    Point End = new Point(LineHeight, (int)(i * Zoom) - Offset);
                    g_.DrawLine(GridLine, Begin, End);
                }
            }
        }
    }
}

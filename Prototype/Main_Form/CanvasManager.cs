using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteArtist
{
    public partial class FRM_Main
    {
        private void CenterCanvas() => PNL_Canvas.Location = new Point(PNL_Drag_Zone.Width / 2 - (PNL_Canvas.Width / 2), PNL_Drag_Zone.Height / 2 - (PNL_Canvas.Height / 2));
        private void CenterCanvas(int X, int Y) => PNL_Canvas.Location = new Point(PNL_Drag_Zone.Width / 2 - X, PNL_Drag_Zone.Height / 2 - Y);

        private void DragCanvas(Point MouseLocation)
        {
            CurrentDragPoint = MouseLocation;
            PNL_Canvas.Location = new Point(PNL_Canvas.Location.X - (OldDragPoint.X - CurrentDragPoint.X), PNL_Canvas.Location.Y - (OldDragPoint.Y - CurrentDragPoint.Y));
            OldDragPoint = CurrentDragPoint;
            Cursor.Current = Cursors.Hand;
        }

        private void ChangeZoom(float newZoom)
        {
            Zoom = newZoom;
            PNL_Canvas.Width = (int)(Canvas_Width * Zoom);
            PNL_Canvas.Height = (int)(Canvas_Height * Zoom);
            PNL_Canvas.Invalidate();
            TXB_ZoomLevel.Text = (Zoom * 100).ToString() + '%';

        }

        private void ZoomIn()
        {
            if (Zoom != ZOOM_MAX)
                ChangeZoom(Zoom * 2);
        }

        private void ZoomIn(MouseEventArgs e)
        {
            if (Zoom != ZOOM_MAX)
            {
                ChangeZoom(Zoom * 2);
                CenterCanvas(e.X * 2, e.Y * 2);
            }
        }

        private void ZoomOut()
        {
            if (Zoom != ZOOM_MIN)
                ChangeZoom(Zoom / 2);
        }

        private void Zoom1x()
        {
            ChangeZoom(1);
            CenterCanvas();
        }

        private void ZoomOut(MouseEventArgs e)
        {
            if (Zoom != ZOOM_MIN)
            {
                ChangeZoom(Zoom / 2);
                CenterCanvas(e.X / 2, e.Y / 2);
            }
        }
    }
}

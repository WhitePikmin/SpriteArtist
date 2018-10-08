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
                    ActiveSelection = true;
                }
            Point CursorPoint = GetCursorLocationRelative(e);
            SelectionZone.Width = CursorPoint.X - SelectionZone.X;
            SelectionZone.Height = CursorPoint.Y - SelectionZone.Y;

            SelectionZone = FormatRectangleInbound(SelectionZone);
            PNL_Canvas.Invalidate();
        }

        private void SelectAll()
        {
            SelectionZone.X = 0;
            SelectionZone.Y = 0;
            SelectionZone.Width = Sprite.Width;
            SelectionZone.Height = Sprite.Height;
            ActiveSelection = true;
            PNL_Canvas.Invalidate();
        }

        private void ReleaseSelection()
        {
            if (DraggingSelection)
            {
                DraggingSelection = false;
                ActiveSelection = true;
                Selection = CopySelection();
            }
        }

        private void MoveSelection()
        {

        }

        private void DrawSelectionZone(Graphics g_)
        {
            if (ActiveSelection)
            {
                if (ActiveSelection || DraggingSelection)
                {
                    Pen RectanglePen = new Pen(Color.Black, 2);
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
                }
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

        private void DisplayContextMenu(MouseEventArgs e)
        {
           PNL_Canvas.ContextMenuStrip = CTM_Selection_Options;
        }

        private void CopySelectionIntoClipboard()
        {
            if (ActiveSelection)
            {
                SelectionZone = FormatRectanglePositiveCoord(SelectionZone);
                SelectionZone = FormatRectangleInbound(SelectionZone);
                
                Bitmap Copy = Sprite.Clone(SelectionZone, Sprite.PixelFormat);
                PlaceInClipboard(Copy);
            }
        }

        private Bitmap CopySelection()
        {
            if (ActiveSelection)
            {
                SelectionZone = FormatRectanglePositiveCoord(SelectionZone);
                SelectionZone = FormatRectangleInbound(SelectionZone);

                return Sprite.Clone(SelectionZone, Sprite.PixelFormat);
            }
            return null;
        }

        private void PlaceInClipboard(Bitmap img)
        {
            //Faire en sorte que la transparence soie pris en compte dans le presse papier en copiant en PNG
            MemoryStream PngStream = new MemoryStream();
            img.Save(PngStream, ImageFormat.Png);
            DataObject data = new DataObject("PNG",PngStream);

            Clipboard.Clear();
            Clipboard.SetDataObject(data,true);
        }

        private void DeleteSelection()
        {
            if (ActiveSelection)
            {
                SelectionZone = FormatRectanglePositiveCoord(SelectionZone);
                SelectionZone = FormatRectangleInbound(SelectionZone);

                int LimitY = SelectionZone.Height + SelectionZone.Y;
                int LimitX = SelectionZone.Width + SelectionZone.X;

                for (int j = SelectionZone.Y; j < LimitY; j++)
                {
                    for (int i = SelectionZone.X; i < LimitX; i++)
                    {
                        Sprite.SetPixel(i, j, Color.Transparent);
                    }
                }
                PNL_Canvas.Invalidate();
                FileChanged = true;
            }
        }

        private void CutSelection()
        {
            CopySelectionIntoClipboard();
            DeleteSelection();
        }

    }
}

﻿using System;
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
        private void HandleClickSelection(MouseEventArgs e)
        {
            OldPoint = GetCursorLocationRelative(e);
            if (ActiveSelection)
                if (!PointInRectangle(OldPoint, SelectionZone))
                {
                    StopSelecting();
                }
        }

        private void HandleSelection(MouseEventArgs e)
        {
            if (ActiveSelection && !DraggingSelection)
            {
                Point CursorPoint = GetCursorLocationRelative(e);
                if (SelectionZone.Contains(CursorPoint) || MovingSelection)
                {
                    MoveSelection(e);
                }
                else
                {
                    StopSelecting();
                    DragSelectedZone(e);
                }
            }
            else
            {
                if (ActiveSelection)
                    StopSelecting();
                DragSelectedZone(e);
            }
        }

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
            PNL_Canvas.Invalidate();
        }

        private void SelectAll()
        {
            if (SelectionZone.X == 0 &&
            SelectionZone.Y == 0 &&
            SelectionZone.Width == Sprite.Width &&
            SelectionZone.Height == Sprite.Height && ActiveSelection)
                StopSelecting();
            else
            {
                StopSelecting();
                SelectionZone.X = 0;
                SelectionZone.Y = 0;
                SelectionZone.Width = Sprite.Width;
                SelectionZone.Height = Sprite.Height;
                StartSelecting();
            }
            PNL_Canvas.Invalidate();
        }

        private void ReleaseSelection()
        {
            if (DraggingSelection)
            {
                DraggingSelection = false;
                if (SelectionZone.Width == 0 || SelectionZone.Height == 0)
                {
                    ActiveSelection = false;
                    ClearTimelineSelection();
                }
                else
                {
                    StartSelecting();
                }
            }
            else
            if (MovingSelection)
            {
                MovingSelection = false;
            }
        }

        private void StartSelecting()
        {
            UpdateTimeline();
            ActiveSelection = true;
            Selection = CopySelection();
            DeleteSelection();
            
        }

        private void StopSelecting()
        {
            if(Selection != null && ActiveSelection)
            PasteImagetoSprite(Selection, SelectionZone.X, SelectionZone.Y);
            ClearTimelineSelection();
        }

        private void PasteImagetoSprite(Bitmap img_,int X, int Y)
        {
            int WidthImg = img_.Width + X;
            int HeightImg = img_.Height + Y;
            ActiveSelection = false;
            

            for (int j = Y; j < HeightImg; j++)
            {
                if(j < Sprite.Height && j >= 0)
                for (int i = X; i < WidthImg; i++)
                {
                        if (i < Sprite.Width && i >= 0)
                        {
                            Sprite.SetPixel(i, j, OverColor(img_.GetPixel(i-X,j-Y), Sprite.GetPixel(i, j)));
                        }
                }
            }
            PNL_Canvas.Invalidate();
        }

        private void PasteImagetoSelection(Bitmap img_, int X, int Y)
        {
            int WidthImg = img_.Width + X;
            int HeightImg = img_.Height + Y;

            for (int j = Y; j < HeightImg; j++)
            {
                if (j < Selection.Height && j >= 0)
                    for (int i = X; i < WidthImg; i++)
                    {
                        if (i < Selection.Width && i >= 0)
                        {
                            Selection.SetPixel(i, j, OverColor(img_.GetPixel(i - X, j - Y), Selection.GetPixel(i, j)));
                        }
                    }
            }
            PNL_Canvas.Invalidate();
        }

        private void PasteClipboardToSelection()
        {
            if (Clipboard.ContainsData("PNG"))
            {
                var Clipboarddata = Clipboard.GetData("PNG");
                if (Clipboarddata is MemoryStream)
                    StreamToSelection(Clipboarddata as MemoryStream);
            }
            else if (Clipboard.ContainsData("CF_BITMAP"))
            {
                var Clipboarddata = Clipboard.GetData("CF_BITMAP");
                if (Clipboarddata is MemoryStream)
                    StreamToSelection(Clipboarddata as MemoryStream);
            }
        }

        private void StreamToSelection(MemoryStream strm)
        {
            StopSelecting();
            ActiveSelection = true;
            Selection = (Bitmap)Image.FromStream(strm);
            SelectionZone.X = 0;
            SelectionZone.Y = 0;
            SelectionZone.Width = Selection.Width;
            SelectionZone.Height = Selection.Height;
            PNL_Canvas.Invalidate();
        }

        private Color OverColor(Color ColA, Color ColB)
        {
            float Alphaf = ColA.A / 255;
            byte R = (byte)(ColA.R * (Alphaf) + ColB.R * (1 - Alphaf));
            byte G = (byte)(ColA.G * (Alphaf) + ColB.G * (1 - Alphaf));
            byte B = (byte)(ColA.B * (Alphaf) + ColB.B * (1 - Alphaf));

            int AlphaTemp = ColA.A + ColB.A;
            byte A = (byte)AlphaTemp;
            if (AlphaTemp > 255)
                A = 255;

            return Color.FromArgb(A, R, G, B);
        }

        private void MoveSelection(MouseEventArgs e)
        {
            int MoveByX;
            int MoveByY;
            MovingSelection = true;
            
            CurrentPoint = GetCursorLocationRelative(e);

            MoveByX = OldPoint.X - CurrentPoint.X;
            MoveByY = OldPoint.Y - CurrentPoint.Y;

            SelectionZone.X -= MoveByX;
            SelectionZone.Y -= MoveByY;

            SelectionZone = FormatRectanglePositiveCoord(SelectionZone);
            

            OldPoint = CurrentPoint;
            PNL_Canvas.Invalidate();
            DebugWindow.UpdateValues(ActiveSelection, DraggingSelection, SelectionZone);
        }

        private void DrawSelectionZone(Graphics g_)
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

        private void DrawSelection(Graphics g_)
        {
            if (ActiveSelection && Selection != null)
            {
                Rectangle DrawZone = FormatRectanglePositiveCoord(SelectionZone);
                g_.DrawImage(Selection, new RectangleF(DrawZone.X * Zoom, DrawZone.Y * Zoom, DrawZone.Width * Zoom, DrawZone.Height * Zoom), new RectangleF(-0.5f, -0.5f, Selection.Width, Selection.Height), GraphicsUnit.Pixel);
            }
        }

        private bool PointInRectangle(Point p_, Rectangle r_)
        {
            return ((p_.X > r_.X && p_.X < r_.X + r_.Width) && (p_.Y > r_.Y && p_.Y < r_.Y + r_.Height));
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
            if(ActiveSelection)
           PNL_Canvas.ContextMenuStrip = CTM_Selection_Options;
        }

        private void CopySelectionIntoClipboard()
        {
            if (ActiveSelection)
            {
                SelectionZone = FormatRectanglePositiveCoord(SelectionZone);
                SelectionZone = FormatRectangleInbound(SelectionZone);
                
                PlaceInClipboard(Selection);
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
                        Sprite.SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                    }
                }
                
                PNL_Canvas.Invalidate();
                FileChanged = true;
            }
        }

        private void RemoveSelection()
        {
            ActiveSelection = false;
            Selection = null;
            PNL_Canvas.Invalidate();
            ClearTimelineSelection();
        }

        private void CutSelection()
        {
            CopySelectionIntoClipboard();
            RemoveSelection();
        }

    }
}

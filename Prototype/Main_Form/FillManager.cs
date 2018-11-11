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
        private void BTN_Fill_CheckedChanged(object sender, EventArgs e) => SetTool();

        private void Fill(Color NewCol, MouseEventArgs e)
        {
            if (ActiveSelection)
                Canvas = Graphics.FromImage(Selection);
            else
                Canvas = Graphics.FromImage(Sprite);

            OldPoint = AdaptPointToSelection(GetCursorLocationRelative(e));

            Bitmap img = new Bitmap(1, 1);
            bool ActiveFill = false;
            if (ActiveSelection)
            {
                if (OldPoint.X < Selection.Width && OldPoint.X >= 0)
                {
                    if (OldPoint.Y < Selection.Height && OldPoint.Y >= 0)
                    {
                        img = Selection;
                        ActiveFill = true;
                    }
                }
            }
            else
            {
                img = Sprite;
                ActiveFill = true;
            }

            if (ActiveFill)
            {
                
                Begin_Fill(ref img, OldPoint, NewCol);
                if (ActiveSelection)
                    Selection = img;
                else
                    Sprite = img;
                PNL_Canvas.Invalidate();
                FileChanged = true;
            }
        }

        private void Begin_Fill(ref Bitmap img, Point StartPoint, Color NewCol)
        {
            Color OldCol = Sprite.GetPixel(StartPoint.X,StartPoint.Y);
            if (OldCol.ToArgb() != NewCol.ToArgb())
            {
                UpdateTimeline();
                List<Point> Painters = new List<Point>();

                Painters.Add(OldPoint);
                img.SetPixel(OldPoint.X, OldPoint.Y, NewCol);
                int i = 0;

                int x_ = 0;
                int y_ = 0;

                int X = OldPoint.X;
                int Y = OldPoint.Y;
                int LastPainterIndex;

                while (Painters.Count != 0)
                {
                    LastPainterIndex = Painters.Count - 1;
                    X = Painters[LastPainterIndex].X;
                    Y = Painters[LastPainterIndex].Y;

                    Painters.RemoveAt(LastPainterIndex);
                    for (int j = -1; j < 6; j += 2)
                    {
                        if (j < 2)
                        {
                            x_ = j;
                            y_ = 0;
                        }
                        else
                        {
                            x_ = 0;
                            y_ = j - 4;
                        }

                        if (Fill_PixelToChange(ref img, ref OldCol, X + x_, Y + y_))
                        {
                            img.SetPixel(X + x_, Y + y_, NewCol);
                            Painters.Add(new Point(X + x_, Y + y_));
                        }
                    }
                    i++;
                }
            }
        }

        private bool Fill_PixelToChange(ref Bitmap img, ref Color Col, int X, int Y)
        {
            if (X >= 0 && X < img.Width)
            {
                if (Y >= 0 && Y < img.Height)
                {
                    return Col.Equals(img.GetPixel(X, Y));
                }
            }
            return false;
        }

        private void Fill_SetPixel(ref Bitmap Img, ref Color OldColor, ref Color NewColor, int X, int Y)
        {
            if (X < Img.Width && X >= 0)
            {
                if (Y < Img.Height && Y >= 0)
                {
                    if (Img.GetPixel(X, Y) == OldColor)
                    {
                        Img.SetPixel(X, Y, NewColor);
                    }
                }
            }
        }
    }
}

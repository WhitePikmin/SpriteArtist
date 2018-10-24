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

            /*
            if (ActiveSelection)
            {
                if (OldPoint.X < Selection.Width && OldPoint.X >= 0)
                {
                    if (OldPoint.Y < Selection.Height && OldPoint.Y >= 0)
                    {
                        Color OldCol = Selection.GetPixel(OldPoint.X, OldPoint.Y);
                        if(!OldCol.Equals(NewCol))
                            Fill_SetPixel(ref Selection, ref OldCol, ref NewCol, OldPoint.X, OldPoint.Y);
                    }
                }
            }
            else
            {
            */
            Color OldCol = Sprite.GetPixel(OldPoint.X, OldPoint.Y);
            if (!OldCol.Equals(NewCol))
            {
                List<Point> Painters = new List<Point>();
                Bitmap img = Sprite;
                Painters.Add(OldPoint);
                img.SetPixel(OldPoint.X,OldPoint.Y, NewCol);
                int i = 0;
                int NbPainters = 0;
                while(i <= NbPainters)
                {
                    int Max_i = Painters.Count;
                        int X = Painters[i].X;
                        int Y = Painters[i].Y;
                        //Painters.RemoveAt(i);
                        
                        for (int j = -1; j < 6; j+=2)
                        {
                            int x_ = 0;
                            int y_ = 0;
                            if (j < 2)
                                x_ = j;
                            else
                                y_ = j-4;

                            if (Fill_PixelToChange(ref img, ref OldCol,X + x_,Y + y_))
                            {
                                img.SetPixel(X + x_, Y + y_, NewCol);
                                Painters.Add(new Point(X + x_,Y + y_));
                                NbPainters++;
                            }
                        }
                    i++;
                }
                Sprite = img;
            }

            //}

            PNL_Canvas.Invalidate();
            FileChanged = true;
        }

        private bool Fill_PixelToChange(ref Bitmap img,ref Color Col, int X, int Y)
        {
            if (X >= 0 && X < img.Width)
            {
                if (Y >= 0 && Y < img.Height)
                {
                    return Col.Equals(img.GetPixel(X,Y));
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

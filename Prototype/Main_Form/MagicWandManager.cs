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
        private void BTN_MagicWand_CheckedChanged(object sender, EventArgs e) => SetTool();

        private void MagicWand(MouseEventArgs e)
        {
            if(ActiveSelection)
            StopSelecting();

            OldPoint = AdaptPointToSelection(GetCursorLocationRelative(e));

            bool[,] PixelsSelected = Begin_MagicWand(ref Sprite, OldPoint);
            int NewSelection_MinX = PixelsSelected.GetLength(0) - 1;
            int NewSelection_MinY = PixelsSelected.GetLength(1) - 1;
            int NewSelection_MaxX = 0, NewSelection_MaxY = 0;

            for (int j = 0; j < PixelsSelected.GetLength(0) ; j++)
            {
                for (int k = 0; k < PixelsSelected.GetLength(1); k++)
                {
                    if (PixelsSelected[j, k])
                    {
                        if (j > NewSelection_MaxX)
                            NewSelection_MaxX = j;

                        if (j < NewSelection_MinX)
                            NewSelection_MinX = j;

                        if (k > NewSelection_MaxY)
                            NewSelection_MaxY = k;

                        if (k < NewSelection_MinY)
                            NewSelection_MinY = k;
                    }
                }
            }
            int NewSelection_Width = NewSelection_MaxX - NewSelection_MinX + 1;
            int NewSelection_Height = NewSelection_MaxY - NewSelection_MinY + 1;

            if (NewSelection_Width > 0 && NewSelection_Height > 0)
            {
                Bitmap NewSelection = new Bitmap(NewSelection_Width, NewSelection_Height);
                for (int j = NewSelection_MinX; j <= NewSelection_MaxX; j++)
                {
                    for (int k = NewSelection_MinY; k <= NewSelection_MaxY; k++)
                    {
                        Color NewCol = new Color();
                        if (PixelsSelected[j, k])
                        {
                            NewCol = Sprite.GetPixel(j, k);
                            Sprite.SetPixel(j, k, Color.Transparent);
                        }
                        else
                            NewCol = Color.Transparent;
                        NewSelection.SetPixel(j - NewSelection_MinX, k - NewSelection_MinY, NewCol);
                    }
                }
                StartSelectingMagicWand(ref NewSelection, new Point(NewSelection_MinX, NewSelection_MinY));
            }    
        }

        private bool[,] Begin_MagicWand(ref Bitmap img, Point StartPoint)
        {
            bool[,] PixelsSelected = new bool[img.Width, img.Height];

            for (int j = 0; j < PixelsSelected.GetLength(0); j++)
            {
                for (int k = 0; k < PixelsSelected.GetLength(1); k++)
                {
                    PixelsSelected[j, k] = false;
                }
            }

            Color OldCol = Sprite.GetPixel(StartPoint.X,StartPoint.Y);
            List<Point> Finders = new List<Point>();

            Finders.Add(OldPoint);
            PixelsSelected[OldPoint.X, OldPoint.Y] = true;
                int i = 0;

                int x_ = 0;
                int y_ = 0;

                int X = OldPoint.X;
                int Y = OldPoint.Y;
                int LastFinderIndex;

                while (Finders.Count != 0)
                {
                    LastFinderIndex = Finders.Count - 1;
                    X = Finders[LastFinderIndex].X;
                    Y = Finders[LastFinderIndex].Y;

                    Finders.RemoveAt(LastFinderIndex);
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

                        if (MagicWand_PixelToChange(ref img, ref OldCol, ref PixelsSelected , X + x_, Y + y_))
                        {
                        MagicWand_SetPixel(ref img,ref PixelsSelected,X + x_, Y + y_);
                        Finders.Add(new Point(X + x_, Y + y_));
                        }
                    }
                    i++;
                }
            return PixelsSelected;
            
        }

        private bool MagicWand_PixelToChange(ref Bitmap img, ref Color Col, ref bool[,] PixelsSelected, int X, int Y)
        {
            if (X >= 0 && X < img.Width)
            {
                if (Y >= 0 && Y < img.Height)
                {
                    return (Col.Equals(img.GetPixel(X, Y)) && !PixelsSelected[X,Y]);
                }
            }
            return false;
        }

        private void MagicWand_SetPixel(ref Bitmap Img, ref bool[,] PixelsSelected, int X, int Y)
        {
            if (X < Img.Width && X >= 0)
            {
                if (Y < Img.Height && Y >= 0)
                {
                    PixelsSelected[X, Y] = true;
                }
            }
        }

        private void StartSelectingMagicWand(ref Bitmap NewSelection, Point UpperLeftCorner)
        {
            ActiveSelection = true;
            Selection = NewSelection;
            SelectionZone.X = UpperLeftCorner.X;
            SelectionZone.Y = UpperLeftCorner.Y;
            SelectionZone.Width = NewSelection.Width;
            SelectionZone.Height = NewSelection.Height;
            BTN_Select.Checked = true;
            PNL_Canvas.Invalidate();
        }
    }
}

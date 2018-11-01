using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;

namespace SpriteArtist
{

    public partial class FRM_Main
    {
        private DialogResult SaveFileAs()
        {
            if (DLG_Save.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(DLG_Save.FileName);
                ImageFormat format = ImageFormat.Png;
                switch (extension.ToLower())
                {
                    case ".bmp": format = ImageFormat.Bmp; break;
                    case ".gif": format = ImageFormat.Gif; break;
                }
                if (OGAnimationFrame.Count > 1 && format == ImageFormat.Png)
                {
                    Bitmap imgSaving = new Bitmap(Sprite.Width * OGAnimationFrame.Count, Sprite.Height);
                    for (int i = 0; i < OGAnimationFrame.Count; i++)
                    {

                        for (int j = 0; j < Sprite.Width; j++)
                        {

                            for (int k = 0; k < Sprite.Height; k++)
                            {
                                imgSaving.SetPixel(i * Sprite.Width + j, k, OGAnimationFrame[i].Image.GetPixel(j, k));
                            }
                        }
                    }
                    imgSaving.Save(DLG_Save.FileName, ImageFormat.Png);
                }
                else
                    Sprite.Save(DLG_Save.FileName, format);
                FileName = DLG_Save.FileName;
                FileChanged = false;
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }

        private DialogResult PromptSave()
        {
            return MessageBox.Show("Voulez vous sauvegarder les modifications avant de fermer le programme?", "Avertissement", MessageBoxButtons.YesNoCancel);
        }

        private DialogResult OpenFile()
        {
            if (DLG_Open.ShowDialog() == DialogResult.OK)
            {
                bool Open;
                if (FileChanged)
                {
                    DialogResult result = PromptSave();
                    if (result == DialogResult.Yes)
                    {
                        result = SaveFileAs();
                        Open = (result == DialogResult.OK);
                    }
                    else
                        Open = (result != DialogResult.Cancel);
                }
                else
                    Open = true;

                if (Open)
                {
                    LoadImage(new Bitmap(DLG_Open.FileName));
                }
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }
        private void SaveAsSprite(string path)
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = "Script\\sprite.exe";
            psi.Arguments = path;
            Process.Start(psi);
        }
        private void LoadImage(Bitmap ImageOpened)
        {
            Sprite = ImageOpened;
            Canvas = Graphics.FromImage(Sprite);

            Canvas_Width = (ushort)Sprite.Width;
            Canvas_Height = (ushort)Sprite.Height;
            PNL_Canvas.Width = Sprite.Width;
            PNL_Canvas.Height = Sprite.Height;
            ChangeZoom(INITIAL_ZOOM);
            CenterCanvas();
        }

    }
}

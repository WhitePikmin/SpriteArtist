using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;
using System.IO.Packaging;
using System.Windows.Media.Imaging;

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
                    Bitmap imgSaving = new Bitmap(AnimationSave());
                    imgSaving.Save(DLG_Save.FileName, ImageFormat.Png);
                    addImageComment(DLG_Save.FileName, OGAnimationFrame[0].Image.Width.ToString(), TBAR_FrameRate.Value.ToString());
                }
                else
                    Sprite.Save(DLG_Save.FileName, format);
                FileName = DLG_Save.FileName;
                FileChanged = false;
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }
        private Bitmap AnimationSave()
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
            return imgSaving;
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
        public void addImageComment(string imageFlePath, string width, string fps)
        {
            string jpegDirectory = Path.GetDirectoryName(imageFlePath);
            string jpegFileName = Path.GetFileNameWithoutExtension(imageFlePath);

            BitmapDecoder decoder = null;
            BitmapFrame bitmapFrame = null;
            BitmapMetadata metadata = null;
            FileInfo originalImage = new FileInfo(imageFlePath);

            if (File.Exists(imageFlePath))
            {
                // load the jpg file with a JpegBitmapDecoder    
                using (Stream jpegStreamIn = File.Open(imageFlePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    decoder = new PngBitmapDecoder(jpegStreamIn, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                }

                bitmapFrame = decoder.Frames[0];
                metadata = (BitmapMetadata)bitmapFrame.Metadata;

                if (bitmapFrame != null)
                {
                    BitmapMetadata metaData = (BitmapMetadata)bitmapFrame.Metadata.Clone();

                    if (metaData != null)
                    {
                        // modify the metadata   
                        metaData.SetQuery("/tEXt/{str=Description}", width + "|" + fps);

                        // get an encoder to create a new jpg file with the new metadata.      
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapFrame, bitmapFrame.Thumbnail, metaData, bitmapFrame.ColorContexts));
                        //string jpegNewFileName = Path.Combine(jpegDirectory, "JpegTemp.jpg");

                        // Delete the original
                        originalImage.Delete();

                        // Save the new image 
                        using (Stream jpegStreamOut = File.Open(imageFlePath, FileMode.CreateNew, FileAccess.ReadWrite))
                        {
                            encoder.Save(jpegStreamOut);
                        }
                    }
                }
            }
        }
    }
}

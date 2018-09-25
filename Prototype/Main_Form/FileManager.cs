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

namespace Prototype
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
                Sprite.Save(DLG_Save.FileName, format);
                FileName = DLG_Save.FileName;
                LastSave = new Bitmap(Sprite);
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }

    }
}

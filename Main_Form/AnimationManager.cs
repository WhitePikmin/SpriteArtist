using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SpriteArtist
{
    public partial class FRM_Main
    {

        List<FrameWithIndex> OGAnimationFrame = new List<FrameWithIndex>();

        double _Fps = 1;
        int _Index = 0;
        private int _FrameNum = 0;
        bool Calque = false;

        private void TMR_FrameRate_Tick(object sender, EventArgs e)
        {
            if (_Index >= OGAnimationFrame.Count)
                _Index = 0;

            PBX_Animation.InitialImage = null;
            if (OGAnimationFrame.Count != 0)
                PBX_Animation.Image = OGAnimationFrame[_Index].Image;
            PBX_Animation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            _Index++;
        }
        private void CalculateFrameRate()
        {
            _Fps = TBAR_FrameRate.Value;
            double tick_Value = 1000 / _Fps;
            TMR_FrameRate.Interval = (int)Math.Round(tick_Value, MidpointRounding.AwayFromZero);
        }
        private void TBAR_FrameRate_ValueChanged(object sender, EventArgs e)
        {
            CalculateFrameRate();
            LB_fps_value.Text = _Fps.ToString();
        }

        private void Add_Flow_Layout_Panel(ref Bitmap bitmap)
        {
            PictureBox newPictureBox = new PictureBox();
            newPictureBox.Name = _FrameNum.ToString();
            Bitmap resized = new Bitmap(bitmap, new Size(30, 30));

            newPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            newPictureBox.Image = resized;
            newPictureBox.Width = 60;
            newPictureBox.Height = 60;
            newPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.p_MouseDown);
            newPictureBox.Paint += new PaintEventHandler(this.PictureBox_Paint);
            FLP_All_Frame.Controls.Add(newPictureBox);
            _FrameNum++;
        }
        private void Update_Flow_Layout_Panel()
        {
            _FrameNum = 0;
            for (int i = 0; i < OGAnimationFrame.Count; i++)
            {
                PictureBox pbx = (PictureBox)FLP_All_Frame.Controls[i];
                OGAnimationFrame[i].NewIndex = i;
                pbx.Image = OGAnimationFrame[i].Image;
                pbx.Name = OGAnimationFrame[i].NewIndex.ToString();
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
        }

        private void RefreshSelect()
        {
            foreach (PictureBox pbx in FLP_All_Frame.Controls)
            {
                pbx.BackColor = Color.Transparent;
                pbx.Padding = new Padding(0);
            }
        }
        private ref Bitmap AddBitmapToAnimation()
        {
            Bitmap bitmap = new Bitmap(Sprite.Width, Sprite.Height, PixelFormat.Format32bppArgb);

            OGAnimationFrame.Add(new FrameWithIndex(bitmap, OGAnimationFrame.Count));
            Sprite = bitmap;
            PNL_Canvas.Invalidate();
            return ref Sprite;
        }
        private ref Bitmap AddLastFrameToAnimation()
        {
            Bitmap bitmap = new Bitmap(OGAnimationFrame.Last().Image, Sprite.Width, Sprite.Height);

            OGAnimationFrame.Add(new FrameWithIndex(bitmap, OGAnimationFrame.Count));
            Sprite = bitmap;
            PNL_Canvas.Invalidate();
            return ref Sprite;
        }
        #region evenement
        private void BTN_Add_Frame_Click(object sender, EventArgs e) => Add_Flow_Layout_Panel(ref AddBitmapToAnimation());
        private void BTN_Add_LastFrame_Click(object sender, EventArgs e) => Add_Flow_Layout_Panel(ref AddLastFrameToAnimation());

        private void activerCalqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Calque = !Calque; // Inverse l'état du calque ex: Activer -> Desactiver
            for (int i = 0; i < FLP_All_Frame.Controls.Count; i++)
            {
                PictureBox pbx = (PictureBox)FLP_All_Frame.Controls[i];
                if (pbx.BackColor == Color.DarkGray)
                {
                    PictureBox pb = new PictureBox();
                    PNL_Canvas.Controls.Add(pb);
                    pb.Location = new Point(0, 0);
                    pb.BackColor = Color.Transparent;
                    pb.Width = PNL_Canvas.Width;
                    pb.Height = PNL_Canvas.Height;
                    pb.Image = OGAnimationFrame[i - 1].Image;    
                }
            }*/
        }
        private void BTN_Start_Click(object sender, EventArgs e)
        {
            CalculateFrameRate();
            TMR_FrameRate.Enabled = true;
        }
        private void BTN_Stop_Click(object sender, EventArgs e) => TMR_FrameRate.Enabled = false;
        private void activerAnimationToolStripMenuItem_Click(object sender, EventArgs e) => PNL_Animation.Visible = !PNL_Animation.Visible;
        private void FLP_All_Frame_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.All;
        private void FLP_All_Frame_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox picture = (PictureBox)e.Data.GetData(typeof(PictureBox));
            FlowLayoutPanel _source = (FlowLayoutPanel)picture.Parent;
            FlowLayoutPanel _destination = (FlowLayoutPanel)sender;

            Point p = _destination.PointToClient(new Point(e.X, e.Y));
            var item = _destination.GetChildAtPoint(p);
            int index = _destination.Controls.GetChildIndex(item, false);
            _destination.Controls.SetChildIndex(picture, index);
            _destination.Invalidate();
            if (index >= 0 && index < OGAnimationFrame.Count)
            {

                if (OGAnimationFrame[int.Parse(picture.Name)].NewIndex > index)
                {
                    for (int i = index; i < OGAnimationFrame.Count; i++)
                    {
                        OGAnimationFrame[i].OldIndex = OGAnimationFrame[i].NewIndex;
                        OGAnimationFrame[i].NewIndex = OGAnimationFrame[i].NewIndex + 1;
                    }
                }
                else if (OGAnimationFrame[int.Parse(picture.Name)].NewIndex < index)
                {
                    for (int i = index; i < OGAnimationFrame.Count; i++)
                    {
                        OGAnimationFrame[i].OldIndex = OGAnimationFrame[i].NewIndex;
                        OGAnimationFrame[i].NewIndex = OGAnimationFrame[i].NewIndex - 1;
                    }
                }
                OGAnimationFrame[int.Parse(picture.Name)].OldIndex = OGAnimationFrame[int.Parse(picture.Name)].NewIndex;
                OGAnimationFrame[int.Parse(picture.Name)].NewIndex = index;
                OGAnimationFrame = OGAnimationFrame.OrderBy(x => x.NewIndex).ToList();
                Sprite = OGAnimationFrame[int.Parse(picture.Name)].Image;
                PNL_Canvas.Invalidate();
                Update_Flow_Layout_Panel();
            }
        }
        private void p_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            RefreshSelect();
            p.BackColor = Color.DarkGray;
            p.Padding = new Padding(5);
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                p.DoDragDrop(p, DragDropEffects.All);
            }
        }
        private void BTN_Supp_Frame_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FLP_All_Frame.Controls.Count; i++)
            {
                PictureBox pbx = (PictureBox)FLP_All_Frame.Controls[i];
                if (pbx.BackColor == Color.DarkGray)
                {
                    FLP_All_Frame.Controls.RemoveAt(i);
                    OGAnimationFrame.RemoveAt(i);
                }
            }
        }
    }
#endregion
    public class FrameWithIndex
    {
        public Bitmap Image { get; set; }
        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
        public FrameWithIndex(Bitmap bitmap, int index)
        {
            Image = bitmap;
            NewIndex = index;
        }
    }
}
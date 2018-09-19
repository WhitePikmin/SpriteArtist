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
    public partial class FRM_Main : Form
    {
        ushort Canvas_Width = 640;
        ushort Canvas_Height = 480;
        float Zoom = 1;

        string FileName;

        Point OldPoint = new Point();
        Point CurrentPoint = new Point();

        Point OldDragPoint = new Point();
        Point CurrentDragPoint = new Point();

        Bitmap Sprite = new Bitmap(1, 1);
        Bitmap LastSave;
        Graphics Canvas;
        Pen MainPen = new Pen(Color.Black,1);
        Pen SecondPen = new Pen(Color.Black, 1);
        System.Drawing.Drawing2D.LineCap LineCap = System.Drawing.Drawing2D.LineCap.Round;
        System.Drawing.Drawing2D.DashCap DashCap = System.Drawing.Drawing2D.DashCap.Round;

        enum Tool {Pen,Eraser,ColorPicker,Line,Select,Bucket,Zoom};
        Tool CurrentTool = Tool.Pen;


        public FRM_Main(ushort NewWidth, ushort NewHeight)
        {
            InitializeComponent();

            Canvas_Height = NewHeight;
            Canvas_Width = NewWidth;

            PNL_Canvas.Width = Canvas_Width;
            PNL_Canvas.Height = Canvas_Height;

            Sprite = new Bitmap(Sprite, new Size(Canvas_Width, Canvas_Height));
            Canvas = Graphics.FromImage(Sprite);

            InitCanvas();
        }

        public FRM_Main(Bitmap ImageOpened)
        {
            InitializeComponent();

            Sprite = ImageOpened;
            Canvas = Graphics.FromImage(Sprite);

            Canvas_Width = (ushort)Sprite.Width;
            Canvas_Height = (ushort)Sprite.Height;
            PNL_Canvas.Width = Sprite.Width;
            PNL_Canvas.Height = Sprite.Height;

            InitCanvas();
        }

        private void InitCanvas()
        {
            
            Canvas = PNL_Canvas.CreateGraphics();

            MainPen.SetLineCap(LineCap, LineCap, DashCap);
            SecondPen.SetLineCap(LineCap, LineCap, DashCap);

            ChangeColorMainPen(Color.Black);
            ChangeColorSecondPen(Color.White);

            CenterCanvas();

            LastSave = new Bitmap(Sprite);

            //Activer le "Double buffer"
            //Éviter que le canevas clignote
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            PNL_Canvas, new object[] { true });

            //listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            ChangeZoom(4);
        }

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
                Sprite.Save(DLG_Save.FileName,format);
                FileName = DLG_Save.FileName;
                LastSave = new Bitmap(Sprite);
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }


        private void CenterCanvas()
        {
            PNL_Canvas.Location = new Point(PNL_Drag_Zone.Width / 2 - Canvas_Width / 2, PNL_Drag_Zone.Height / 2 - Canvas_Height / 2);
        }

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

        }

        private Point GetCursorLocationRelative(MouseEventArgs e)
        {
            return new Point((int)((e.X / Zoom) + 0.5),(int)((e.Y /  Zoom) + 0.5));
        }

        private void Draw()
        {

        }

        private void EraseOnCanvas(Pen pen_, MouseEventArgs e)
        {
            Pen Eraser = new Pen(Color.Transparent);
            Eraser.Width = pen_.Width;
            Eraser.SetLineCap(LineCap, LineCap, DashCap);

            Canvas.CompositingMode = CompositingMode.SourceCopy;

            CurrentPoint = GetCursorLocationRelative(e);
            Canvas.DrawLine(Eraser, OldPoint, CurrentPoint);
            OldPoint = CurrentPoint;
            PNL_Canvas.Invalidate();
        }

        private void DrawOnCanvas(Pen pen_, MouseEventArgs e)
        {
            Canvas = Graphics.FromImage(Sprite);
            CurrentPoint = GetCursorLocationRelative(e);
            Canvas.DrawLine(pen_, OldPoint, CurrentPoint);
            OldPoint = CurrentPoint;
            //Sprite = new Bitmap(Canvas_Width,Canvas_Height,Canvas);
            PNL_Canvas.Invalidate();
        }

        private void EraseSingleDotOnCanvas(Color col_, MouseEventArgs e)
        {
            Pen Eraser = new Pen(Color.Transparent);
            Eraser.Width = MainPen.Width;
            Eraser.SetLineCap(LineCap, LineCap, DashCap);

            Canvas.CompositingMode = CompositingMode.SourceCopy;

            Canvas = Graphics.FromImage(Sprite);
            OldPoint = GetCursorLocationRelative(e);
            int PenWidthHalf = (int)Math.Ceiling(Eraser.Width / 2);

            if (Eraser.Width < 3)
                Canvas.FillRectangle(new SolidBrush(col_), OldPoint.X + 1 - PenWidthHalf, OldPoint.Y + 1 - PenWidthHalf, Eraser.Width, Eraser.Width);
            else
                Canvas.FillEllipse(new SolidBrush(col_), OldPoint.X - PenWidthHalf, OldPoint.Y - PenWidthHalf, Eraser.Width, Eraser.Width);
            //Sprite = new Bitmap(Canvas_Width, Canvas_Height, Canvas);
            PNL_Canvas.Invalidate();
        }

        private void DrawSingleDotOnCanvas(Color col_, MouseEventArgs e)
        {
            Canvas = Graphics.FromImage(Sprite);
            OldPoint = GetCursorLocationRelative(e);
            int PenWidthHalf = (int)Math.Ceiling(MainPen.Width / 2);
            if (MainPen.Width < 3)
                Canvas.FillRectangle(new SolidBrush(col_), OldPoint.X + 1 - PenWidthHalf, OldPoint.Y + 1 - PenWidthHalf, MainPen.Width, MainPen.Width);
            else
                Canvas.FillEllipse(new SolidBrush(col_), OldPoint.X - PenWidthHalf, OldPoint.Y - PenWidthHalf, MainPen.Width, MainPen.Width);
            //Sprite = new Bitmap(Canvas_Width, Canvas_Height, Canvas);
            PNL_Canvas.Invalidate();
        }

        private void ChangeColorMainPen(Color col)
        {
            MainPen.Color = col;
            BTN_MainColor.BackColor = col;
        }

        private void ChangeColorSecondPen(Color col)
        {
            SecondPen.Color = col;
            BTN_SecondColor.BackColor = col;
        }

        private void ChangePenSize(float size_)
        {
            MainPen.Width = size_;
            SecondPen.Width = size_;
        }

        private void FRM_Main_Load(object sender, EventArgs e)
        {

        }

        private void PNL_Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.DrawImage(Sprite, new Rectangle(0,0,PNL_Canvas.Width+1,PNL_Canvas.Height+1),new Rectangle(0,0,Sprite.Width,Sprite.Height),GraphicsUnit.Pixel);
        }



        private void PNL_Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Cross;
            if (e.Button == MouseButtons.Left)
            {
                switch (CurrentTool)
                {
                    case Tool.Pen: DrawOnCanvas(MainPen, e); break;
                    case Tool.Eraser: EraseOnCanvas(MainPen, e); break;
                }
            }

            else
            if (e.Button == MouseButtons.Right)
            {
                switch (CurrentTool)
                {
                    case Tool.Pen: DrawOnCanvas(SecondPen, e); break;
                    case Tool.Eraser: EraseOnCanvas(SecondPen, e); break;
                }
            }  
            else

            if (e.Button == MouseButtons.Middle)
            {
                DragCanvas(PNL_Drag_Zone.PointToClient(Cursor.Position));
            }
        }

        private void PNL_Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (CurrentTool)
                {
                    case Tool.Pen: DrawSingleDotOnCanvas(MainPen.Color,e); break;
                    case Tool.Eraser: DrawSingleDotOnCanvas(Color.Transparent, e); break;
                }
                
            }
            else
            if (e.Button == MouseButtons.Right)
            {
                switch (CurrentTool)
                {
                    case Tool.Pen: DrawSingleDotOnCanvas(SecondPen.Color, e); break;
                    case Tool.Eraser: DrawSingleDotOnCanvas(Color.Transparent, e); break;
                }
            }


            if (e.Button == MouseButtons.Middle)
            {
                OldDragPoint = PNL_Drag_Zone.PointToClient(Cursor.Position);
                CurrentDragPoint = OldDragPoint;
                
            }
        }

        private void PNL_Drag_Zone_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                DragCanvas(e.Location);
            }
        }

        private void PNL_Drag_Zone_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Cross;
            if (e.Button == MouseButtons.Middle)
            {
                OldDragPoint = e.Location;
                CurrentDragPoint = OldDragPoint;
            }
        }

        private void NUM_Pen_Size_ValueChanged(object sender, EventArgs e)
        {
            ChangePenSize((float)NUM_Pen_Size.Value);
        }

        private void BTN_MainColor_Click(object sender, EventArgs e)
        {
            if (DLG_Color.ShowDialog() == DialogResult.OK)
            {
                ChangeColorMainPen(DLG_Color.Color);
            }
        }

        private void BTN_Add_Color_Click(object sender, EventArgs e)
        {
            
        }

        private void SetTool()
        {
            if (BTN_Pen.Checked)
                CurrentTool = Tool.Pen;
            else
            {
                if (BTN_Erase.Checked)
                    CurrentTool = Tool.Eraser;
            }
        }

        private void BTN_Pen_CheckedChanged(object sender, EventArgs e)
        {
            SetTool();
        }

        private void BTN_Erase_CheckedChanged(object sender, EventArgs e)
        {
            SetTool();
        }

        private void BTN_ZoomIn_Click(object sender, EventArgs e)
        {
        }

        private void PNL_Canvas_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Cross;
        }

        private void BTN_Save_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRM_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Sprite != LastSave)
            {
                DialogResult dlg = MessageBox.Show("Voulez vous sauvegarder les modifications avant de fermer le programme?", "Avertissement", MessageBoxButtons.YesNoCancel);
                if (dlg == DialogResult.Yes)
                {
                    if (SaveFileAs() == DialogResult.OK)
                    {
                        Application.ExitThread();
                    }
                    else
                        e.Cancel = true;
                }
                else if (dlg == DialogResult.No)
                {
                    Application.ExitThread();
                }
                else
                    e.Cancel = true;
            }
        }


        //Pour les layers qui sont locked
        /*private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if(e.Index == 1)
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(),new Font("Arial",10, FontStyle.Strikeout), Brushes.Black, e.Bounds);
            else
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }*/
    }
}

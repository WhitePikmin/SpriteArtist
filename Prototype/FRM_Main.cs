using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Prototype
{
    public partial class FRM_Main : Form
    {
        ushort Canvas_Width = 640;
        ushort Canvas_Height = 480;

        Point OldPoint = new Point();
        Point CurrentPoint = new Point();

        Point OldDragPoint = new Point();
        Point CurrentDragPoint = new Point();

        Bitmap Sprite = new Bitmap(1, 1);
        Graphics Canvas;
        Pen MainPen = new Pen(Color.Black,1);
        Pen SecondPen = new Pen(Color.Black, 1);
        System.Drawing.Drawing2D.LineCap LineCap = System.Drawing.Drawing2D.LineCap.Round;
        System.Drawing.Drawing2D.DashCap DashCap = System.Drawing.Drawing2D.DashCap.Round;


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

            //Activer le "Double buffer"
            //Éviter que le canevas clignote
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            PNL_Canvas, new object[] { true });
        }

        public FRM_Main(Bitmap ImageOpened)
        {
            InitializeComponent();

            Sprite = ImageOpened;
            Canvas_Width = (ushort)Sprite.Width;
            Canvas_Height = (ushort)Sprite.Height;
            Canvas = Graphics.FromImage(Sprite);

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

            //listBox1.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void CenterCanvas()
        {
            PNL_Canvas.Location = new Point(PNL_Drag_Zone.Width / 2 - Canvas_Width / 2, PNL_Drag_Zone.Height / 2 - Canvas_Height / 2);
        }

        private void DragCanvas(Point MouseLocation)
        {
            CurrentDragPoint = MouseLocation;
            PNL_Canvas.Location = new Point(PNL_Canvas.Location.X + (OldDragPoint.X - CurrentDragPoint.X), PNL_Canvas.Location.Y + (OldDragPoint.Y - CurrentDragPoint.Y));
            OldDragPoint = CurrentDragPoint;
            Cursor.Current = Cursors.Hand;
        }

        private void Draw()
        {

        }

        private void DrawOnCanvas(Pen pen_, MouseEventArgs e)
        {
            Canvas = Graphics.FromImage(Sprite);
            CurrentPoint = e.Location;
            Canvas.DrawLine(pen_, OldPoint, CurrentPoint);
            OldPoint = CurrentPoint;
            //Sprite = new Bitmap(Canvas_Width,Canvas_Height,Canvas);
            PNL_Canvas.Invalidate();
        }

        private void DrawSingleDotOnCanvas(Color col_, MouseEventArgs e)
        {
            Canvas = Graphics.FromImage(Sprite);
            OldPoint = e.Location;
            int PenWidthHalf = (int)Math.Ceiling(MainPen.Width / 2);
            if (MainPen.Width < 3)
                Canvas.FillRectangle(new SolidBrush(col_), OldPoint.X - PenWidthHalf, OldPoint.Y - PenWidthHalf, MainPen.Width, MainPen.Width);
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
            e.Graphics.DrawImage(Sprite, new Point(0, 0));
        }

        private void PNL_Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DrawOnCanvas(MainPen, e);
            else
            if (e.Button == MouseButtons.Right)
                DrawOnCanvas(SecondPen, e);
     
            if (e.Button == MouseButtons.Middle)
            {
                DragCanvas(PNL_Drag_Zone.PointToClient(Cursor.Position));
            }
        }

        private void PNL_Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DrawSingleDotOnCanvas(MainPen.Color,e);
            }
            else
            if (e.Button == MouseButtons.Right)
            {
                DrawSingleDotOnCanvas(SecondPen.Color, e);
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

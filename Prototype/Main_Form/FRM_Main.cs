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

namespace SpriteArtist
{
    public partial class FRM_Main : Form
    {
        ushort Canvas_Width = 640;
        ushort Canvas_Height = 480;
        float Zoom = 1;
        const float ZOOM_MAX = 128;
        const float ZOOM_MIN = 0.5f;
        const float INITIAL_ZOOM = 4;
        int FrameTimer = 0;

        bool DisplayGrid = false;
        bool FileChanged = false;

        bool DraggingSelection = false;
        bool ActiveSelection = false;
        Rectangle SelectionZone = new Rectangle(2, 2, 9, 9);

        string FileName;

        Point OldPoint = new Point();
        Point CurrentPoint = new Point();

        Point OldDragPoint = new Point();
        Point CurrentDragPoint = new Point();

        Bitmap Sprite = new Bitmap(1, 1,PixelFormat.Format32bppArgb);
        Graphics Canvas;
        Pen MainPen = new Pen(Color.Black,1);
        Pen SecondPen = new Pen(Color.Black, 1);
        LineCap LineCap = System.Drawing.Drawing2D.LineCap.Round;
        DashCap DashCap = System.Drawing.Drawing2D.DashCap.Round;

        readonly List<RadioButton> ToolButtons = new List<RadioButton>();

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

            LoadImage(ImageOpened);

            InitCanvas();
        }

        private void InitCanvas()
        {
            ToolButtons.Add(BTN_Pen);
            ToolButtons.Add(BTN_Erase);
            ToolButtons.Add(BTN_ColorPick);
            ToolButtons.Add(BTN_Line);
            ToolButtons.Add(BTN_Select);
            ToolButtons.Add(BTN_Fill);
            ToolButtons.Add(BTN_Zoom);

            Canvas = PNL_Canvas.CreateGraphics();
            this.PNL_Canvas.MouseWheel += PNL_Canvas_MouseWheel;

            MainPen.SetLineCap(LineCap, LineCap, DashCap);
            SecondPen.SetLineCap(LineCap, LineCap, DashCap);

            ChangeColorMainPen(Color.Black);
            ChangeColorSecondPen(Color.White);

            CenterCanvas();

            //Activer le "Double buffer"
            //Éviter que le canevas clignote
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            PNL_Canvas, new object[] { true });

            

            FileChanged = false;
        }

        private Point GetCursorLocationRelative(MouseEventArgs e) { return new Point((int)((e.X / Zoom)), (int)((e.Y / Zoom))); }

        private void FRM_Main_Load(object sender, EventArgs e)
        {

        }

        private void PNL_Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.DrawImage(Sprite, new RectangleF(0,0, PNL_Canvas.Width,PNL_Canvas.Height),new RectangleF(-0.5f,-0.5f,Sprite.Width,Sprite.Height),GraphicsUnit.Pixel);
            DrawGrid(e.Graphics);
            DrawSelectionZone(e.Graphics);
        }

        private void PNL_Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            PNL_Canvas.Focus();
            Cursor.Current = Cursors.Cross;
            if (e.Button == MouseButtons.Left)
            {
                switch (CurrentTool)
                {
                    case Tool.Pen: DrawOnCanvas(MainPen, e, false); break;
                    case Tool.Eraser: DrawOnCanvas(MainPen, e, true); break;
                    case Tool.Select: DragSelectedZone(e); break;
                }
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    switch (CurrentTool)
                    {
                        case Tool.Pen: DrawOnCanvas(SecondPen, e, false); break;
                        case Tool.Eraser: DrawOnCanvas(SecondPen, e, true); break;
                        case Tool.Select: DisplayContextMenu(e); break;
                    }

                }
                else

                if (e.Button == MouseButtons.Middle)
                {
                    DragCanvas(PNL_Drag_Zone.PointToClient(Cursor.Position));
                }

                if (DraggingSelection)
                {
                    DraggingSelection = false;
                }
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

        private void NUM_Pen_Size_ValueChanged(object sender, EventArgs e) => ChangePenSize((float)NUM_Pen_Size.Value);

        private void BTN_MainColor_Click(object sender, EventArgs e)
        {
            if (DLG_Color.ShowDialog() == DialogResult.OK)
            {
                ChangeColorMainPen(DLG_Color.Color);
            }
        }

        private void BTN_SecondColor_Click(object sender, EventArgs e)
        {
            if (DLG_Color.ShowDialog() == DialogResult.OK)
            {
                ChangeColorSecondPen(DLG_Color.Color);
            }
        }

        private void BTN_Add_Color_Click(object sender, EventArgs e)
        {
            
        }

        private void SetTool()
        {
            for (int i = 0; i < ToolButtons.Count; i++)
            {
                if (ToolButtons[i].Checked)
                {
                    CurrentTool = Tool.Pen + i;
                    i = ToolButtons.Count;
                }
            }
            LBL_Debug.Text = CurrentTool.ToString();
        }

        private void BTN_Pen_CheckedChanged(object sender, EventArgs e) => SetTool();

        private void BTN_Erase_CheckedChanged(object sender, EventArgs e) => SetTool();

        private void BTN_Select_CheckedChanged(object sender, EventArgs e) => SetTool();

        private void PNL_Canvas_Click(object sender, EventArgs e) => Cursor.Current = Cursors.Cross;

        private void BTN_Save_Click(object sender, EventArgs e) => SaveFileAs();

        private void BTN_Load_Click(object sender, EventArgs e) => OpenFile();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        private void FRM_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FileChanged)
            {
                DialogResult dlg = PromptSave();
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

        private void BTN_Grid_CheckedChanged(object sender, EventArgs e) => SetGridVisible(BTN_Grid.Checked);

        private void BTN_ZoomIn_Click(object sender, EventArgs e) =>  ZoomIn();

        private void BTN_ZoomOut_Click(object sender, EventArgs e) => ZoomOut();

        private void BTN_Zoom1x_Click(object sender, EventArgs e) =>  Zoom1x();

        private void PNL_Canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                ZoomOut(e);
            else
                ZoomIn(e);
        }

        private void BTN_Upload_Click(object sender, EventArgs e)
        {
            FRM_SendImage send = new FRM_SendImage(Sprite);
            send.ShowDialog();
        }

        private void BTNTLS_Copy_Click(object sender, EventArgs e) => CopySelection();
        

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

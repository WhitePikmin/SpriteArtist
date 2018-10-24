using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteArtist
{
    public partial class DebugViewer : Form
    {
        public DebugViewer()
        {
            InitializeComponent();
        }

        public void UpdateValues(bool ActiveSelection, bool DraggingSelection, Rectangle SelectionZone)
        {
            //LBL_Values.Text =
              //  "ActiveSelection:   " + ActiveSelection.ToString() + "\n" +
              //  "DraggingSelection: " + DraggingSelection.ToString() + "\n" +
              //  "SelectionZone:     " + SelectionZone.ToString() + "\n";
        }

        public void UpdateValues(int NbPainter)
        {
            LBL_Values.Text =
                "NbPainters:     " + NbPainter.ToString(); 
        }
    }
}

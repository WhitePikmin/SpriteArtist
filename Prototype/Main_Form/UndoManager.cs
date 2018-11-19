using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpriteArtist
{
    public partial class FRM_Main
    {
        const int MAX_UNDO = 8;
        List<Bitmap> Timeline = new List<Bitmap>();
        List<Bitmap> TimelineSelection = new List<Bitmap>();
        int TimelinePointer = 0;
        int TimelineSelectionPointer = 0;

        public void UpdateTimeline()
        {
            if (ActiveSelection)
            {
                while (TimelineSelection.Count > TimelineSelectionPointer )
                    TimelineSelection.RemoveAt(TimelineSelection.Count - 1);

                if (TimelineSelection.Count >= MAX_UNDO)
                {
                    TimelineSelection.RemoveAt(0);
                    TimelineSelectionPointer--;
                }

                TimelineSelection.Add((Bitmap)Selection.Clone());
                TimelineSelectionPointer++;
            }
            else
            {
                while (Timeline.Count > TimelinePointer)
                    Timeline.RemoveAt(Timeline.Count - 1);

                if (Timeline.Count >= MAX_UNDO)
                {
                    Timeline.RemoveAt(0);
                    TimelinePointer--;
                }

                Timeline.Add((Bitmap)Sprite.Clone());
                TimelinePointer++;
                FileChanged = true;
            }
            UpdateUndoButton();
        }

        public void ClearTimelines()
        {
            Timeline = new List<Bitmap>();
            TimelineSelection = new List<Bitmap>();
            TimelinePointer = 0;
            TimelineSelectionPointer = 0;
            UpdateUndoButton();
        }

        public void ClearTimelineSelection()
        {
            TimelineSelection = new List<Bitmap>();
            TimelineSelectionPointer = 0;
            UpdateUndoButton();
        }

        public void Undo()
        {
            if (ActiveSelection)
            {
                if (TimelineSelectionPointer != 0)
                {
                    if(TimelineSelectionPointer == TimelineSelection.Count)
                    TimelineSelection.Add(Selection);

                    Selection = TimelineSelection[TimelineSelectionPointer-1];
                    TimelineSelectionPointer--;
                }
            }
            else
            {
                if (TimelinePointer != 0)
                {
                    if (TimelinePointer == Timeline.Count)
                        Timeline.Add((Bitmap)Sprite.Clone());

                    int UndoPointer = TimelinePointer - 1;

                    for (int i = 0; i < Sprite.Height; i++)
                    {
                        for (int j = 0; j < Sprite.Width; j++)
                        {
                            Sprite.SetPixel(j, i, Timeline[UndoPointer].GetPixel(j,i));
                        }
                    }

                    TimelinePointer--;
                }
            }
            PNL_Canvas.Invalidate();
            Update_Flow_Layout_Panel();
            UpdateUndoButton();
        }

        public void Redo()
        {
                if (ActiveSelection)
                {
                    if (TimelineSelectionPointer < TimelineSelection.Count - 1)
                    {
                        TimelineSelectionPointer++;
                        Selection = TimelineSelection[TimelineSelectionPointer];
                    }
                }
                else
                {
                    if (TimelinePointer < Timeline.Count - 1)
                    {
                        TimelinePointer++;
                        for (int i = 0; i < Sprite.Height; i++)
                        {
                            for (int j = 0; j < Sprite.Width; j++)
                            {
                                Sprite.SetPixel(j, i, Timeline[TimelinePointer].GetPixel(j, i));
                            }
                        }
                    }
                }
                PNL_Canvas.Invalidate();
            Update_Flow_Layout_Panel();
            UpdateUndoButton();
        }

        public void UpdateUndoButton()
        {
            if (ActiveSelection)
            {
                BTN_Undo.Enabled = (TimelineSelectionPointer != 0);
                BTN_Redo.Enabled = (TimelineSelectionPointer < TimelineSelection.Count - 1);
            }
            else
            {
                BTN_Undo.Enabled = (TimelinePointer != 0);
                BTN_Redo.Enabled = (TimelinePointer < Timeline.Count - 1);
            }
        }
    }
}

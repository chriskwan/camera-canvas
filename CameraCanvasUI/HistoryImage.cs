using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing; //for Bitmap
using System.Diagnostics; //for Assert

namespace CameraCanvas
{
    public class HistoryImage
    {
        private int capacity;
        private Stack<Snapshot> undoBuffer;
        private Stack<Snapshot> redoBuffer;

        public HistoryImage(int capacity)
        {
            this.capacity = capacity;
            undoBuffer = new Stack<Snapshot>(capacity);
            redoBuffer = new Stack<Snapshot>(capacity);
        }

        public void takeSnap(Bitmap source, string tag, int width, int height)
        {
            this.undoBuffer.Push(new Snapshot(source.Clone() as Bitmap, tag, width, height));
            this.redoBuffer.Clear();
        }

        public Snapshot unDo(Bitmap frame_current, string tag, int width, int height)
        {
            Snapshot frame_before = this.undoBuffer.Pop();
            this.redoBuffer.Push(new Snapshot(frame_current.Clone() as Bitmap, tag, width, height));

            if (frame_before != null)
            {
                return (frame_before);
            }
            else
            {
                return null;
            }
        }

        public Snapshot reDo(Bitmap frame_current, string tag, int width, int height)
        {
            Snapshot frame_after = this.redoBuffer.Pop();
            this.undoBuffer.Push(new Snapshot(frame_current.Clone() as Bitmap, tag, width, height));

            if (frame_after != null)
            {
                return (frame_after);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Are there any more undone changes to redo?
        /// </summary>
        public bool HasRedos
        {
            get
            {
                return (redoBuffer.Count > 0);
            }
        }

        /// <summary>
        /// Are there any more changes to undo?
        /// </summary>
        public bool HasUndos
        {
            get
            {
                return (undoBuffer.Count > 0);
            }
        }


        public List<string> getHistoryTags()
        {
            List<string> histTags = new List<string>();
            foreach (Snapshot s in this.undoBuffer)
            {
                if (s != null)
                {
                    histTags.Add(s.tag);
                }
            }
            return histTags;
        }

        public List<string> getFutureTags()
        {
            List<string> futTags = new List<string>();
            foreach (Snapshot s in this.redoBuffer)
            {
                if (s != null)
                {
                    futTags.Add(s.tag);
                }
            }
            return futTags;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing; //for Bitmap

namespace CameraCanvas
{
    public class Snapshot
    {
        public Bitmap image;
        public int width;
        public int height;
        public string tag;
        public Snapshot(Bitmap snap, string tag, int width, int height)
        {
            this.image = snap;
            this.tag = tag;
            this.width = width;
            this.height = height;
        }
    }
}

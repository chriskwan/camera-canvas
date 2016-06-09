using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CameraCanvas
{
    // should implement clonable and enumerable interfaces
    public class Image : ICloneable
    {
        public Bitmap imageBitmap;
        private Bitmap tempBuffer;

        public ImageFormat fileFormat;

        /* related to undo */
        private HistoryImage undoRedoBuffer;
        private string currentTag = "current";
        private int maxUndo = 5;//10;

        /* display mode related */
        public bool selecting = false;
        public bool pasting = false;
        public bool rotInitialized = false;

        /* related to paste function */

        private Bitmap pasteBuffer;
        private Bitmap pasteBufferAlpha;
        public Point pastePosition;

        /* related to rotation */
        private Bitmap rotationBuffer;

        //public int colorDepth; // not used at the moment - should be implemented(default image depth used instead)
        private int imageWidth;
        private int imageHeight;

        public string imagePath;
        public string imageName;

        public bool loaded = false;

        public int imageX;
        public int imageY;
        public float zoom;

        private Rectangle selected;


        private bool effectChanged = false;

        public Image()
        {
            
            imageBitmap = Clipboard.GetImage() as Bitmap;
            this.fileFormat = ImageFormat.Png;
            tempBuffer = new Bitmap(imageBitmap.Width, imageBitmap.Height);
            updateDim();
            redrawTempBuf();
            zoom = 1.0f;
            imageX = 0;
            imageY = 0;
            selectedArea = new Rectangle(0, 0, 0, 0);
            imagePath = null;
            imageName = name;


            loaded = true;

        }

        public Image(int x, int y, string name)
        {
            imageBitmap = new Bitmap(x, y);
            this.fileFormat = ImageFormat.Png;
            tempBuffer = new Bitmap(imageBitmap.Width, imageBitmap.Height);
            updateDim();
            redrawTempBuf();
            zoom = 1.0f;
            imageX = 0;
            imageY = 0;
            selectedArea = new Rectangle(0, 0, 0, 0);
            imagePath = null;
            imageName = name;
            loaded = true;
        }

        public Image(string fileName)
        {
            LoadImage(fileName);
            tempBuffer = new Bitmap(imageBitmap.Width, imageBitmap.Height);
            redrawTempBuf();
            zoom = 1.0f;
            imageX = 0;
            imageY = 0;
            selectedArea = new Rectangle(0, 0, 0, 0);
            updateDim();
            loaded = true;
        }

        public object Clone()
        {
            Image cloneImage = new Image(this.width, this.height, "copy_of_" + this.name);
            Graphics g = Graphics.FromImage(cloneImage.imageBitmap);
            g.DrawImage(this.imageBitmap, new Rectangle(0, 0, this.width, this.height),
                        new Rectangle(0, 0, this.width, this.height), GraphicsUnit.Pixel);
            g.Dispose();
            return cloneImage;
        }


        public Bitmap getImage()
        {
            return imageBitmap;
        }

        // properties for image width and height
        // changing them should automatically resize the image
        public int width
        {
            get
            {
                return (int)(this.imageWidth);
            }
            set
            {
                this.imageWidth = value;
            }
        }

        public int height
        {
            get
            {
                return (int)(this.imageHeight);
            }
            set
            {
                this.imageHeight = value;
            }
        }
        public int zoomedwidth
        {
            get
            {
                return (int)(this.imageWidth * this.zoom);
            }
        }

        public int zoomedheight
        {
            get
            {
                return (int)(this.imageHeight * this.zoom);
            }
        }

        public string name
        {
            get
            {
                return this.imageName;
            }
            set
            {
                this.imageName = value;
            }
        }

        public bool LoadImage(string fileName)
        {

            // use exception to find error
            Bitmap temporary = System.Drawing.Image.FromFile(fileName) as Bitmap;
            imageBitmap = new Bitmap(temporary.Width, temporary.Height);

            Graphics g = Graphics.FromImage(imageBitmap);
            g.DrawImage(temporary, new Rectangle(0, 0, temporary.Width, temporary.Height));
            g.Dispose();
            temporary.Dispose();
            updateDim();
            loadFileInfo(fileName);

            return true;
        }

        public void loadFileInfo(string fileName)
        {
            System.IO.FileInfo imagefileinfo = new System.IO.FileInfo(fileName);
            imageName = imagefileinfo.Name;
            imagePath = imagefileinfo.DirectoryName + "\\";
            string ext = imagefileinfo.Extension.ToLower();
            if (ext.LastIndexOf(".jpeg") != -1 || ext.LastIndexOf(".jpg") != -1)
            {
                this.fileFormat = ImageFormat.Jpeg;
                //Console.WriteLine("jpg!");
            }
            else if (ext.LastIndexOf(".gif") != -1)
            {
                this.fileFormat = ImageFormat.Gif;
                //Console.WriteLine("gif!");
            }
            else if (ext.LastIndexOf(".bmp") != -1)
            {
                this.fileFormat = ImageFormat.Bmp;
                //Console.WriteLine("bmp!");
            }
            else if (ext.LastIndexOf(".png") != -1)
            {
                this.fileFormat = ImageFormat.Png;
                //Console.WriteLine("png!");
            }
            else
            {
                this.fileFormat = null;
            }
        }

        public bool SaveImage(string fileName, ImageFormat imgfmt, int quality)
        {
            if (imgfmt == ImageFormat.Jpeg) // for jpeg images, use the quality input as encoding parameter
            {
                ImageCodecInfo codecSelected = null;
                foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                {
                    if (codec.MimeType == "image/jpeg")
                        codecSelected = codec;
                }
                EncoderParameters selectedPara = new EncoderParameters();
                selectedPara.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);
                
                imageBitmap.Save(fileName, codecSelected, selectedPara);
            }
            else // for all else
            {
                Console.WriteLine("saving as not jpeg at {0}", fileName);

                try
                {
                    imageBitmap.Save(fileName, imgfmt);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error with saving");
                    Console.WriteLine(e.ToString());
                }
            }
            return true;
        }

        public bool SaveImage(int quality)
        {
            return this.SaveImage(this.imagePath + this.name, ImageFormat.Jpeg, quality);

        }

        public bool SaveImage()
        {
            return this.SaveImage(this.imagePath + this.name, this.fileFormat, 75);
        }

        private void updateDim()
        {
            imageWidth = imageBitmap.Width;
            imageHeight = imageBitmap.Height;
        }

        public void draw(Graphics g, int targetwidth, int targetheight)
        {
            //g.DrawImage(imageBitmap, 0, 0, viewPort, GraphicsUnit.Pixel);
            //g.DrawImage(imageBitmap, 0, 0, new Rectangle(0, 0, 300, 400), GraphicsUnit.Pixel);

            if (effectChanged)
            {
                redrawTempBuf();
                drawPasting();
                drawSelected();

                effectChanged = false;
            }

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(tempBuffer,
                        new Rectangle(0, 0, targetwidth, targetheight),
                        new Rectangle((int)(-this.imageX), (int)(-this.imageY), (int)(targetwidth / zoom), (int)(targetheight / zoom)), GraphicsUnit.Pixel);


        }

        public Rectangle selectedArea
        {
            get
            {
                return selected;
            }

            set
            {


                if (value.Width > 0 && value.Height > 0)
                {
                    selected = new Rectangle((int)(value.X), (int)(value.Y), (int)(value.Width), (int)(value.Height));
                }
                else if (value.Width < 0 && value.Height < 0)
                {
                    selected = new Rectangle((int)((value.X + value.Width)), (int)((value.Y + value.Height)), (int)(-value.Width), (int)(-value.Height));
                }
                else if (value.Width < 0)
                {
                    selected = new Rectangle((int)((value.X + value.Width)), (int)(value.Y), (int)(-value.Width), (int)(value.Height));

                }
                else if (value.Height < 0)
                {
                    selected = new Rectangle((int)(value.X), (int)((value.Y + value.Height)), (int)(value.Width), (int)(-value.Height));
                }

                selected.X = Math.Max(0, selected.X);
                selected.Y = Math.Max(0, selected.Y);
                selected.X = Math.Min(imageWidth, selected.X);
                selected.Y = Math.Min(imageHeight, selected.Y);

                selected.Width = Math.Min(imageWidth - selected.X, selected.Width);
                selected.Height = Math.Min(imageHeight - selected.Y, selected.Height);


            }

        }

        private void drawSelected()
        {

            if (selected.Width != 0 && selecting)
            {
                Graphics g = Graphics.FromImage(tempBuffer);

                //fill a rectangle to show selected area
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.LightBlue)), selected);

                //draw outline around select area
                Pen outlinePen = new Pen(Color.Black, 5.0f);
                outlinePen.DashStyle = DashStyle.DashDot;
                g.DrawRectangle(outlinePen, selected);

                outlinePen.Dispose();
                g.Dispose();
            }

        }

        public void copyImage()
        {
            //draw to a temporary image and copy it to clipboard
            // use dimen selectedRectangle_adjusted

            if (selected.Width != 0)
            {
                Bitmap temporary = new Bitmap(selected.Width, selected.Height);
                Graphics g = Graphics.FromImage(temporary);
                g.DrawImage(imageBitmap, new Rectangle(0, 0, selected.Width, selected.Height), selected, GraphicsUnit.Pixel);
                g.Dispose();


                Clipboard.SetImage(temporary);

                temporary.Dispose();
            }

            else
            {
                copyWholeImage();
            }

            //resetSelection();

        }

        public void copyWholeImage()
        {
            Bitmap temporary = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(temporary);
            g.DrawImage(imageBitmap, new Rectangle(0, 0, imageWidth, imageHeight), new Rectangle(0, 0, imageWidth, imageHeight), GraphicsUnit.Pixel);
            g.Dispose();


            Clipboard.SetImage(temporary);

            temporary.Dispose();
        }

        public void cropImage()
        {
            if (selected.Width != 0)
            {
                Bitmap temporary = new Bitmap(selected.Width, selected.Height);
                Graphics g = Graphics.FromImage(temporary);
                g.DrawImage(imageBitmap, new Rectangle(0, 0, selected.Width, selected.Height), selected, GraphicsUnit.Pixel);
                g.Dispose();
                this.imageBitmap.Dispose();
                this.imageBitmap = temporary;
                this.width = temporary.Width;
                this.height = temporary.Height;
                this.redrawTempBuf();
            }
        }

        public void pasteImage(int x, int y)
        {
            Console.WriteLine("do paste!");

            if (!pasting && System.Windows.Forms.Clipboard.ContainsImage())
            {
                pasteBuffer = (Bitmap)Clipboard.GetImage();

                //if (pasteBuffer == null)
                //{
                //    //pasteBuffer = new Bitmap(800, 800);
                //    return;
                //}

                pasteBufferAlpha = new Bitmap(pasteBuffer.Width, pasteBuffer.Height);

                for (int i = 0; i < pasteBuffer.Height; i++)
                    for (int j = 0; j < pasteBuffer.Width; j++)
                    {

                        pasteBufferAlpha.SetPixel(j, i, Color.FromArgb(150, pasteBuffer.GetPixel(j, i)));

                    }
                pastePosition = new Point(x, y);
                pasting = true;

            }
            else if (pasting && null != pasteBuffer)
            {
                Graphics g = Graphics.FromImage(imageBitmap);
                g.DrawImage(pasteBuffer, new Rectangle(x, y, pasteBuffer.Width, pasteBuffer.Height),
                            new Rectangle(0, 0, pasteBuffer.Width, pasteBuffer.Height), GraphicsUnit.Pixel);
                g.Dispose();
                Console.WriteLine(Clipboard.GetImage().Width + "pasted!");
                this.Invalidate();
                pasting = false;
            }
        }

        private void drawPasting()
        {
            if (pasting && pasteBufferAlpha != null)
            {

                Graphics g = Graphics.FromImage(tempBuffer);
                g.DrawImage(pasteBufferAlpha, new Rectangle(pastePosition.X, pastePosition.Y, pasteBuffer.Width, pasteBuffer.Height),
                            new Rectangle(0, 0, pasteBuffer.Width, pasteBuffer.Height), GraphicsUnit.Pixel);
                g.Dispose();
            }
        }


        public void clearImage()
        {
            Graphics g = Graphics.FromImage(imageBitmap);
            //            g.DrawRectangle(Pens.White, new Rectangle(0, 0, width, height));
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
            g.Dispose();

        }

        //public void drawLine(Color linecolor, float width, Point start, Point end)
        //{
        //    if (loaded == true)
        //    {
        //        Graphics g = Graphics.FromImage((System.Drawing.Image)imageBitmap);
        //        Pen newpen = new Pen(linecolor, width);

        //        g.DrawLine(newpen, start, end);
        //        //g.DrawBezier(
        //    }
        //}

        public void DrawText(string text)
        {
            if (loaded == true)
            {
                Graphics g = Graphics.FromImage((System.Drawing.Image)imageBitmap);
                
                //draw text to show in the box
                
                Font font = new Font(FontFamily.GenericSansSerif, 25);
                SizeF textSize = g.MeasureString(text, font, imageBitmap.Width);

                Size textSize2 = textSize.ToSize();
                textSize2.Width += 1;
                textSize2.Height += 1;

                Rectangle newRect = new Rectangle(0,
                    imageBitmap.Height - textSize2.Height, textSize2.Width, textSize2.Height);

                g.FillRectangle(Brushes.White, newRect);

                g.DrawString(text, font, Brushes.Black, newRect);
                
                //scroll image down so the caption is visible for feedback
                int buffer = 100;
                this.imageY = (-1 * newRect.Y) + buffer;
                
            }
        }

        /// <summary>
        /// Draw a shape with given color, thickness at start and end points.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="lineThickness"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="shape"></param>
        public void DrawShape(Color color, float lineThickness, Point startPoint, Point endPoint, CCMainForm.DrawingShape shape)
        {
            if (loaded == true)
            {
                Graphics g = Graphics.FromImage((System.Drawing.Image)imageBitmap);
                Pen pen = new Pen(color, lineThickness); //for un-filled shapes and line
                Brush brush = new SolidBrush(color); //for filled shapes
                Rectangle shapeAreaRect = new Rectangle(
                    Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), 
                    Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                
                switch (shape)
                {
                    case CCMainForm.DrawingShape.Circle:
                        g.DrawEllipse(pen, shapeAreaRect);
                        break;
                    case CCMainForm.DrawingShape.Fill:
                        //TODO
                        Console.WriteLine("Need to implement Fill");
                        //
                        break;
                    case CCMainForm.DrawingShape.FilledCircle:
                        g.FillEllipse(brush, shapeAreaRect);
                        break;
                    case CCMainForm.DrawingShape.FilledRectangle:
                        g.FillRectangle(brush, shapeAreaRect);
                        break;
                    case CCMainForm.DrawingShape.Line:
                        g.DrawLine(pen, startPoint, endPoint);
                        break;
                    case CCMainForm.DrawingShape.Rectangle:
                        g.DrawRectangle(pen, shapeAreaRect);
                        break;
                    default:
                        break;
                }//end switch
            }//end if
        }


        /// <summary>
        /// Draw Bezier curve: NOT YET IMPLEMENTED
        /// </summary>
        /// <param name="linecolor"></param>
        /// <param name="width"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="point3"></param>
        /// <param name="point4"></param>
        public void drawBezier(Color linecolor, float width, Point point1, Point point2, Point point3, Point point4)
        {
            if (loaded == true)
            {
                Graphics g = Graphics.FromImage((System.Drawing.Image)imageBitmap);
                Pen newpen = new Pen(linecolor, width);

                g.DrawBezier(newpen, point1, point2, point3, point4);
                //g.DrawBezier(
            }
        }


        public void rotate(float amount)
        {
            amount = amount % 360;
            float rad = (amount % 180) * 3.141592f / 180f;
            double halfpi = Math.PI / 2;
            int a = Math.Abs((int)(imageBitmap.Height / Math.Sin(halfpi) * Math.Sin(rad)));
            int b = Math.Abs((int)(imageBitmap.Width / Math.Sin(halfpi) * Math.Sin(halfpi - rad)));
            int c = Math.Abs((int)(imageBitmap.Width / Math.Sin(halfpi) * Math.Sin(rad)));
            int d = Math.Abs((int)(imageBitmap.Height / Math.Sin(halfpi) * Math.Sin(halfpi - rad)));



            Bitmap rotatedBuffer = new Bitmap(a + b, c + d);
            this.tempBuffer = new Bitmap(a + b, c + d);

            Graphics e = Graphics.FromImage(rotatedBuffer);
            e.Clear(SystemColors.ControlDark);

            e.Clear(Color.White);

            Matrix rotMat = new Matrix();

            rotMat.Translate(rotatedBuffer.Width / 2 - imageBitmap.Width / 2, rotatedBuffer.Height / 2 - imageBitmap.Height / 2);
            rotMat.Translate(imageBitmap.Width / 2, imageBitmap.Height / 2);
            rotMat.Rotate(amount);
            rotMat.Translate(-imageBitmap.Width / 2, -imageBitmap.Height / 2);
            e.Transform = rotMat;

            e.DrawImage(imageBitmap, new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height),
                        new Rectangle(0, 0, imageBitmap.Width, imageBitmap.Height), GraphicsUnit.Pixel);
            e.Dispose();

            imageBitmap = rotatedBuffer;
            rotInitialized = false;
            //this.Invalidate();
            updateDim();
            Console.WriteLine("rotation preview done!");

        }

        public Bitmap previewRotation(float amount)
        {
            if (!rotInitialized)
            {
                rotationBuffer = new Bitmap(320, imageHeight * 320 / imageWidth);
                Graphics g = Graphics.FromImage(rotationBuffer);
                g.DrawImage(imageBitmap, new Rectangle(0, 0, rotationBuffer.Width, rotationBuffer.Height), new Rectangle(0, 0, imageWidth, imageHeight), GraphicsUnit.Pixel);
                g.Dispose();
                rotInitialized = true;
            }


            amount = amount % 360;
            float rad = (amount % 180) * 3.141592f / 180f;
            double halfpi = Math.PI / 2;
            int a = Math.Abs((int)(rotationBuffer.Height / Math.Sin(halfpi) * Math.Sin(rad)));
            int b = Math.Abs((int)(rotationBuffer.Width / Math.Sin(halfpi) * Math.Sin(halfpi - rad)));
            int c = Math.Abs((int)(rotationBuffer.Width / Math.Sin(halfpi) * Math.Sin(rad)));
            int d = Math.Abs((int)(rotationBuffer.Height / Math.Sin(halfpi) * Math.Sin(halfpi - rad)));



            Bitmap rotatedBuffer = new Bitmap(a + b, c + d);

            Graphics e = Graphics.FromImage(rotatedBuffer);
            //e.Clear(SystemColors.ControlDark);

            //e.Clear(Color.White);

            Matrix rotMat = new Matrix();

            rotMat.Translate(rotatedBuffer.Width / 2 - rotationBuffer.Width / 2, rotatedBuffer.Height / 2 - rotationBuffer.Height / 2);
            rotMat.Translate(rotationBuffer.Width / 2, rotationBuffer.Height / 2);
            rotMat.Rotate(amount);
            rotMat.Translate(-rotationBuffer.Width / 2, -rotationBuffer.Height / 2);
            e.Transform = rotMat;

            e.DrawImage(rotationBuffer, new Rectangle(0, 0, rotationBuffer.Width, rotationBuffer.Height),
                        new Rectangle(0, 0, rotationBuffer.Width, rotationBuffer.Height), GraphicsUnit.Pixel);
            e.Dispose();

            Console.WriteLine("rotation preview done!");


            return rotatedBuffer;


        }

        public void Brighten(int nBrightness)
        {
            BrightenImage(nBrightness, this.imageBitmap);
        }

        public Bitmap BrightenPreview(int nBrightness)
        {
            Bitmap brightenBuffer = new Bitmap(320, imageHeight * 320 / imageWidth);
            Graphics g = Graphics.FromImage(brightenBuffer);
            g.DrawImage(imageBitmap, new Rectangle(0, 0, brightenBuffer.Width, brightenBuffer.Height), new Rectangle(0, 0, imageWidth, imageHeight), GraphicsUnit.Pixel);
            g.Dispose();

            BrightenImage(nBrightness, brightenBuffer);
            return brightenBuffer;
        }

        public void BrightenImage(int nBrightness, Bitmap b)
        {
            //Bitmap b = this.imageBitmap;
            BitmapData bmData
            = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                int nVal;
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = (int)(p[0] + nBrightness);
                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;
                        p[0] = (byte)nVal;
                        ++p;
                    }
                    p += nOffset;
                }
            }
            b.UnlockBits(bmData);
        }

        public void Contrast(int nCtr)
        {
            ContrastImage(nCtr, this.imageBitmap);
        }

        public Bitmap ContrastPreview(int nCtr)
        {
            int previewWidth = 320;
            Bitmap tempBmp = new Bitmap(previewWidth, this.imageHeight * previewWidth / this.imageWidth);
            Graphics g = Graphics.FromImage(tempBmp);
            g.DrawImage(this.imageBitmap, 
                new Rectangle(0, 0, tempBmp.Width, tempBmp.Height), 
                new Rectangle(0, 0, this.imageBitmap.Width, this.imageBitmap.Height),
                GraphicsUnit.Pixel
                );
            g.Dispose();
            ContrastImage(nCtr, tempBmp);
            return tempBmp;
        }

        public void ContrastImage(int nCtr, Bitmap b)
        {
            float ctrP = (float)(nCtr + 100) / 100.0f;
            //Bitmap b = this.imageBitmap;
            BitmapData bmData
            = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                //int nVal;
                float pVal;
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        pVal = (float)p[0] / 255f;
                        pVal -= 0.5f;
                        pVal *= ctrP;
                        pVal += 0.5f;
                        pVal *= 255f;
                        if (pVal < 0f) pVal = 0f;
                        if (pVal > 255f) pVal = 255f;
                        p[0] = (byte)pVal;
                        ++p;
                    }
                    p += nOffset;
                }
            }
            b.UnlockBits(bmData);
        }

        /// <summary>
        /// Invert the colors of the main image.
        /// </summary>
        public void InvertColors()
        {
            InvertColorsImage(this.imageBitmap);
        }

        /// <summary>
        /// Helper method to invert the colors of an image.
        /// </summary>
        /// <param name="img"></param>
        private void InvertColorsImage(Bitmap img)
        {
            //Ref: http://www.geekpedia.com/tutorial202_Using-the-ColorMatrix-in-Csharp.html
            //Ref: http://www.c-sharpcorner.com/UploadFile/mahesh/Transformations0512192005050129AM/Transformations05.aspx
            //Ref: http://www.bobpowell.net/negativeimage.htm
            ImageAttributes ia = new ImageAttributes();
            ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix
            (
                new float[][]
                {
                    new float[] {-1, 0, 0, 0, 0},   //R (negate)
                    new float[] {0, -1, 0, 0, 0},   //G (negate)
                    new float[] {0, 0, -1, 0, 0},   //B (negate)
                    new float[] {0, 0, 0, 1, 0},    //alpha (leave alone)
                    new float[] {1, 1, 1, 0, 1}     //w = translating RGBa
                }
            );
            ia.SetColorMatrix(cm);
            
            Graphics g = Graphics.FromImage(img);
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            g.Dispose();
        }

        /// <summary>
        /// Make the main image grayscale.
        /// </summary>
        public void Grayscale()
        {
            GrayscaleImage(this.imageBitmap);
        }

        /// <summary>
        /// Helper method to make an image grayscale.
        /// </summary>
        /// <param name="img"></param>
        private void GrayscaleImage(Bitmap img)
        {
            //Ref: http://www.geekpedia.com/tutorial202_Using-the-ColorMatrix-in-Csharp.html
            ImageAttributes ia = new ImageAttributes();
            ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix
            (
                new float[][]
                {
                    new float[] {0.30f, 0.30f, 0.30f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                }
            );
            ia.SetColorMatrix(cm);

            Graphics g = Graphics.FromImage(img);
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            g.Dispose();
        }




        public Point getPicturePoint(Point screenPoint)
        {
            Point picPoint = new Point((int)Math.Floor((screenPoint.X / zoom) - (this.imageX) + 0.5),
                                       (int)Math.Floor((screenPoint.Y / zoom) - (this.imageY) + 0.5));

            Point sp = getScreenPoint(picPoint);
            Graphics g = Graphics.FromImage(imageBitmap);
            //Console.WriteLine(sp.X + "," + sp.Y);
            g.Dispose();

            //if (picPoint.X < 0 || picPoint.X >= this.imageWidth || picPoint.Y < 0 || picPoint.Y >= this.imageHeight)
            //{
            //picPoint.X = Math.Min(imageWidth-1, Math.Max(0, picPoint.X));
            //picPoint.Y = Math.Min(imageHeight-1, Math.Max(0, picPoint.Y));
            return picPoint;
            //}
            //else
            //{
            //    return picPoint;
            //}
        }

        public Point getScreenPoint(Point picturePoint)
        {
            //Console.WriteLine(((picturePoint.X * zoom) + (this.imageX * zoom)) +","+ ((picturePoint.Y * zoom) + (this.imageY * zoom)));
            Point picPoint = new Point((int)((picturePoint.X + this.imageX - 0.5) * zoom),
                                       (int)((picturePoint.Y + this.imageY - 0.5) * zoom));
            //if (picPoint.X < 0 || picPoint.X >= this.imageWidth || picPoint.Y < 0 || picPoint.Y >= this.imageHeight)
            //{

            return picPoint;
            //}
            //else
            //{
            //    return picPoint;
            //}

        }

        private void redrawTempBuf()
        {
            if (this.tempBuffer.Width != this.width || this.tempBuffer.Height == this.height)
            {
                this.tempBuffer.Dispose();
                this.tempBuffer = new Bitmap(this.width, this.height);
            }
            Graphics e = Graphics.FromImage(tempBuffer);
            e.DrawImage(imageBitmap, new Rectangle(0, 0, imageWidth, imageHeight), new Rectangle(0, 0, imageWidth, imageHeight), GraphicsUnit.Pixel);
            e.Dispose();
        }

        /// <summary>
        /// Sets effect changed to true.  But what does that mean?
        /// </summary>
        public void Invalidate()
        {
            this.effectChanged = true;
        }

        public void resetSelection()
        {
            selected.Width = 0;
        }

        public void moveLeft(int amount)
        {
            this.imageX += amount;
        }

        public void moveRight(int amount)
        {
            this.imageX -= amount;
        }

        public void moveUp(int amount)
        {
            this.imageY += amount;
        }

        public void moveDown(int amount)
        {
            this.imageY -= amount;
        }

        public void takeSnap(string tag)
        {
            if (this.undoRedoBuffer == null && maxUndo > 0) this.undoRedoBuffer = new HistoryImage(maxUndo);
            this.undoRedoBuffer.takeSnap(this.imageBitmap, tag, this.width, this.height);
        }

        public int undoDepth
        {
            get
            {
                return this.maxUndo;
            }

            set
            {
                if (value >= 0)
                    this.maxUndo = value;
            }
        }

        /// <summary>
        /// Does the image have any more changes that can be undone?
        /// </summary>
        /// <returns></returns>
        public bool HasUndos()
        {
            return this.undoRedoBuffer.HasUndos;
        }

        /// <summary>
        /// Undo the last change made to the image.
        /// </summary>
        /// <returns></returns>
        public bool undo()
        {
            //TODO should undo recenter the image?

            //Cannot undo if:
            //1. undoRedoBuffer has not been initialized
            //2. no undo's are allowed
            //3. no changes have been made to the image
            if (null == undoRedoBuffer || 0 == maxUndo || false == undoRedoBuffer.HasUndos)
            {
                return false;
            }
            else
            {
                Console.WriteLine("Undo!");

                Snapshot frame_before = undoRedoBuffer.unDo(this.imageBitmap, currentTag, this.width, this.height);
                if (frame_before == null)
                {
                    return false;
                }

                this.imageBitmap = frame_before.image;
                this.currentTag = frame_before.tag;
                this.width = frame_before.width;
                this.height = frame_before.height;

                redrawTempBuf();
                this.Invalidate();
                printHistory();

                return true;
            }//end else
        }

        /// <summary>
        /// Does the image have any more undone changes that can be redone?
        /// </summary>
        /// <returns></returns>
        public bool HasRedos()
        {
            return this.undoRedoBuffer.HasRedos;
        }

        /// <summary>
        /// Redo the last change that you undid.
        /// </summary>
        /// <returns></returns>
        public bool redo()
        {
            //TODO delete
            //Cannot redo if:
            //1. undoRedoBuffer is not initialized
            //2. no undoes are allowed
            //3. no undone changes to redo
            if (null == undoRedoBuffer || 0 == maxUndo || false == undoRedoBuffer.HasRedos)
            {
                return false;
            }
            else
            {
                Snapshot frame_after = undoRedoBuffer.reDo(this.imageBitmap, currentTag, this.width, this.height);
                if (frame_after == null)
                {
                    return false;
                }

                this.imageBitmap = frame_after.image;
                this.currentTag = frame_after.tag;
                this.width = frame_after.width;
                this.height = frame_after.height;

                redrawTempBuf();
                this.Invalidate();
                printHistory();

                return true;
            }//end else
        }

        public void printHistory()
        {

            List<string> histags = undoRedoBuffer.getHistoryTags();
            for (int i = histags.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("[{0}]", histags[i]);
            }


            Console.WriteLine("[{0}] <- current", currentTag);


            List<string> futags = undoRedoBuffer.getFutureTags();
            for (int i = 0; i < futags.Count; i++)
            {
                Console.WriteLine("[{0}]", futags[i]);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util; 

//MARK: Check out the designer and its code to see the initialization of UI controls
namespace CameraCaptureWithHAARCascade
{
    public partial class CameraCapture : Form
    {
        //Capture obj to capture the image/video from the system camera as its frames
        private Capture capture;
         
        public CameraCapture()
        {
            InitializeComponent();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            /*In EMGUCV 3.0, Image is in the different directory--Emgu.CV.Mat
             * Also QueryFrame() output the matrix, then need to
             *  convert to the Image with the same generic types*/
            //Image<Bgr, Byte> ImageFrame =  capture.QueryFrame(); 
            Image<Bgr, Byte> ImageFrame = capture.QueryFrame().ToImage<Bgr, Byte>();
            CamImageBox.Image = ImageFrame;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void caption_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbCamIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set the camera number to the one selected via combo 
            int camnumber = 0;
            camnumber = int.Parse(cbCamIndex.Text);

            //Start the camera with the selected number 
            if (capture == null) {
                try
                {
                    capture = new Capture(camnumber);
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            //Start showing the stream from camera
            btnStart_Click(sender, e); btnStart.Enabled = true;   //enable the button for pause/resumw
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            #region if capture is not created, create it now
            if (capture == null)
            {
                try
                {
                    capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            #endregion

            else
            {
                //if the button is paused, then resume it 
                if (btnStart.Text == "Pause")
                {
                    btnStart.Text = "Resume";
                    Application.Idle -= ProcessFrame; //hold the capture 
                }
                else //otherwise, pause it 
                {
                    btnStart.Text = "Pause";
                    Application.Idle += ProcessFrame;
                }
            }
        }

       

        //safely take care of closing the application 
        private void ReleaseData()
        {
            if (capture != null)
            {
                capture.Dispose(); 
            }
        }
    }
}

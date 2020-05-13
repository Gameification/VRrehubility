using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Windows;
using System.Drawing;



public class Tutor : MonoBehaviour
{
    
    private KinectSensor kinectSensor = null;

    private ColorFrameReader colorFrameReader = null;

    // Start is called before the first frame update
    void Start()
    {
        //Bitmap colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);
        kinectSensor = KinectSensor.GetDefault();
        colorFrameReader = kinectSensor.ColorFrameSource.OpenReader();
        colorFrameReader.FrameArrived += Reader_ColorFrameArrived;
        FrameDescription colorFrameDescription = kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
        kinectSensor.Open();
    }

     private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                            //colorFrame.CopyConvertedFrameDataToIntPtr(, (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4), ColorImageFormat.Bgra);
                            //this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));
                            //this.colorBitmap.Unlock();
                    }
                }
            }
        }
    
    // Update is called once per frame
    void Update()
    {
        
    }

   
}

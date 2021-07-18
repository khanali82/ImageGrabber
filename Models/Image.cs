using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageGrabber.Models
{
    class Image
    {
        public int ID { get; set; }

        public string ImageURI { get; set; }

        public BitmapImage Bitmap { get; set; }
    }
}

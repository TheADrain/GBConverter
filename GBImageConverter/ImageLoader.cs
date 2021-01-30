using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GBImageConverter
{
    public static class ImageLoader
    {
        public static Bitmap LoadImage(string filePath)
        {
            Image img = null;
            try
            {
                img = Image.FromFile(filePath);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: Could not load image file {0}.", filePath);
            }

            if (img == null)
            {
                return null;
            }
            else
            {
                Bitmap bmp = new Bitmap(img);
                img.Dispose();// File remains locked until image is disposed

                return bmp;
            }
        }
    }
}

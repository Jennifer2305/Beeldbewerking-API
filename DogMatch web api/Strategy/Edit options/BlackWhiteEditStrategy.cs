using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using System;
using DogMatch_web_api.Service;

namespace DogMatch_web_api.Strategy
{
    public class BlackWhiteEditStrategy : IPictureEditStrategy
    {
        public Bitmap EditPicture(Bitmap picture, params object[] parameters)
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(7)){
                for (int x = 0; x < picture.Width; x++){
                    for (int y = 0; y < picture.Height; y++){
                        var pixel = picture.GetPixel(x, y);
                        int colorCode = (int)(Math.Round(((double)(pixel.R + pixel.G + pixel.B) / 3.0) / 255) * 255);
                        pixel = Color.FromArgb(colorCode, colorCode, colorCode);
                        picture.SetPixel(x, y, pixel);
                    }
                }
            }
            return picture;
        }
    }
}
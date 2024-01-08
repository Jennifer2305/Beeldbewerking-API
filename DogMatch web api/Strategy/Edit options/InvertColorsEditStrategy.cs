using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using System;
using DogMatch_web_api.Service;

namespace DogMatch_web_api.Strategy
{
    public class InvertColorsEditStrategy : IPictureEditStrategy
    {
        public Bitmap EditPicture(Bitmap picture, params object[] parameters)
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(7)){
                for (int x = 0; x < picture.Width; x++){
                    for (int y = 0; y < picture.Height; y++){
                        var pixel = picture.GetPixel(x, y);
                        pixel = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                        picture.SetPixel(x, y, pixel);
                    }
                }
            }
            return picture;
        }
    }
}
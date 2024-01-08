using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using System;
using DogMatch_web_api.Service;

namespace DogMatch_web_api.Strategy
{
    public class GrayscaleEditStrategy : IPictureEditStrategy
    {
        public Bitmap EditPicture(Bitmap picture, params object[] parameters)
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(7))
            {
                for (int x = 0; x < picture.Width; x++)
                {
                    for (int y = 0; y < picture.Height; y++)
                    {
                        var pixel = picture.GetPixel(x, y);
                        int a = pixel.A;
                        int r = pixel.R;
                        int g = pixel.G;
                        int b = pixel.B;

                        int avg = (r + b + g) / 3;
                        var newPixel = Color.FromArgb(a, avg, avg, avg);

                        picture.SetPixel(x, y, newPixel);
                    }
                }
            }
            return picture;
        }
    }
}
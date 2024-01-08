using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using System;
using DogMatch_web_api.Service;

namespace DogMatch_web_api.Strategy
{
    public class LightEditStrategy : IPictureEditStrategy
    {
        public Bitmap EditPicture(Bitmap picture, params object[] parameters)
        {
            if (parameters.Length > 0){
                double amount = (double)parameters[0];
                if (OperatingSystem.IsWindowsVersionAtLeast(7)){
                    for (int x = 0; x < picture.Width; x++){
                        for (int y = 0; y < picture.Height; y++){
                            var pixel = picture.GetPixel(x, y);
                            var newPixel = Color.FromArgb(pixel.A,(int)Math.Min(255, pixel.R + 255 * amount), (int)Math.Min(255, pixel.G + 255 * amount), (int)Math.Min(255, pixel.B + 255 * amount));
                            picture.SetPixel(x, y, newPixel);
                        }
                    }
                }
            }
            return picture;
        }
    }
}
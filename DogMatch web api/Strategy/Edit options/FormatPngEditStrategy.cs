using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using System;
using DogMatch_web_api.Service;

namespace DogMatch_web_api.Strategy
{
    public class FormatPngEditStrategy : IPictureEditStrategy
    {
        public Bitmap EditPicture(Bitmap picture, params object[] parameters)
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(7)){
                if (parameters.Length > 0){
                    string saveLocation = (string)parameters[0];
                    long dogId = (long)parameters[1];
                    Image image = picture;
                    if (!Directory.Exists(saveLocation)){
                            Directory.CreateDirectory(saveLocation);
                    }
                    var editedPictureSaveLocation = Path.Combine(saveLocation, dogId + ".png");
                    image.Save(editedPictureSaveLocation, ImageFormat.Png);
                    return (Bitmap)image;
                }
            }
            return picture;
        }
    }
}
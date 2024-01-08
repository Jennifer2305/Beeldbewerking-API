using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using System;
using DogMatch_web_api.Service;

namespace DogMatch_web_api.Strategy
{
    public class SizeEditStrategy : IPictureEditStrategy
    {
        public Bitmap EditPicture(Bitmap picture, params object[] parameters)
        {
            if (parameters.Length > 0){
                int width = (int)parameters[0];
                int height = (int)parameters[1];
                if (OperatingSystem.IsWindowsVersionAtLeast(7)){
                    Bitmap newPicture = new Bitmap(picture,width, height);
                    return newPicture;
                }
            }
            return picture;
        }
    }
}
using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using System;

namespace DogMatch_web_api.Strategy
{
    public interface IPictureEditStrategy
    {
        Bitmap EditPicture(Bitmap picture, params object[] parameters);
    }     
}
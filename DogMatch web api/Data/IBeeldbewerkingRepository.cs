using Models;

namespace DogMatch_web_api.Repository
{
    public interface  IBeeldbewerkingRepository
    {
        Task downloadPictures(string picturePath);
        Task downloadFromUrl(string url, string fileName, string picturePath);
    }    
}
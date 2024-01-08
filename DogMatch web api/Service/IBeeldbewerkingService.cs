using System.Drawing;

namespace DogMatch_web_api.Service
{
    public interface IBeeldbewerkingService
    {
        bool dogExists(long id);
        string editPicture(long id, int[] options, int sizeWidth, int sizeHeight, double amountOfAdjustment);
        Byte[] getEditedPicture(string fileName);
        Task addPicturesToFolder();
        void deleteEditedPictures();
    }    
}
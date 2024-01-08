using System.Drawing;
using DogMatch_web_api.Repository;
using System.Drawing.Imaging;
using System.Net;
using DogMatch_web_api.Strategy;

namespace DogMatch_web_api.Service
{
    public class BeeldbewerkingService : IBeeldbewerkingService
    {
        //dependency injection repository en pictureEditing
        private readonly IBeeldbewerkingRepository _repository; 
        private IPictureEditStrategy _pictureEditing;
        public string editedPictureFolderPath = Path.Combine(Environment.CurrentDirectory, @"Edited pictures\");
        public string pictureFolderPath = Path.Combine(Environment.CurrentDirectory, @"Pictures\");
        
        public BeeldbewerkingService(IBeeldbewerkingRepository repository, IPictureEditStrategy pictureEditing){
            _repository = repository;
            _pictureEditing = pictureEditing;
        }

        //Check of hond id te vinden is tussen de profielfoto's
        public bool dogExists(long id)
        {
            string imageName = id.ToString() + ".jpg";
            string pathForImage = Path.Combine(pictureFolderPath, imageName);
            if (System.IO.File.Exists(pathForImage))
            {
                return true;
            }
            return false;
        }

        //Bewerk foto met de opgegeven filters en returnt de naam van de bewerkte foto
        public string editPicture(
            long id, 
            int[] options, 
            int sizeWidth, 
            int sizeHeight, 
            double amountOfAdjustment)
        {
            int count = options.Length;
            if (OperatingSystem.IsWindowsVersionAtLeast(7)){
                string pictureName = id.ToString() + ".jpg";
                string pictureLocation = Path.Combine(pictureFolderPath, pictureName);
                var dogPicture = new Bitmap(pictureLocation);
                string pictureSaveLocation =  Path.Combine(editedPictureFolderPath, pictureName);
                    for (int i = 0; i < count; i++){
                        //Door de options array loopen om te zien welke filters de gebruiker heeft toegepast
                        switch (options[i]){
                            case 1:
                                _pictureEditing = new GrayscaleEditStrategy();
                                dogPicture = _pictureEditing.EditPicture(dogPicture);
                                break;
                            case 2:
                                _pictureEditing = new BlackWhiteEditStrategy();
                                dogPicture = _pictureEditing.EditPicture(dogPicture);
                                break;
                            case 3:
                                _pictureEditing = new InvertColorsEditStrategy();
                                dogPicture = _pictureEditing.EditPicture(dogPicture);
                                break;
                            case 4:
                                _pictureEditing = new LightEditStrategy();
                                dogPicture = _pictureEditing.EditPicture(dogPicture, amountOfAdjustment);
                                break;
                            case 5:
                                _pictureEditing = new DarkEditStrategy();
                                dogPicture = _pictureEditing.EditPicture(dogPicture, amountOfAdjustment);
                                break;
                            case 6:
                                _pictureEditing = new SizeEditStrategy();
                                dogPicture = _pictureEditing.EditPicture(dogPicture, sizeWidth, sizeHeight);
                                break;
                            case 7:
                                _pictureEditing = new FormatPngEditStrategy();
                                dogPicture= _pictureEditing.EditPicture(dogPicture, editedPictureFolderPath, id);
                                pictureName = id.ToString() + ".png";
                                break;
                        }
                    }
                    if (pictureName.Substring(pictureName.Length - 3) == "jpg"){
                        if (!Directory.Exists(editedPictureFolderPath)){
                            Directory.CreateDirectory(editedPictureFolderPath);
                        }
                        dogPicture.Save(pictureSaveLocation);
                    }
                    return pictureName;                   
            }
            return "";                
        } 

        //Stuurt de foto terug nadat deze bewerkt en opgeslagen is
        public Byte[] getEditedPicture(string fileName){
            string editedPictureLocation = Path.Combine(editedPictureFolderPath, fileName);
            Byte[] editedImage = System.IO.File.ReadAllBytes(editedPictureLocation);
            return editedImage;
        }

        //Roept functie in repository class aan om hondenfoto's te downloaden
        public async Task addPicturesToFolder(){
            await _repository.downloadPictures(pictureFolderPath);
        }

        //Verwijdert alle bewerkte foto's die zijn opgeslagen
        public void deleteEditedPictures(){
            string[] files = Directory.GetFiles(editedPictureFolderPath);
            foreach (string file in files)
                File.Delete(file);
        }
    }
} 



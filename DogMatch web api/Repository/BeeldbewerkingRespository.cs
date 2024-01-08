using Models;

namespace DogMatch_web_api.Repository
{
    //Deze class bevat 2 functies waarmee de gebruikte hondenfoto's in DogMatch gedownload worden en lokaal opgeslagen worden
    public class BeeldbewerkingRepository : IBeeldbewerkingRepository
    {
        public async Task downloadPictures(string picturePath)
        {
            string url = "";
            using (var db = new DataContext()){
                var result = from m in db.hond select m;
                foreach (var entry in result){
                    if (entry.Foto != null){
                        url = entry.Foto;
                        string filename = entry.Id.ToString() + ".jpg";
                        await downloadFromUrl(url, filename, picturePath);
                    }
                }
            }
        }

        public async Task downloadFromUrl(string url, string fileName, string picturePath)
        {
            using (HttpClient httpClient = new HttpClient()){
                var result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode){
                    byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
                    string pictureLocation = Path.Combine(picturePath, fileName);
                    if (! System.IO.File.Exists(pictureLocation)){
                        System.IO.File.WriteAllBytes(pictureLocation, imageBytes);
                    }
                }           
            } 
        }
    }
}
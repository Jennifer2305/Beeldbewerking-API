using Models;

namespace DogMatch_web_api.Entities
{
    public class Afgewezen
    {
        public Hond? Hond1 {get;set;}
        public long Hond1Id {get;set;}
        public Hond? Hond2 {get;set;}
        public long Hond2Id {get;set;}
    }
}
using Models;

namespace DogMatch_web_api.Entities
{
    public class Hond_Profiel
    {
        public Hond? Hond {get;set;}
        public long HondId {get;set;}
        public Profiel? Profiel {get;set;}
        public long ProfielId {get;set;}
    }
}
using Models;

namespace DogMatch_web_api.Entities
{
    public class Profiel
    {
        public long Id {get;set;}
        public string? Beschrijving {get;set;}
        public string? Voorkeur {get;set;}
        public virtual Hond_Profiel? Hond {get;set;}
    }  
}
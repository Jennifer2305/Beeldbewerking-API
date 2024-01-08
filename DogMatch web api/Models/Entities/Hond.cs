using Models;

namespace DogMatch_web_api.Entities
{
    public class Hond
    {
        public long Id { get; set; }
        public string? Naam { get; set; }
        public string? Geslacht { get; set; }
        public DateTime Geboortedatum {get;set;}
        public string? Foto {get;set;}
        public virtual List<Match>? MatchedHond1 {get;set;}
        public virtual List<Match>? MatchedHond2 {get;set;}
        public virtual List<Afgewezen>? AfgewezenHond1 {get;set;}
        public virtual List<Afgewezen>? AfgewezenHond2 {get;set;}
        public virtual Hond_Profiel? Profiel {get;set;}

    }
}
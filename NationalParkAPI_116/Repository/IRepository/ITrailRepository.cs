using NationalParkAPI_116.Models;

namespace NationalParkAPI_116.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();
        Trail GetTrail(int trailId);
        ICollection<Trail> GetTrailsInNationalPark(int nationalParkId);
        bool TrailExists(int trailId);
        bool TrailExists(string trailName);
        bool CreateTrail(Trail trail);  
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();

    }
}

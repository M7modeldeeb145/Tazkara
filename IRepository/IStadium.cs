using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface IStadium
    {
        void Create(Stadium stadium);
        void Update(Stadium stadium);
        void Delete(int id);
        List<Stadium> GetAll();
        Stadium GetById(int id);
        List<Team> GetAllTeams();
        List<NorthPremiumStands> GetAllNorthPremiumStands();
        List<EastPremiumStands> GetAllEastPremiumStands();
        List<EastStands> GetAllEastStands();
        List<CourtSidesRow3> GetAllCourtSidesRow3();
    }
}

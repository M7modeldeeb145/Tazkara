using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface ILeague
    {
        void Create(League league);
        void Update(League league);
        void Delete(int id);
        List<League> GetAll();
        League GetById(int id);
        List<Match> GetMatchesInLeague(int id);
        List<Stadium> GetStadiums();
    }
}

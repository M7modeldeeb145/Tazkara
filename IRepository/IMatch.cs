
using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface IMatch
    {
        List<Match> GetAll();
        List<Team> GetTeams();
        List<Stadium> GetStadiums();
        List<League> GetLeagues();
        List<Match> Search(string name);
        Match GetById(int id);
        void Create(Match match);
        void Update(Match match);
        void Delete(int id);
    }
}

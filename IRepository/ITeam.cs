using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface ITeam
    {
        void Create(Team team);
        void Update(Team team);
        void Delete(int id);
        List<Team> GetAll();
        Team GetById(int id);
        
    }
}

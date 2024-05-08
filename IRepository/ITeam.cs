using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface ITeam
    {
        void Create(Team team);
        void Update(Team team);
        void Delete(Team team);
        List<Team> GetAll();
        Team GetById(int id);
    }
}

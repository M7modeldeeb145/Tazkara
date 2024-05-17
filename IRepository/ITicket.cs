using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface ITicket
    {
        void Create(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(int id);
        List<Ticket> GetAll();
        Ticket GetById(int id);
        Match GetMatch(int id);
        List<Match> GetMatches();
        List<Stadium> GetStadiums();
    }
}

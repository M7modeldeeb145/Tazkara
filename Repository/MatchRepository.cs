using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class MatchRepository : IMatch
    {
        ApplicationDbContext context;
        public MatchRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Match match)
        {
            context.Matchs.Add(match);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var delete = context.Matchs.Find(id);
            if (delete != null)
            {
                context.Matchs.Remove(delete);
                context.SaveChanges();
            }
        }

        public List<Match> GetAll()
        {
            return context.Matchs.Include(e=>e.Stadium).Include(e=>e.League).Include(e=>e.Teams).ToList();
        }

        public Match GetById(int id)
        {
            var match = context.Matchs.Find(id);
            if (match != null)
            {
                return match;
            }
            return null;
            
        }

        public List<League> GetLeagues()
        {
            throw new NotImplementedException();
        }

        public List<Stadium> GetStadiums()
        {
            throw new NotImplementedException();
        }

        public List<Team> GetTeams()
        {
            return context.Teams.ToList();
        }

        public List<Match> Search(string name)
        {
            return context.Matchs.Include(e=>e.LeagueId).Include(e=>e.Stadium).Include(e=>e.Teams).Include(e=>e.Tickets).Where(e=>e.Name.Contains(name)).ToList();
        }

        public void Update(Match match)
        {
            var edit = context.Matchs.Find(match.Id);
            if (edit != null)
            {
                edit.StartDate = match.StartDate;
                edit.EndDate = match.EndDate;
                edit.Name = match.Name;
                //edit.TicketId = match.TicketId;
                edit.LeagueId = match.LeagueId;
                //edit.TeamId = match.TeamId;
                context.SaveChanges();
            }
        }

    }
}

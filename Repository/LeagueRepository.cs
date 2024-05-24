using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;
using Tazkara.Models;

namespace Tazkara.Repository
{
    public class LeagueRepository : ILeague
    {
        ApplicationDbContext context;
        public LeagueRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(League league)
        {
            context.Leagues.Add(league);
            context.SaveChanges();  
        }

        public void Delete(int id)
        {
            var league = context.Leagues.Find(id);
            if (league != null)
            {
                context.Remove(league);
                context.SaveChanges();
            }
        }

        public List<League> GetAll()
        {
            return context.Leagues.ToList();
        }
        public League GetById(int id)
        {
            return context.Leagues.Find(id);
        }

        public List<Match> GetMatchesInLeague(int id)
        {
            var result = context.Matchs.Include(e => e.League).Include(e=>e.Stadium).Where(e=>e.LeagueId==id).ToList();
            return result;
        }

        public List<Stadium> GetStadiums()
        {
            return context.Stadiums.ToList();
        }

        public void Update(League league)
        {
            context.Leagues.Update(league);
            context.SaveChanges();
        }
    }
}

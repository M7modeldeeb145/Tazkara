using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class TeamRepository : ITeam
    {
        ApplicationDbContext context;
        public TeamRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Team team)
        {
            context.Teams.Add(team);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var delete = context.Teams.Find(id);
            if (delete != null)
            {
                context.Teams.Remove(delete);
                context.SaveChanges();
            }
        }

        public List<Team> GetAll()
        {
            return context.Teams.ToList();
        }

        public Team GetById(int id)
        {
            return context.Teams.Find(id);
        }
        public void Update(Team team)
        {
            context.Teams.Update(team); 
            context.SaveChanges();
        }
    }
}

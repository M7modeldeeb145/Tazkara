using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class StadiumRepository : IStadium
    {
        ApplicationDbContext context;
        public StadiumRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Stadium stadium)
        {
            context.Stadiums.Add(stadium);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var delete = context.Stadiums.Find(id);
            if (delete != null)
            {
                context.Stadiums.Remove(delete);
                context.SaveChanges();
            }
        }

        public List<Stadium> GetAll()
        {
            return context.Stadiums.ToList();
        }

        public Stadium GetById(int id)
        {
            return context.Stadiums.Find(id);
        }

        public void Update(Stadium stadium)
        {
            var edit = context.Stadiums.Find(stadium.Id);
            if (edit != null)
            {
                edit.Address = stadium.Address;
                edit.Capacity = stadium.Capacity;
                edit.Name = stadium.Name;   
                edit.MatchId = stadium.MatchId;
                edit.TeamId = stadium.TeamId;
                context.SaveChanges();
            }
        }
    }
}

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

        public List<CourtSidesRow3> GetAllCourtSidesRow3()
        {
            return context.CourtSidesRow3.ToList();
        }

        public List<EastPremiumStands> GetAllEastPremiumStands()
        {
            return context.EastPremiumStands.ToList();
        }

        public List<EastStands> GetAllEastStands()
        {
            return context.EastStands.ToList();
        }

        public List<NorthPremiumStands> GetAllNorthPremiumStands()
        {
            return context.NorthPremiumStands.ToList();
        }

        public List<Team> GetAllTeams()
        {
            return context.Teams.ToList();
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
                edit.TotalCapacity = stadium.TotalCapacity;
                edit.Name = stadium.Name;
                edit.TeamId = stadium.TeamId;
                edit.StadiumStatus = stadium.StadiumStatus;
                edit.CourtSidesRow3Id = stadium.CourtSidesRow3Id;
                edit.EastPremiumStandsId = stadium.EastPremiumStandsId;
                edit.NorthPremiumStandsId= stadium.NorthPremiumStandsId;
                edit.EastStandsId = stadium.EastStandsId;   
                context.SaveChanges();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;

namespace Tazkara.Repository
{
    public class CourtSidesRow3Repository
    {
        ApplicationDbContext context;
        public CourtSidesRow3Repository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(CourtSidesRow3 CourtSidesRow3)
        {
            context.CourtSidesRow3.Add(CourtSidesRow3);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var CourtSidesRow3 = context.CourtSidesRow3.Find(id);
            if (CourtSidesRow3 != null)
            {
                context.Remove(CourtSidesRow3);
                context.SaveChanges();
            }
        }

        public List<CourtSidesRow3> GetAll()
        {
            return context.CourtSidesRow3.ToList();
        }

        public List<Stadium> GetAllStadiums()
        {
            return context.Stadiums.ToList();
        }

        public CourtSidesRow3 GetById(int id)
        {
            return context.CourtSidesRow3.Find(id);
        }

        public void Update(CourtSidesRow3 CourtSidesRow3)
        {
            var edit = context.CourtSidesRow3.Find(CourtSidesRow3.Id);
            if (edit != null)
            {
                edit.Cost = CourtSidesRow3.Cost;
                edit.Name = CourtSidesRow3.Name;
                edit.Capacity = CourtSidesRow3.Capacity;
                context.SaveChanges();
            }
        }
    }
}

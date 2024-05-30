using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class EastStandsRepository : IEastStands
    {
        ApplicationDbContext context;
        public EastStandsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(EastStands EastStands)
        {
            context.EastStands.Add(EastStands);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var EastStands = context.EastStands.Find(id);
            if (EastStands != null)
            {
                context.Remove(EastStands);
                context.SaveChanges();
            }
        }

        public List<EastStands> GetAll()
        {
            return context.EastStands.ToList();
        }

		public List<Stadium> GetAllStadiums()
		{
			return context.Stadiums.ToList();
		}

		public EastStands GetById(int id)
        {
            return context.EastStands.Find(id);
        }

        public void Update(EastStands EastStands)
        {
            var edit = context.EastStands.Find(EastStands.Id);
            if (edit != null)
            {
                edit.Cost = EastStands.Cost;
                edit.Name = EastStands.Name;
                edit.Capacity = EastStands.Capacity;
                context.SaveChanges();
            }
        }
    }
}

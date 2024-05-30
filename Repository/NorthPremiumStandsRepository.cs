using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;

namespace Tazkara.Repository
{
	public class NorthPremiumStandsRepository
	{
		ApplicationDbContext context;
		public NorthPremiumStandsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}
		public void Create(NorthPremiumStands NorthPremiumStands)
		{
			context.NorthPremiumStands.Add(NorthPremiumStands);
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			var NorthPremiumStands = context.NorthPremiumStands.Find(id);
			if (NorthPremiumStands != null)
			{
				context.Remove(NorthPremiumStands);
				context.SaveChanges();
			}
		}

		public List<NorthPremiumStands> GetAll()
		{
			return context.NorthPremiumStands.ToList();
		}

		public List<Stadium> GetAllStadiums()
		{
			return context.Stadiums.ToList();
		}

		public NorthPremiumStands GetById(int id)
		{
			return context.NorthPremiumStands.Find(id);
		}

		public void Update(NorthPremiumStands NorthPremiumStands)
		{
			var edit = context.NorthPremiumStands.Find(NorthPremiumStands.Id);
			if (edit != null)
			{
				edit.Cost = NorthPremiumStands.Cost;
				edit.Name = NorthPremiumStands.Name;
				edit.Capacity = NorthPremiumStands.Capacity;
				context.SaveChanges();
			}
		}
	}
}

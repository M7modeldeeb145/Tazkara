using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;

namespace Tazkara.Repository
{
	public class EastPremiumStandsRepository
	{
		ApplicationDbContext context;
		public EastPremiumStandsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}
		public void Create(EastPremiumStands EastPremiumStands)
		{
			context.EastPremiumStands.Add(EastPremiumStands);
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			var EastPremiumStands = context.EastPremiumStands.Find(id);
			if (EastPremiumStands != null)
			{
				context.Remove(EastPremiumStands);
				context.SaveChanges();
			}
		}

		public List<EastPremiumStands> GetAll()
		{
			return context.EastPremiumStands.ToList();
		}

		public List<Stadium> GetAllStadiums()
		{
			return context.Stadiums.ToList();
		}

		public EastPremiumStands GetById(int id)
		{
			return context.EastPremiumStands.Find(id);
		}

		public void Update(EastPremiumStands EastPremiumStands)
		{
			var edit = context.EastPremiumStands.Find(EastPremiumStands.Id);
			if (edit != null)
			{
				edit.Cost = EastPremiumStands.Cost;
				edit.Name = EastPremiumStands.Name;
				edit.Capacity = EastPremiumStands.Capacity;
				context.SaveChanges();
			}
		}
	}
}

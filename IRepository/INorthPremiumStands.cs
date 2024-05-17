using Tazaker.Models;

namespace Tazkara.IRepository
{
	public interface INorthPremiumStands
	{
		void Create(NorthPremiumStands NorthPremiumStands);
		void Update(NorthPremiumStands NorthPremiumStands);
		void Delete(int id);
		List<NorthPremiumStands> GetAll();
		NorthPremiumStands GetById(int id);
		List<Stadium> GetAllStadiums();
	}
}

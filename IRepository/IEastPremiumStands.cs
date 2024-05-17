using Tazaker.Models;

namespace Tazkara.IRepository
{
	public interface IEastPremiumStands
	{
		void Create(EastPremiumStands EastPremiumStands);
		void Update(EastPremiumStands EastPremiumStands);
		void Delete(int id);
		List<EastPremiumStands> GetAll();
		EastPremiumStands GetById(int id);
		List<Stadium> GetAllStadiums();
	}
}

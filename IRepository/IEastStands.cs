using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface IEastStands
    {
        void Create(EastStands eastStands);
        void Update(EastStands eastStands);
        void Delete(int id);
        List<EastStands> GetAll();
        EastStands GetById(int id);
        List<Stadium> GetAllStadiums();
    }
}

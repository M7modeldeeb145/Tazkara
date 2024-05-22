using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface ICourtSidesRow3
    {
        void Create(CourtSidesRow3 CourtSidesRow3);
        void Update(CourtSidesRow3 CourtSidesRow3);
        void Delete(int id);
        List<CourtSidesRow3> GetAll();
        CourtSidesRow3 GetById(int id);
        List<Stadium> GetAllStadiums();
    }
}

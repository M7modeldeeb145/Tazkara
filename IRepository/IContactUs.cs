using Tazaker.Models;

namespace Tazkara.IRepository
{
    public interface IContactUs
    {
        List<ContactUs> ReadAll();
        ContactUs GetById(int id);
        void Delete(int id);
        void Update(ContactUs contactUs);
        void Create(ContactUs contactUs);
    }
}

using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class ContactUsRepository : IContactUs
    {
        ApplicationDbContext context;
        public ContactUsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(ContactUs contactUs)
        {
            context.ContactUs.Add(contactUs);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var contactus = context.ContactUs.Find(id);
            if (contactus != null)
            {
                context.ContactUs.Remove(contactus);
                context.SaveChanges();
            }
        }

        public ContactUs GetById(int id)
        {
            return context.ContactUs.Find(id);
        }

        public List<ContactUs> ReadAll()
        {
            return context.ContactUs.ToList();
        }

        public void Update(ContactUs contactUs)
        {
            var edit = context.ContactUs.Find(contactUs.Id);
            if (edit != null)
            {
                edit.Email = contactUs.Email;
                edit.Subject = contactUs.Subject;
                edit.Message = contactUs.Message;
                edit.Status = contactUs.Status;
                context.SaveChanges();
            }
        }
    }
}

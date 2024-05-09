using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class TicketRepository : ITicket
    {
        ApplicationDbContext context;
        public TicketRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Ticket Ticket)
        {
            context.Tickets.Add(Ticket);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Ticket = context.Tickets.Find(id);
            if (Ticket != null)
            {
                context.Remove(Ticket);
                context.SaveChanges();
            }
        }

        public List<Ticket> GetAll()
        {
            return context.Tickets.ToList();
        }

        public Ticket GetById(int id)
        {
            return context.Tickets.Find(id);
        }

        public void Update(Ticket Ticket)
        {
            var edit = context.Tickets.Find(Ticket.Id);
            if (edit != null)
            {
                edit.Title = Ticket.Title;
                edit.StadiumId = Ticket.StadiumId;
                edit.ReferenceNum = Ticket.ReferenceNum;
                edit.MatchId = Ticket.MatchId;
                context.SaveChanges();
            }
        }
    }
}

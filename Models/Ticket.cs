using Tazkara.Models;

namespace Tazaker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid ReferenceNum { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int StadiumId { get; set; }
        public Stadium Stadium { get; set; }
        public List<ApplicationUser> Users { get; set; }
        
    }
}

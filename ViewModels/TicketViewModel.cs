using System.ComponentModel.DataAnnotations;

namespace Tazkara.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } 
        public Guid ReferenceNum { get; set; }
        [Required]
        public int MatchId { get; set; }
        [Required]
        public int StadiumId { get; set; }
    }
}

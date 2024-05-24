using System.ComponentModel.DataAnnotations;

namespace Tazkara.ViewModels
{
    public class LeagueViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LeagueLogo { get; set; }
    }
        
}

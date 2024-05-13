using System.ComponentModel.DataAnnotations;

namespace Tazkara.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TeamLogo { get; set; }
    }
}


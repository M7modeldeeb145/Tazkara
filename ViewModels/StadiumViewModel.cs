using System.ComponentModel.DataAnnotations;
using Tazaker.Models;

namespace Tazkara.ViewModels
{
    public class StadiumViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TotalCapacity { get; set; }
        [Required]
        public string Address { get; set; } 
        public StadiumStatus StadiumStatus { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int EastStandsId { get; set; }
        [Required]
        public int EastPremiumStandsId { get; set; }
        [Required]
        public int NorthPremiumStandsId { get; set; }
        [Required]
        public int CourtSidesRow3Id { get; set; }
    }
}

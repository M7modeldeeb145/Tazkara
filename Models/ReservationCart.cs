using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tazaker.Models;

namespace Tazkara.Models
{
    public class ReservationCart
    {
        public int Id { get; set; }
        [ForeignKey("MatchId")]
        [ValidateNever]
        public Match Match { get; set; }
        public int MatchId { get; set; }
        [Range(0, 4 , ErrorMessage ="The Max Num of Tickets for One User is 4") ]
        public int Count { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public int StadiumId { get; set; }
        [ValidateNever]
        [ForeignKey("StadiumId")]
        public Stadium Stadium { get; set; }
        public string Title { get; set; } = null!;
        public Guid ReferenceNum { get; set; }
        [NotMapped]
        public double Price { get; set; }
        public int EastPremiumStandsId { get; set; }
        [ValidateNever]
        [ForeignKey("EastPremiumStandsId")]
        public EastPremiumStands EastPremiumStands { get; set; }
        public int NorthPremiumStandsId { get; set; }
        [ValidateNever]
        [ForeignKey("NorthPremiumStandsId")]
        public NorthPremiumStands NorthPremiumStands { get; set; }
        public int EastStandsId { get; set; }
        [ValidateNever]
        [ForeignKey("EastStandsId")]
        public EastStands EastStands { get; set; }
        public int CourtSidesRow3Id { get; set; }
        [ValidateNever]
        [ForeignKey("CourtSidesRow3Id")]
        public CourtSidesRow3 CourtSidesRow3 { get; set; }
    }
}

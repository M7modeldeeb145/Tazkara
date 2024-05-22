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
        [Range(0, 10 , ErrorMessage ="The Max Num of Tickets for One User is 10") ]
        public int Count { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace Tazkara.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public int LeagueId { get; set; }
        [Required]
        public int StadiumId { get; set; }
    }
}

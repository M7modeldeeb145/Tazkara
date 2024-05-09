﻿namespace Tazkara.ViewModels
{
    public class LeagueViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

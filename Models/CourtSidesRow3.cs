﻿namespace Tazaker.Models
{
    public class CourtSidesRow3
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public double Cost { get; set; }
        public int StadiumId { get; set; }
        public Stadium Stadium { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace Tazkara.ViewModels
{
    public class EastStandsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public int StadiumId { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Screening
{
    public class Screening
    {
        public int Id { get; set; } 
        public int screenNumber { get; set; }
        public int capacity { get; set; }
        public DateTime startsAt { get; set; }
        
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

    }
}

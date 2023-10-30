﻿using System.ComponentModel.DataAnnotations;

namespace api_cinema_challenge.Models.Customer
{    
    public class Customer 
    {
        public int Id { get; set; } 
        public string name { get; set; }    
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}

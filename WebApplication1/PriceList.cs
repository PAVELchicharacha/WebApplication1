﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class PriceList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
    }
}

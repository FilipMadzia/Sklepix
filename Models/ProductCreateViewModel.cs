﻿using Sklepix.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string? Name { get; set; }
        [Required]
        [DisplayName("Cena")]
        public decimal Price { get; set; }
        public List<CategoryEntity>? Categories { get; set; }
        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }
        public List<ShelfEntity>? Shelves { get; set; }
        [DisplayName("Półka")]
        public int ShelfId { get; set; }
        public List<AisleEntity>? Aisles { get; set; }
    }
}

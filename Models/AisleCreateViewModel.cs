﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sklepix.Models
{
	public class AisleCreateViewModel
	{
		public int Id { get; set; }
		[Required]
		[DisplayName("Nazwa")]
		public string? Name { get; set; }
	}
}

﻿using System.ComponentModel;

namespace Sklepix.Models.Shelves
{
	public class ShelfDetailsViewModel
	{
		public int Id { get; set; }
		[DisplayName("Numer")]
		public int Number { get; set; }
		[DisplayName("Alejka")]
		public string? Aisle { get; set; }
	}
}

using Microsoft.AspNetCore.Mvc;
using Sklepix.Models;
using Sklepix.Repositories;
using System.Diagnostics;

namespace Sklepix.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ProductRepository _productRepository;
		private readonly CategoryRepository _categoryRepository;
		private readonly AisleRepository _aisleRepository;
		private readonly ShelfRepository _shelfRepository;

		public HomeController(ILogger<HomeController> logger, ProductRepository productRepository, CategoryRepository categoryRepository, AisleRepository aisleRepository, ShelfRepository shelfRepository)
		{
			_logger = logger;
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_aisleRepository = aisleRepository;
			_shelfRepository = shelfRepository;
		}

		public IActionResult Index()
		{
			HomeViewModel model = new HomeViewModel()
			{
				ProductCount = _productRepository.GetProducts().Count,
				CategoryCount = _categoryRepository.GetCategories().Count,
				AisleCount = _aisleRepository.GetAisles().Count,
				ShelfCount = _shelfRepository.GetShelves().Count
			};

			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
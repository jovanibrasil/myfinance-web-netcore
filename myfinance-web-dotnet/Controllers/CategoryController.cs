using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.interfaces;

namespace myfinance_web_dotnet.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var listCategories = _categoryService.getCategories();
            List<CategoryModel> listCategoriesModel = new List<CategoryModel>();

            foreach (var listCategoryItem in listCategories)
            {
                var itemCategoryModel = new CategoryModel()
                {
                    id = listCategoryItem.id,
                    description = listCategoryItem.description,
                    type = listCategoryItem.categorytype
                };
                listCategoriesModel.Add(itemCategoryModel);
            }

            ViewBag.ListCategories = listCategoriesModel;

            return View();
        }

        [HttpGet]
        [Route("Upsert")]
        [Route("Upsert/{id}")]
        public IActionResult Upsert(int? id)
        {
            if (id == null)
                return View();

            var category = _categoryService.getCategory((int)id);

            var categoryModel = new CategoryModel()
            {
                id = category.id,
                description = category.description,
                type = category.categorytype
            };

            return View(categoryModel);
        }

        [HttpPost]
        [Route("Upsert")]
        [Route("Upsert/{id}")]
        public IActionResult Upsert(CategoryModel categoryModel)
        {
            var category = new Category()
            {
                id = categoryModel.id,
                description = categoryModel.description,
                categorytype = categoryModel.type
            };

            _categoryService.upsert(category);

            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("Remove/{id}")]
        public IActionResult Remove(int? id)
        {
            _categoryService.delete((int)id);

            return RedirectToAction("index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
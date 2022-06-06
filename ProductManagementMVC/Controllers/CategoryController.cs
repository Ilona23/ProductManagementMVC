using Microsoft.AspNetCore.Mvc;
using PagedList;
using ProductManagementMVC.Data;
using ProductManagementMVC.Entities;
using ProductManagementMVC.Interfaces;
using ProductManagementMVC.Models;

namespace ProductManagementMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //public IActionResult Index(string searchString, int? page)
        //{
        //    var pageNumber = page ?? 1;
        //    var pageSize = 10;
        //    var objCategoryList = _categoryService.GetCategories(searchString).ToPagedList(pageNumber, pageSize);
        //    return View(objCategoryList);
        //}

        public IActionResult Index(string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewData["CodeSortParm"] = sortOrder == "Code" ? "Code_desc" : "Code";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
           
            //var objCategoryList = _categoryService.GetCategories("");

            //if (!String.IsNullOrEmpty(searchString))
            //{       
             var objCategoryList = _categoryService.GetCategories(searchString); 
            //}

            switch (sortOrder)
            {
                case "Name_desc":
                    objCategoryList = objCategoryList.OrderByDescending(s => s.Name);
                    break;
                case "Code_desc":
                    objCategoryList = objCategoryList.OrderByDescending(s => s.Code);
                    break;
                case "Description_desc":
                    objCategoryList = objCategoryList.OrderByDescending(s => s.Description);
                    break;
                case "Name":
                    objCategoryList = objCategoryList.OrderBy(s => s.Name);
                    break;
                case "Code":
                    objCategoryList = objCategoryList.OrderBy(s => s.Code);
                    break;
                case "Description":
                    objCategoryList = objCategoryList.OrderBy(s => s.Description);
                    break;
                default:
                    objCategoryList = objCategoryList.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 10;

            return View(PaginatedList<Category>.Create(objCategoryList.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Create(CategoryModel category)
        {
            CreateCategoryResponse createCategoryResponse = new CreateCategoryResponse();

            if (ModelState.IsValid)
            {
                createCategoryResponse = _categoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View(createCategoryResponse.CreatedCategory);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _categoryService.GetCategory(new GetCategoryRequest() { Id = (int)id });
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb.Category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(new UpdateCategoryRequest() { CategoryToUpdate = category });
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _categoryService.GetCategory(new GetCategoryRequest() { Id = (int)id });
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb.Category);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _categoryService.GetCategory(new GetCategoryRequest() { Id = (int)id });
            if (obj == null)
            {
                return NotFound();
            }
            _categoryService.DeleteCategory(new DeleteCategoryRequest(){ Id = (int)id });
            return RedirectToAction("Index");

        }
    }
}

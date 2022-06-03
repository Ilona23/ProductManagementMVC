﻿using ProductManagementMVC.Entities;
using ProductManagementMVC.Models;

namespace ProductManagementMVC.Interfaces
{
    public interface ICategoryService
    {
        GetCategoryResponse GetCategory(GetCategoryRequest request);
        CreateCategoryResponse CreateCategory(CategoryModel request);
        UpdateCategoryResponse UpdateCategory(UpdateCategoryRequest request);
        DeleteCategoryResponse DeleteCategory(DeleteCategoryRequest request);
        IEnumerable<Category> GetCategories(string searchString);
    }
}







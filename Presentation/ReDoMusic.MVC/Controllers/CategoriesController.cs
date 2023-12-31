﻿using DoReMusic.Domain.Entities;
using DoReMusic.MVC.Models;
using DoReMusic.MVC.ViewModels;
using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoReMusic.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        public readonly DoReMusicDbContext _context;
        public CategoriesController()
        {
            _context = new DoReMusicDbContext();
        }


        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddCategory(AddCategoryModel categoryModel)
        {
            if(string.IsNullOrEmpty(categoryModel.Name) || string.IsNullOrEmpty(categoryModel.Kinds) || string.IsNullOrEmpty(categoryModel.Details))
            {
                return BadRequest();
            }
            List<string> kinds = categoryModel.Kinds.Split(',').ToList();
            var CategoryToBeAdded = new Category()
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Name = categoryModel.Name,
                Details = categoryModel.Details,
                Kinds = kinds,
            };

            _context.Categories.Add(CategoryToBeAdded);
            _context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult UpdateCategory( string id)
        {
            Category category = _context.Categories.Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefault();

            return View(category);
        }
        [HttpPost]
        public IActionResult UpdateCategory(CategoryUpdateViewModel updateViewModel)
        {
            Category ModifiedCategory = _context.Categories.Where(x => x.Id == Guid.Parse(updateViewModel.Id)).FirstOrDefault();

            ModifiedCategory.Name = updateViewModel.Name;
            ModifiedCategory.Kinds = updateViewModel.Kinds;
            ModifiedCategory.Details = updateViewModel.Details;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteCategory(string id)
        {
            Category category = _context.Categories.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

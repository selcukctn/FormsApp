﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string search, string category)
    {
        var products = Repository.Products;

        if (!string.IsNullOrEmpty(search))
        {
            ViewBag.search = search;
            products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(category) && int.Parse(category) != 0)
        {
            Console.WriteLine(category);
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        ViewBag.Categories = new SelectList(Repository.Category, "CategoryId", "Name", category);
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

}

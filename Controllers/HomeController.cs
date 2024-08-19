using System.Diagnostics;
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
            products = products.Where(p => p.Name!.ToLower().Contains(search.ToLower())).ToList();
        }
        if (!string.IsNullOrEmpty(category) && int.Parse(category) != 0)
        {
            Console.WriteLine(category);
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        //ViewBag.Categories = new SelectList(Repository.Category, "CategoryId", "Name", category);

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Category,
            SelectedCategory = category
        };
        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Category, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        ViewBag.Categories = new SelectList(Repository.Category, "CategoryId", "Name");
        string currentTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
        if (imageFile != null)
        {
            var extension = Path.GetExtension(imageFile.FileName);
            currentTime += imageFile.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", currentTime);
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Geçerli bir resim seçiniz.(jpeg,jpg,png)");
            }
            if (ModelState.IsValid)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.ProductId = Repository.Products.Count + 1;
                model.Image = currentTime;
                Repository.CreateProduct(model);
                return RedirectToAction("Index");
            }
        }

        return View(model);
    }

}

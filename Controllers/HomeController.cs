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


    public IActionResult Edit(int? id)
    {

        if (id == null)
        {
            return NotFound();
        }
        else
        {
            ViewBag.Categories = new SelectList(Repository.Category, "CategoryId", "Name");
            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, Product model, IFormFile? imageFile)
    {

        if (id == null)
        {
            return NotFound();
        }
        else
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                string currentTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
                model.ProductId = (int)id;
                if (imageFile != null)
                {
                    if (!string.IsNullOrEmpty(model.Image))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", model.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    var extension = Path.GetExtension(imageFile.FileName);
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("", "Geçerli bir resim seçiniz.(jpeg,jpg,png)");
                    }
                    currentTime += imageFile.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", currentTime);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.Image = currentTime;


                    Repository.EditProduct(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("2 Calisti");
                    Repository.EditProduct(model);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Categories = new SelectList(Repository.Category, "CategoryId", "Name");
            return View(model);
        }

    }


    public IActionResult Delete(int? id){
        if(id == null){
            return NotFound();
        } else {
            /*Repository.Delete(id);
            return RedirectToAction("Index");*/
            ViewBag.productId = id;
            return View();
        }
    }
    public IActionResult DeleteStore(int? id){
        if(id == null){
            return NotFound();
        } else {
            Repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

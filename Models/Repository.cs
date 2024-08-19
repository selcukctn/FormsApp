namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _category = new();

        static Repository()
        {
            _category.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _category.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });

            _products.Add(new Product { ProductId = 1, Name = "Telefon Bir", Price = 70000, Image = "1.png", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "Telefon İki", Price = 70000, Image = "2.png", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductId = 3, Name = "Bilgisayar Bir", Price = 40000, Image = "3.png", IsActive = true, CategoryId = 2 });
            _products.Add(new Product { ProductId = 4, Name = "Bilgisayar İki", Price = 40000, Image = "4.png", IsActive = true, CategoryId = 2 });
        }

        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static void CreateProduct(Product product)
        {
            _products.Add(product);
        }

        public static void EditProduct(Product updatedProduct)
        {
            var entitiy = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (entitiy != null)
            {
                entitiy.Name = updatedProduct.Name;
                entitiy.Price = updatedProduct.Price;
                entitiy.Image = updatedProduct.Image;
                entitiy.CategoryId = updatedProduct.CategoryId;
                entitiy.IsActive = updatedProduct.IsActive;
            }
        }

        public static void Delete(int? id)
        {
            var entitiy = _products.FirstOrDefault(p => p.ProductId == id);
            if (entitiy != null)
            {
                if (!string.IsNullOrEmpty(entitiy.Image))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", entitiy.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _products.Remove(entitiy);
            }
        }
        public static List<Category> Category
        {
            get
            {
                return _category;
            }
        }
    }
}
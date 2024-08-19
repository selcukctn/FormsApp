namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _category = new();

        static Repository() {
            _category.Add(new Category {CategoryId = 1, Name = "Telefon"});
            _category.Add(new Category {CategoryId = 2, Name = "Bilgisayar"});

            _products.Add(new Product{ProductId=1, Name="Telefon Bir", Price=70000, Image="1.png", IsActive=true, CategoryId=1 });
            _products.Add(new Product{ProductId=2, Name="Telefon İki", Price=70000, Image="2.png", IsActive=true, CategoryId=1 });
            _products.Add(new Product{ProductId=3, Name="Bilgisayar Bir", Price=40000, Image="3.png", IsActive=true, CategoryId=2 });
            _products.Add(new Product{ProductId=4, Name="Bilgisayar İki", Price=40000, Image="4.png", IsActive=true, CategoryId=2 });
        }

        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static void CreateProduct(Product product){
            _products.Add(product);
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
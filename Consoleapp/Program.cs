using Domain.Models;
using Infrastructure.Repositories;

var categoryRepo = new CategoryRepository();
var productRepo = new ProductRepository();

while (true)
{
    Console.WriteLine("===============================");
    Console.WriteLine(@"1 - Show all categories.
2 - Show category by id.
3 - Add new category.
4 - Update category.
5 - Delete category.
6 - Show all products.
7 - Show product by id.
8 - Show products by category.
9 - Add new product.
10 - Update product.
11 - Delete product.
12 - Get amount of products.
13 - Get amount of categories.
14 - Get amount of products by category.
15 - Get minimum product price by category.
16 - Get maximum product price by category.
17 - Get average product price by category.
18 - Get total price of products.
0 - Exit.");
    var choice = Console.ReadLine();
    Console.WriteLine("===============================");

    switch (choice)
    {
        case "0":
            return;
        case "1":
            foreach (var category in categoryRepo.GetCategories())
            {
                Console.WriteLine($"Id: {category.Id}, Name: {category.Name}");
            }
            break;
        case "2":
            Console.WriteLine("Enter category id:");
            if (int.TryParse(Console.ReadLine(), out int categoryId))
            {
                var category = categoryRepo.GetCategoryById(categoryId);
                if (category != null)
                {
                    Console.WriteLine($"Id: {category.Id}, Name: {category.Name}");
                }
                else
                {
                    Console.WriteLine("Category not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "3":
            Console.WriteLine("Enter new category name:");
            var newCategoryName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCategoryName))
            {
                categoryRepo.AddCategory(new Category { Name = newCategoryName });
                Console.WriteLine("Added successfully.");
            }
            else
            {
                Console.WriteLine("Category name cannot be empty.");
            }
            break;
        case "4":
            Console.WriteLine("Enter category id to update:");
            if (int.TryParse(Console.ReadLine(), out int updateCategoryId))
            {
                Console.WriteLine("Enter new category name:");
                var updatedCategoryName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(updatedCategoryName))
                {
                    categoryRepo.UpdateCategory(new Category { Id = updateCategoryId, Name = updatedCategoryName });
                    Console.WriteLine("Updated.");
                }
                else
                {
                    Console.WriteLine("Category name cannot be empty.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "5":
            Console.WriteLine("Enter category id:");
            if (int.TryParse(Console.ReadLine(), out int deleteCategoryId))
            {
                categoryRepo.DeleteCategory(deleteCategoryId);
                Console.WriteLine("Deleted.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "6":
            foreach (var product in productRepo.GetProducts())
            {
                Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, CategoryId: {product.CategoryId}");
            }
            break;
        case "7":
            Console.WriteLine("Enter product id:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                var product = productRepo.GetProductById(productId);
                if (product != null)
                {
                    Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, CategoryId: {product.CategoryId}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "8":
            Console.WriteLine("Enter category id:");
            if (int.TryParse(Console.ReadLine(), out int productsCategoryId))
            {
                foreach (var product in productRepo.GetProductsByCategory(productsCategoryId))
                {
                    Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, CategoryId: {product.CategoryId}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "9":
            Console.WriteLine("Enter new product name:");
            var newProductName = Console.ReadLine();
            Console.WriteLine("Enter product price:");
            var newProductPriceInput = Console.ReadLine();
            Console.WriteLine("Enter product quantity:");
            var newProductQuantityInput = Console.ReadLine();
            Console.WriteLine("Enter product category id:");
            var newProductCategoryIdInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newProductName) &&
                decimal.TryParse(newProductPriceInput, out decimal newProductPrice) &&
                int.TryParse(newProductQuantityInput, out int newProductQuantity) &&
                int.TryParse(newProductCategoryIdInput, out int newProductCategoryId))
            {
                productRepo.AddProduct(new Product
                {
                    Name = newProductName,
                    Price = newProductPrice,
                    Quantity = newProductQuantity,
                    CategoryId = newProductCategoryId
                });
                Console.WriteLine("Added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "10":
            Console.WriteLine("Enter product id:");
            var updateProductIdInput = Console.ReadLine();
            Console.WriteLine("Enter new product name:");
            var updatedProductName = Console.ReadLine();
            Console.WriteLine("Enter new product price:");
            var updatedProductPriceInput = Console.ReadLine();
            Console.WriteLine("Enter new product quantity:");
            var updatedProductQuantityInput = Console.ReadLine();
            Console.WriteLine("Enter new product category id:");
            var updatedProductCategoryIdInput = Console.ReadLine();

            if (int.TryParse(updateProductIdInput, out int updateProductId) &&
                !string.IsNullOrWhiteSpace(updatedProductName) &&
                decimal.TryParse(updatedProductPriceInput, out decimal updatedProductPrice) &&
                int.TryParse(updatedProductQuantityInput, out int updatedProductQuantity) &&
                int.TryParse(updatedProductCategoryIdInput, out int updatedProductCategoryId))
            {
                productRepo.UpdateProduct(new Product
                {
                    Id = updateProductId,
                    Name = updatedProductName,
                    Price = updatedProductPrice,
                    Quantity = updatedProductQuantity,
                    CategoryId = updatedProductCategoryId
                });
                Console.WriteLine("Updated.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "11":
            Console.WriteLine("Enter product id:");
            if (int.TryParse(Console.ReadLine(), out int deleteProductId))
            {
                productRepo.DeleteProduct(deleteProductId);
                Console.WriteLine("Deleted.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "12":
            Console.WriteLine($"Amount of products: {productRepo.GetAmountOfProducts()}");
            break;
        case "13":
            Console.WriteLine($"Amount of categories: {categoryRepo.GetAmountOfCategories()}");
            break;
        case "14":
            Console.WriteLine("Enter category id:");
            if (int.TryParse(Console.ReadLine(), out int countCategoryId))
            {
                Console.WriteLine($"Amount of products: {productRepo.GetAmountOfProductsByCategory(countCategoryId)}");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "15":
            Console.WriteLine("Enter category id:");
            if (int.TryParse(Console.ReadLine(), out int minCategoryId))
            {
                var minPrice = productRepo.GetMinPriceOfProductByCategory(minCategoryId);
                Console.WriteLine(minPrice.HasValue ? $"Minimum price: {minPrice}" : "No products in this category.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "16":
            Console.WriteLine("Enter category id:");
            if (int.TryParse(Console.ReadLine(), out int maxCategoryId))
            {
                var maxPrice = productRepo.GetMaxPriceOfProductByCategory(maxCategoryId);
                Console.WriteLine(maxPrice.HasValue ? $"Maximum price: {maxPrice}" : "No products in this category.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "17":
            Console.WriteLine("Enter category id:");
            if (int.TryParse(Console.ReadLine(), out int averageCategoryId))
            {
                var averagePrice = productRepo.GetAveragePriceOfProductByCategory(averageCategoryId);
                Console.WriteLine(averagePrice.HasValue ? $"Average price: {averagePrice}" : "No products in this category.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            break;
        case "18":
            Console.WriteLine($"Total price of products: {productRepo.GetTotalPriceOfProducts()}");
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

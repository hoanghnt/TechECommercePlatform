using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed Categories
        if (!await context.Categories.AnyAsync())
        {
            var laptopCategory = Category.Create("Laptops", "Laptop và máy tính xách tay");
            var phoneCategory = Category.Create("Smartphones", "Điện thoại thông minh");
            var accessoryCategory = Category.Create("Accessories", "Phụ kiện điện tử");
            
            context.Categories.AddRange(laptopCategory, phoneCategory, accessoryCategory);
            await context.SaveChangesAsync();
            
            // Sub-categories for Laptops
            var laptopId = laptopCategory.Id;
            var gamingLaptop = Category.Create("Gaming Laptops", "Laptop chơi game cao cấp", laptopId);
            var businessLaptop = Category.Create("Business Laptops", "Laptop văn phòng", laptopId);
            
            context.Categories.AddRange(gamingLaptop, businessLaptop);
            await context.SaveChangesAsync();
            
            Console.WriteLine("Seeded Categories: 5 categories");
        }

        // Seed Products
        if (!await context.Products.AnyAsync())
        {
            var gamingCategoryId = (await context.Categories.FirstAsync(c => c.Name == "Gaming Laptops")).Id;
            var phoneCategoryId = (await context.Categories.FirstAsync(c => c.Name == "Smartphones")).Id;
            var accessoryCategoryId = (await context.Categories.FirstAsync(c => c.Name == "Accessories")).Id;
            
            var products = new[]
            {
                // Gaming Laptops
                Product.Create(
                    "MacBook Pro 16\" M3 Max", 
                    "Apple M3 Max chip, 36GB unified memory, 1TB SSD storage. Liquid Retina XDR display.", 
                    89990000m, 
                    "MBP16-M3MAX-001", 
                    gamingCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/44/309016/macbook-pro-16-m3-max-2023-den-1.jpg"),
                    
                Product.Create(
                    "Dell XPS 15 9530", 
                    "Intel Core i7-13700H, 32GB RAM, NVIDIA RTX 4060 8GB, 1TB SSD", 
                    54990000m, 
                    "DXPS15-9530-001", 
                    gamingCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/44/307982/dell-xps-15-9530-i7-71013161-1-1.jpg"),
                    
                Product.Create(
                    "ASUS ROG Strix G16", 
                    "Intel Core i9-13980HX, 32GB DDR5, RTX 4070 8GB, 1TB SSD", 
                    59990000m, 
                    "ROG-G16-001", 
                    gamingCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/44/309735/asus-rog-strix-g16-i9-g614jv-n4103w-1.jpg"),
                    
                Product.Create(
                    "Lenovo Legion 5 Pro", 
                    "AMD Ryzen 7 7745HX, 16GB RAM, RTX 4060 8GB, 512GB SSD", 
                    37990000m, 
                    "LEGION5PRO-001", 
                    gamingCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/44/307708/lenovo-legion-5-pro-16irx8-i7-82wk00bmvn-1.jpg"),
                    
                Product.Create(
                    "MSI Katana 15", 
                    "Intel Core i7-13620H, 16GB RAM, RTX 4050 6GB, 512GB SSD", 
                    28990000m, 
                    "MSI-KATANA15-001", 
                    gamingCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/44/307956/msi-katana-15-b13vfk-676vn-i7-13620h-1.jpg"),
                
                // Smartphones
                Product.Create(
                    "iPhone 15 Pro Max 256GB", 
                    "Titanium Design, A17 Pro chip, 48MP Main camera, Action button", 
                    34990000m, 
                    "IP15PM-256-001", 
                    phoneCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/42/305658/iphone-15-pro-max-blue-thumbnew-600x600.jpg"),
                    
                Product.Create(
                    "Samsung Galaxy S24 Ultra", 
                    "Snapdragon 8 Gen 3, 12GB RAM, 256GB, 200MP camera, S Pen", 
                    32990000m, 
                    "SGS24U-256-001", 
                    phoneCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/42/307174/samsung-galaxy-s24-ultra-grey-thumbnew-600x600.jpg"),
                    
                Product.Create(
                    "Xiaomi 14 Ultra", 
                    "Snapdragon 8 Gen 3, 16GB RAM, 512GB, Leica Camera System", 
                    29990000m, 
                    "MI14U-512-001", 
                    phoneCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/42/320722/xiaomi-14-ultra-black-thumbnew-600x600.jpg"),
                
                // Accessories
                Product.Create(
                    "AirPods Pro 2nd Gen USB-C", 
                    "Active Noise Cancellation, Adaptive Audio, USB-C charging", 
                    6290000m, 
                    "AIRPODSPRO2-001", 
                    accessoryCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/54/313046/tai-nghe-bluetooth-airpods-pro-2nd-gen-usb-c-charge-apple-1.jpg"),
                    
                Product.Create(
                    "Logitech MX Master 3S", 
                    "Wireless mouse, 8000 DPI, Quiet clicks, Multi-device", 
                    2590000m, 
                    "MXMASTER3S-001", 
                    accessoryCategoryId,
                    "https://cdn.tgdd.vn/Products/Images/86/316619/chuot-khong-day-logitech-mx-master-3s-den-1.jpg")
            };
            
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
            
            Console.WriteLine($"✅ Seeded Products: {products.Length} products");
        }

        // Seed Inventory
        if (!await context.Inventories.AnyAsync())
        {
            var products = await context.Products.ToListAsync();
            foreach (var product in products)
            {
                var inventory = Inventory.Create(product.Id, 50);
                context.Inventories.Add(inventory);
            }
            await context.SaveChangesAsync();
            
            Console.WriteLine($"✅ Seeded Inventories: {products.Count} inventories");
        }
        
        Console.WriteLine("🎉 Database seeding completed!");
    }
}
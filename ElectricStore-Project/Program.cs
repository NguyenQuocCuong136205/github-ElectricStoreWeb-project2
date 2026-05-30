using ElectricStore_Project.Models;
using ElectricStore_Project.Repositories.Address;
using ElectricStore_Project.Repositories.Brands;
using ElectricStore_Project.Repositories.Carts;
using ElectricStore_Project.Repositories.Categories;
using ElectricStore_Project.Repositories.Countries;
using ElectricStore_Project.Repositories.Customers;
using ElectricStore_Project.Repositories.ProductImgs;
using ElectricStore_Project.Repositories.Products;
using ElectricStore_Project.Repositories.Suppliers;
using ElectricStore_Project.Services.Addresses;
using ElectricStore_Project.Services.Brands;
using ElectricStore_Project.Services.Carts;
using ElectricStore_Project.Services.Categories;
using ElectricStore_Project.Services.Countries;
using ElectricStore_Project.Services.Products;
using ElectricStore_Project.Services.Suppliers;
using ElectricStore_Project.Services.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection") ?? "localhost:6379";
//    options.InstanceName = "ElectricStore_"; // Tiền tố tránh trùng lặp key
//});

// dùng tạo cache tạm thời trong bộ nhớ, không dùng Redis
builder.Services.AddDistributedMemoryCache();

// lấy chuỗi kết nối từ appsetting
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//dependency injection
builder.Services.AddDbContext<ElectronicStoreContext>(options => options.UseSqlServer(connectionString));

// thêm repositories vào DI container
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

// thêm services vào DI container
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICartService, CartService>();

// Đăng ký Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/User/AccessDenied";
    });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // Kích hoạt xác thực Cookie
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

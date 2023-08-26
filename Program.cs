using LMS;
using LMS.Repositories;
using LMS.Repositories.Interfaces;
using LMS.Services;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("vi-VN");

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder);
var app = builder.Build();
var isEnv = app.Environment.IsDevelopment();
Configure(app, isEnv);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");

app.Run();

static void ConfigureServices(IServiceCollection services, WebApplicationBuilder? builder)
{
    services.AddControllersWithViews();
    services.AddRouting(options =>
    {
        options.LowercaseUrls = true;
        options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
    });
    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString"));
    });

    // add repositories
    services.AddScoped<IItemRepository, ItemRepository>();
    services.AddScoped<IBookRepository, BookRepository>();
    services.AddScoped<IDvdRepository, DvdRepository>();
    services.AddScoped<IMagazineRepository, MagazineRepository>();
    services.AddScoped<IBorrowerRepository, BorrowerRepository>();
    services.AddScoped<IBorrowedHistoryRepository, BorrowedHistoryRepository>();
    services.AddScoped<IBorrowedItemRepository, BorrowedItemRepository>();
    services.AddScoped<IBorrowedItemTempRepository, BorrowedItemTempRepository>();
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<ICartItemRepository, CartItemRepository>();
    services.AddScoped<ICartItemTempRepository, CartItemTempRepository>();

    // add services
    services.AddScoped<IItemService, ItemService>();
    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IDvdService, DvdService>();
    services.AddScoped<IMagazineService, MagazineService>();
    services.AddScoped<IBorrowerService, BorrowerService>();
    services.AddScoped<IBorrowedHistoryService, BorrowedHistoryService>();
    services.AddScoped<IBorrowedItemService, BorrowedItemService>();
    services.AddScoped<IBorrowedItemTempService, BorrowedItemTempService>();
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<ICartItemService, CartItemService>();
    services.AddScoped<ICartItemTempService, CartItemTempService>();
}

static void Configure(IApplicationBuilder app, bool isEnv)
{
    if (!isEnv)
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
}

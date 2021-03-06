var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CDataBase>
    (
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
        ServiceLifetime.Transient
    );
builder.Services.AddTransient<CEmployees>();
builder.Services.AddTransient<CGifts>();
builder.Services.AddSingleton<CTickatus>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApp.Data;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Регистрация HttpClient
builder.Services.AddHttpClient();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Add services to the container.
builder.Services.AddScoped<ITodoListWebApiService, TodoListWebApiService>();
builder.Services.AddScoped<ITaskItemWebApiService, TaskItemWebApiService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TodoList}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

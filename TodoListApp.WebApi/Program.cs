using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Models.Data;
using TodoListApp.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Добавляем контекст базы данных TodoListDbContext в контейнер служб и указываем сборку миграций
builder.Services.AddDbContext<TodoListDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TodoListDbConnection"),
        b => b.MigrationsAssembly("TodoListApp.WebApi")));

// Регистрация сервисов в DI-контейнере
builder.Services.AddScoped<ITodoListDatabaseService, TodoListDatabaseService>();
builder.Services.AddScoped<ITaskItemDatabaseService, TaskItemDatabaseService>();

// Добавляем поддержку Swagger/OpenAPI (если включена)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using LibrarieOnline.Data;
using LibrarieOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibrarieOnline.Repositories;
using LibrarieOnline.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<AuthorService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Authors.Any())
    {
        context.Authors.AddRange(
            new Author { Name = "Mihai Eminescu", Nationality = "Română" },
            new Author { Name = "Mircea Eliade", Nationality = "Română" },
            new Author { Name = "William Shakespeare", Nationality = "Engleză" }
        );
    }

    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Name = "Ficțiune", Description = "Romane și povești" },
            new Category { Name = "Dezvoltare personală", Description = "Motivație și productivitate" },
            new Category { Name = "IT", Description = "Programare și tehnologie" }
        );
    }

    if (!context.Publishers.Any())
    {
        context.Publishers.AddRange(
            new Publisher { Name = "Editura Litera", Country = "România" },
            new Publisher { Name = "Humanitas", Country = "România" }
        );
    }

    context.SaveChanges();
}
app.Run();

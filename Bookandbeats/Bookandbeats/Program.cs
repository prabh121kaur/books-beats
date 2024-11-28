using BookandBeats.Services;
using BookandBeats.Models;
using Microsoft.EntityFrameworkCore;
using CoreEntityFramework.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPlaylistSongService, PlaylistSongService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IBookPlaylistService, BookPlaylistService>();
builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// Configure Entity Framework Core with the connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers with views (MVC)
builder.Services.AddControllersWithViews();

// Add Swagger services for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS if the frontend is hosted on a different domain or port (for cross-origin requests)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger in development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Use exception handler and HSTS in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable middleware for HTTPS redirection and serving static files
app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable CORS (if needed for frontend API requests)
app.UseCors("AllowAll");

// Enable authorization middleware
app.UseAuthorization();

// Map controllers for both API and MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// For API controllers, ensure that they are included in the route mapping
app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using ProjectCollabTool.Data;
using ProjectCollabTool.Models;

var builder = WebApplication.CreateBuilder(args);

// --- CORS Policy එක හදන තැන ---
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.AllowAnyOrigin() // ඕනම තැනකින් එන request භාරගන්න
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
// --- CORS එක ඉවරයි ---


// 1. Add services to the container.
builder.Services.AddControllers(); 

// 2. Add our DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Add Swagger services (for the test page)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Configure the HTTP request pipeline (how requests are handled)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

// 5. Use HTTPS Redirection ( අපි මේක දැනට comment out කරනවා )
// app.UseHttpsRedirection(); 

// --- App එකට CORS පාවිච්චි කරන්න කියන තැන ---
app.UseCors(MyAllowSpecificOrigins);
// --- ---

// 6. Use Authorization (we'll use this later for logins)
app.UseAuthorization();

// 7. Tell the app to use the routes from our Controllers
app.MapControllers();

// 8. Run the application
app.Run();


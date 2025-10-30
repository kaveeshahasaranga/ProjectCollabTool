using Microsoft.EntityFrameworkCore;
using ProjectCollabTool.Data;
using ProjectCollabTool.Models;
// අලුතෙන් add කරන line එක
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// === 1. CORS Policy එක ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // ඕනම තැනකින් (file:/// ඇතුළුව)
              .AllowAnyMethod() // ඕනම method එකක් (GET, POST)
              .AllowAnyHeader(); // ඕනම header එකක්
    });
});
// ==========================

// 2. Add services to the container.
// --- මෙන්න වෙනස ---
// "AddControllers()" වෙනුවට, cycle handle කරන්න options එක්ක add කරමු
builder.Services.AddControllers();
// --- වෙනස ඉවරයි ---

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// === 4. "AllowAll" Policy එක පාවිච්චි කරන්න කියලා App එකට කියනවා ===
app.UseCors("AllowAll");
// ========================================================

// app.UseHttpsRedirection(); // මේක comment කරලම තියමු
app.UseAuthorization();
app.MapControllers();
app.Run();


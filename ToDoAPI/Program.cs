using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
// tunneling into the db
app.MapGet("api/todo", async (AppDbContext context) =>
{
    var items = await context.ToDos.ToListAsync();
    return Results.Ok(items);
});



app.Run();


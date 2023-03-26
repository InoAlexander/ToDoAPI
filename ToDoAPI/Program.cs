using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

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

app.MapPost("api/todo", async (AppDbContext context, ToDo toDo) =>
{
    await context.ToDos.AddAsync(toDo);
    await context.SaveChangesAsync();
    // passing back the location where the resource can be found
    return Results.Created($"api/todo/{toDo.Id}", toDo);
});

app.Run();


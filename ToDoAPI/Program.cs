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
// post end point
app.MapPost("api/todo", async (AppDbContext context, ToDo toDo) =>
{
    await context.ToDos.AddAsync(toDo);
    await context.SaveChangesAsync();
    // passing back the location where the resource can be found
    return Results.Created($"api/todo/{toDo.Id}", toDo);
});
// update needs to have the id, will also expect an integer id
app.MapPut("api/todo/{id}", async (AppDbContext context, int id, ToDo toDo) =>
{
    // checking that the resource we are updating exists
    var toDoModel = await context.ToDos.FirstOrDefaultAsync(t => t.Id == id);
    // assuming its not null there will be something to update
    if (toDoModel == null) 
    {
        return Results.NotFound();
    }     
    // will update manually
    toDoModel.ToDoName = toDo.ToDoName;
    // saving the update changes
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();


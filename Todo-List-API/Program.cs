using Microsoft.EntityFrameworkCore;
using Todo_List_API;
using Todo_List_API.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using (var context = new TodoDbContext())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.Run(Env.START_URL);

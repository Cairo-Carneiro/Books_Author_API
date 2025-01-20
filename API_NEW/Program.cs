using API_NEW.Data;
using API_NEW.Services.Autor;
using API_NEW.Services.Livros;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAutorinterface, AutorServices>();
builder.Services.AddScoped<ILivrosinterface, LivroServices>();

builder.Services.AddDbContext<AppDbContext>(opions =>
{
    opions.UseSqlServer(builder.Configuration.GetConnectionString("DefaulConnections"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

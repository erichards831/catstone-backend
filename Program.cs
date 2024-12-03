using CatstoneApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS CORS CORS CORS CORS CORS CORS CORS CORS
builder.Services.AddCors(co => {
    co.AddPolicy("CORS", pb => {
        pb.WithOrigins("*")
        .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS
app.UseCors("CORS");
// CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

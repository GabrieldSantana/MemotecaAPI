using Application.Interfaces;
using Application.Services;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
#endregion

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped(provider =>
{
    return new Func<IDbConnection>(() =>
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    });
});

#region Services
builder.Services.AddScoped<IQuoteService, QuoteService>();
#endregion

#region Repositories
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Adiciona middleware de tratamento de erros
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"Ocorreu um erro interno no servidor.\"}");
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

// Comente temporariamente para evitar problemas com HTTPS
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
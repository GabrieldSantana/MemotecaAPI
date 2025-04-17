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

builder.Services.AddScoped<IDbConnection>(provider => // Provedor abrindo a conexão
{
    // Estância do banco
    SqlConnection connection = new SqlConnection(connectionString);
    connection.Open();
    return connection; // Retornando a conexão criada
});

#region Services
builder.Services.AddScoped<IQuoteService, QuoteService>();
#endregion

#region Repositories
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
#endregion


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

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//    policy => {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

//app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
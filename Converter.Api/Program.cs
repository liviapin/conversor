using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Converter.Conversor.Aplicacao.Conversores.Servicos;
using Converter.Conversor.Dominio.Conversores.Servicos;
using Converter.Conversor.Api.Controllers.Conversores;
using Converter.Conversor.Dominio.Conversores.Repositorios;
using Converter.Conversor.Infra.Conversores.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Registra os servi�os necess�rios no cont�iner de DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "MyPolicy",
//            policy =>
//            {
//                policy.WithOrigins("http://localhost:4200")
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//            });
//});


// Registra os servi�os de aplica��o e dom�nio
builder.Services.AddScoped<IConversoresRepositorio, ConversoresRepositorio>(); // Reposit�rio
builder.Services.AddScoped<ConversorJsonService>(); // Servi�o de convers�o JSON
builder.Services.AddScoped<ConversorJsonAppServico>(); // Servi�o de aplica��o

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
}); app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yardim.Conversor.Aplicacao.Conversores.Servicos;
using Yardim.Conversor.Dominio.Conversores.Servicos;
using Yardim.Conversor.Api.Controllers.Conversores;
using Yardim.Conversor.Dominio.Conversores.Repositorios;
using Yardim.Conversor.Infra.Conversores.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Registra os servi�os necess�rios no cont�iner de DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseAuthorization();
app.MapControllers();

app.Run();

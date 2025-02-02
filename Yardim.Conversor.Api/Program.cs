using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yardim.Conversor.Aplicacao.Conversores.Servicos;
using Yardim.Conversor.Dominio.Conversores.Servicos;
using Yardim.Conversor.Api.Controllers.Conversores;
using Yardim.Conversor.Dominio.Conversores.Repositorios;
using Yardim.Conversor.Infra.Conversores.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Registra os serviços necessários no contêiner de DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra os serviços de aplicação e domínio
builder.Services.AddScoped<IConversoresRepositorio, ConversoresRepositorio>(); // Repositório
builder.Services.AddScoped<ConversorJsonService>(); // Serviço de conversão JSON
builder.Services.AddScoped<ConversorJsonAppServico>(); // Serviço de aplicação

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

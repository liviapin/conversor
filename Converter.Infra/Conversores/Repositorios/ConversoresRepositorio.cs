using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converter.Conversor.Dominio.Conversores.Entidades;
using Converter.Conversor.Dominio.Conversores.Repositorios;

namespace Converter.Conversor.Infra.Conversores.Repositorios
{
    public class ConversoresRepositorio : IConversoresRepositorio
    {
        public Task AdicionarAsync(ConversorJson entidade)
        {
            return Task.CompletedTask;
        }
    }
}

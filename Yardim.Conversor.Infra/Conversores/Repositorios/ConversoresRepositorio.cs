using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yardim.Conversor.Dominio.Conversores.Entidades;
using Yardim.Conversor.Dominio.Conversores.Repositorios;

namespace Yardim.Conversor.Infra.Conversores.Repositorios
{
    public class ConversoresRepositorio : IConversoresRepositorio
    {
        public Task AdicionarAsync(ConversorJson entidade)
        {
            return Task.CompletedTask;
        }
    }
}

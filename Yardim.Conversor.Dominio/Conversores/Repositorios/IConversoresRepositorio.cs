using System.Collections.Generic;
using System.Threading.Tasks;
using Yardim.Conversor.Dominio.Conversores.Entidades.Yardim.Conversores.Dominio.ConversorJson;

namespace Yardim.Conversor.Dominio.Conversores.Repositorios
{
    public interface IConversoresRepositorio
    {
        Task AdicionarAsync(ConversorJson entidade);
    }
}

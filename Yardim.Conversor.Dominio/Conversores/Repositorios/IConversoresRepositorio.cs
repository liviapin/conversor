using System.Collections.Generic;
using System.Threading.Tasks;
using Yardim.Conversor.Dominio.Conversores.Entidades;

namespace Yardim.Conversor.Dominio.Conversores.Repositorios
{
    public interface IConversoresRepositorio
    {
        Task AdicionarAsync(ConversorJson entidade);
    }
}

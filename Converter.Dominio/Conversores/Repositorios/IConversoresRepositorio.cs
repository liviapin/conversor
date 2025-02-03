using System.Collections.Generic;
using System.Threading.Tasks;
using Converter.Conversor.Dominio.Conversores.Entidades;

namespace Converter.Conversor.Dominio.Conversores.Repositorios
{
    public interface IConversoresRepositorio
    {
        Task AdicionarAsync(ConversorJson entidade);
    }
}

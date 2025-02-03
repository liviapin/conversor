

using System;
using System.Threading.Tasks;
using Converter.Conversor.Dominio.Conversores.Entidades;
using Converter.Conversor.Dominio.Conversores.Servicos;

namespace Converter.Conversor.Aplicacao.Conversores.Servicos
{
    public class ConversorJsonAppServico
    {
        private readonly ConversorJsonService conversoresServico;

        public ConversorJsonAppServico(ConversorJsonService conversoresServico)
        {
            this.conversoresServico = conversoresServico;
        }

        public async Task<string> ConverterJsonParaCsv(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("O JSON enviado é inválido ou está vazio.");

            try
            {
                var conversorJson = new ConversorJson(json);
                var csv = conversoresServico.ConverterJsonParaCsv(conversorJson);

                return csv;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter JSON para CSV: {ex.Message}", ex);
            }
        }
    }
}

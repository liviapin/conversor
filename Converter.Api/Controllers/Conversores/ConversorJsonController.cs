using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Converter.Conversor.Aplicacao.Conversores.Servicos;
using Converter.Conversor.DataTransfer.Conversores.Requests;
using Converter.Conversor.DataTransfer.Conversores.Responses;

namespace Converter.Conversor.Api.Controllers.Conversores
{
    [ApiController]
    [Route("api/converter")]
    public partial class ConversorJsonController : ControllerBase
    {
        private readonly ConversorJsonAppServico conversorJsonAppServico;

        public ConversorJsonController(ConversorJsonAppServico conversorJsonAppServico)
        {
            this.conversorJsonAppServico = conversorJsonAppServico;
        }

        [HttpPost("json-para-csv")]
        public async Task<IActionResult> JsonParaCsv([FromBody] ConversorJsonRequest request)
        {
            var csv = await conversorJsonAppServico.ConverterJsonParaCsv(request.Json);
            var csvBytes = System.Text.Encoding.UTF8.GetBytes(csv);

            return File(csvBytes, "text/csv", "output.csv");
        }

    }
}

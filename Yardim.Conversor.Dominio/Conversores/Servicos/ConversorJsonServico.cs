using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Yardim.Conversor.Dominio.Conversores.Entidades;

namespace Yardim.Conversor.Dominio.Conversores.Servicos
{
    public class ConversorJsonService
    {
        public string ConverterJsonParaCsv(ConversorJson conversorJson)
        {
            try
            {
                // Deserializa o JSON para um dicionário de chaves e valores
                var dados = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(conversorJson.Json);
                if (dados == null || !dados.Any())
                    throw new InvalidOperationException("JSON inválido ou sem dados.");

                // Listas para armazenar cabeçalhos e valores
                var cabecalhos = new List<string>();
                var valores = new List<List<string>>(); // Cada linha de valores será uma lista dentro dessa lista

                // Flatten o JSON para adicionar cabeçalhos e valores
                FlattenJson(dados, "", cabecalhos, valores);

                // Garantir que todas as linhas têm os mesmos cabeçalhos
                var csv = string.Join(",", cabecalhos) + "\n";

                // Para cada linha de valores, concatene e adicione ao CSV
                foreach (var linha in valores)
                {
                    csv += string.Join(",", linha) + "\n";
                }

                return csv;
            }
            catch (JsonException ex)
            {
                throw new Exception($"Erro ao deserializar JSON: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter JSON para CSV: {ex.Message}", ex);
            }
        }
        //quando falamos em flattening de um objeto JSON, estamos pegando um objeto que pode ter níveis de aninhamento(objetos dentro de objetos,
        //    arrays dentro de objetos, etc.) e transformando-o em um formato mais simples e acessível,
        //    onde as chaves do objeto se tornam únicas e as informações estão em um único nível.
        private void FlattenJson(Dictionary<string, JsonElement> dados, string prefix, List<string> cabecalhos, List<List<string>> valores)
        {
            // Inicializa uma lista para armazenar os valores dessa linha
            var linhaValores = new List<string>();
            foreach (var item in dados)
            {
                var chave = string.IsNullOrEmpty(prefix) ? item.Key : $"{prefix}.{item.Key}";
                var valor = item.Value;

                if (valor.ValueKind == JsonValueKind.Object)
                {
                    // Recursivamente flatten os objetos dentro do JSON
                    FlattenJson(valor.EnumerateObject().ToDictionary(e => e.Name, e => e.Value), chave, cabecalhos, valores);
                }
                else if (valor.ValueKind == JsonValueKind.Array)
                {
                    int index = 0;
                    foreach (var elemento in valor.EnumerateArray())
                    {
                        // Para cada item do array, flatten o valor e adicione ao CSV
                        FlattenJson(new Dictionary<string, JsonElement> { { $"{chave}[{index}]", elemento } }, "", cabecalhos, valores);
                        index++;
                    }
                }
                else
                {
                    // Se a chave ainda não está nos cabeçalhos, adiciona ela
                    if (!cabecalhos.Contains(chave))
                    {
                        cabecalhos.Add(chave);
                    }

                    // Adiciona o valor ao CSV
                    linhaValores.Add(valor.ToString());
                }
            }


            // Adiciona a linha de valores para as conversões que são feitas em cada nível de recursão
            if (linhaValores.Count > 0)
            {
                valores.Add(linhaValores);
            }
        }
    }
}

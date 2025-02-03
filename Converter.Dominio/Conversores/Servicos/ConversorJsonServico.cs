using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Converter.Conversor.Dominio.Conversores.Entidades;

namespace Converter.Conversor.Dominio.Conversores.Servicos
    {
        public class ConversorJsonService
        {
            public string ConverterJsonParaCsv(ConversorJson conversorJson)
            {
                try
                {
                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(conversorJson.Json);

                    if (jsonElement.ValueKind == JsonValueKind.Array)
                    {
                        var jsonArray = jsonElement.EnumerateArray()
                                                   .Select(el => el.Deserialize<Dictionary<string, JsonElement>>())
                                                   .ToList();

                        return ConverterListaJsonParaCsv(jsonArray);
                    }
                    else if (jsonElement.ValueKind == JsonValueKind.Object)
                    {
                        var jsonObject = jsonElement.Deserialize<Dictionary<string, JsonElement>>();
                        return ConverterListaJsonParaCsv(new List<Dictionary<string, JsonElement>> { jsonObject });
                    }
                    else
                    {
                        throw new InvalidOperationException("O JSON deve ser um objeto ou um array de objetos.");
                    }
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

            private string ConverterListaJsonParaCsv(List<Dictionary<string, JsonElement>> jsonLista)
            {
                if (jsonLista == null || !jsonLista.Any())
                    throw new InvalidOperationException("JSON inválido ou sem dados.");

                var cabecalhos = new HashSet<string>();
                var linhas = new List<Dictionary<string, string>>();

                foreach (var obj in jsonLista)
                {
                    var linha = new Dictionary<string, string>();
                    FlattenJson(obj, "", linha);
                    cabecalhos.UnionWith(linha.Keys);
                    linhas.Add(linha);
                }

                var csv = new StringBuilder();
                csv.AppendLine(string.Join(",", cabecalhos));

                foreach (var linha in linhas)
                {
                    var valores = cabecalhos.Select(c => linha.ContainsKey(c) ? linha[c] : "").ToList();
                    csv.AppendLine(string.Join(",", valores));
                }

                return csv.ToString();
            }

            private void FlattenJson(Dictionary<string, JsonElement> dados, string prefixo, Dictionary<string, string> resultado)
            {
                foreach (var item in dados)
                {
                    var chave = string.IsNullOrEmpty(prefixo) ? item.Key : $"{prefixo}.{item.Key}";
                    var valor = item.Value;

                    switch (valor.ValueKind)
                    {
                        case JsonValueKind.Object:
                            FlattenJson(valor.EnumerateObject().ToDictionary(e => e.Name, e => e.Value), chave, resultado);
                            break;

                        case JsonValueKind.Array:
                            int index = 0;
                            foreach (var elemento in valor.EnumerateArray())
                            {
                                FlattenJson(new Dictionary<string, JsonElement> { { $"{chave}[{index}]", elemento } }, "", resultado);
                                index++;
                            }
                            break;

                        case JsonValueKind.String:
                        case JsonValueKind.Number:
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            resultado[chave] = valor.ToString();
                            break;

                        case JsonValueKind.Null:
                            resultado[chave] = "";
                            break;

                        default:
                            resultado[chave] = valor.ToString();
                            break;
                    }
                }
            }
        }
    }


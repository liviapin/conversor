using System;
using Converter.Conversor.Dominio.Bases.Entidades;

namespace Converter.Conversor.Dominio.Conversores.Entidades
{
    public class ConversorJson : EntidadeBase
    {
        public string Json { get; private set; }

        public ConversorJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("O JSON não pode estar vazio.");

            Json = json;
        }
    }
}

using System;
using Yardim.Conversor.Dominio.Bases.Entidades;

namespace Yardim.Conversor.Dominio.Conversores.Entidades
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

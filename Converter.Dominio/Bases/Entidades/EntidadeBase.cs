using System;

namespace Converter.Conversor.Dominio.Bases.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}

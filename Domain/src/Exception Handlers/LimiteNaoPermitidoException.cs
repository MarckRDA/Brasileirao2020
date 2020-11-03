using System;

namespace Domain.src
{
    // Exceção personalizada
    public class LimiteNaoPermitidoException : Exception
    {
        public LimiteNaoPermitidoException(string msg) : base(msg) {}
    }
}
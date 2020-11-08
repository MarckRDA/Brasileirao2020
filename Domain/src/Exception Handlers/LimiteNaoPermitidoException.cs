using System;

namespace Domain.src.ExceptionsHandlers
{
    // Exceção personalizada
    public class LimiteNaoPermitidoException : Exception
    {
        public LimiteNaoPermitidoException(string msg) : base(msg) {}
    }
}
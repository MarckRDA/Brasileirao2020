using System;

namespace Domain
{
    // Exceção personalizada
    public class LimiteNaoPermitidoException : Exception
    {
        public LimiteNaoPermitidoException(string msg) : base(msg) {}
    }
}
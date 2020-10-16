using System;

namespace Domain
{
    public class LimiteNaoPermitidoException : Exception
    {
        public LimiteNaoPermitidoException(string msg) : base(msg) {}
    }
}
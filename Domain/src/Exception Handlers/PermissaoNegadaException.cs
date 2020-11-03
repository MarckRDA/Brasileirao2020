using System;

namespace Domain.src
{
    // Exceção personalizada
    public class PermissaoNegadaException : Exception
    {
        public PermissaoNegadaException(string msg) : base(msg){}
    }
}
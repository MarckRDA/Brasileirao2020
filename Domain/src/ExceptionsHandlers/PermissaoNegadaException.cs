using System;

namespace Domain.src.ExceptionsHandlers
{
    // Exceção personalizada
    public class PermissaoNegadaException : Exception
    {
        public PermissaoNegadaException(string msg) : base(msg){}
    }
}
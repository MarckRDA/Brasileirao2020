using System;

namespace Domain
{
    // Exceção personalizada
    public class PermissaoNegadaException : Exception
    {
        public PermissaoNegadaException(string msg) : base(msg){}
    }
}
using System;

namespace Domain
{
    public class PermissaoNegadaException : Exception
    {
        public PermissaoNegadaException(string msg) : base(msg){}
    }
}
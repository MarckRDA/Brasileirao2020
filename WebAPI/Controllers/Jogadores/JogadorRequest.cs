using System;
using System.Collections.Generic;
using System.Linq;


namespace WebAPI.Controllers.Jogadores
{
    public class JogadorRequest 
    {
        public string Nome { get; set; }
        public Guid Id { get; private set; } = new Guid();        
        public int Gol { get; private set;} = 0;
    }
}
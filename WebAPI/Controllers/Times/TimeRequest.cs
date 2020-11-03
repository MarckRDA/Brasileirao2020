using System.Collections.Generic;
using Domain.src;

namespace WebAPI.Times
{
    public class TimeRequest
    {
        public string Nome { get; set; }
        public List<Jogador> Jogadores {get; set;} = new List<Jogador>();  
    
    }    
}
using System;

namespace Domain.src.Jogadores
{
    public class JogadorDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Gol { get; set; }
    }
}
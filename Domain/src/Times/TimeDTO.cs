using System;
using System.Collections.Generic;
using Domain.src.Jogadores;
using Domain.src.TabelaEstatistica;

namespace Domain.src.Times
{
    public class TimeDTO
    {
        public Guid Id { get; set; }
        public string NomeTime {get; set;}
        public List<JogadorDTO> Jogadores { get; set;} 
        public TabelaDeEstatisticaTime Tabela{get; set;}
    }
}
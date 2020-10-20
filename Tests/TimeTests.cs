using System.Collections.Generic;
using Domain;
using Xunit;

namespace Tests
{
    public class TimeTests
    {
        [Fact]
        public void Should_Create_A_Time()
        {
            //Given
            Time atletico = new TimeCampeonatoBrasileirao("Atletico");

            //When

            //Then
            Assert.NotNull(atletico);
        }


        [Fact]
        public void Should_ADD_Jogadores_In_Time()
        {
            //Given
            Time atlhetico = new TimeCampeonatoBrasileirao("Atlhetico");
            var listaJogadores = GeradorDeJogadoresAthetico();
            
            //When
            var result = atlhetico.AdicionarListaDeJogadores(listaJogadores);

            //Then
            Assert.True(result);
            
        }



        public List<Jogador> GeradorDeJogadoresAthetico()
        {
            List<Jogador> timeAthelticoParanaense = new List<Jogador>()
            {
                new JogadorTime("Santos"),
                new JogadorTime("Leo Gomes"),
                new JogadorTime("Fabinho"),
                new JogadorTime("Pedro Henrique"),
                new JogadorTime("Thiago Heleno"),
                new JogadorTime("Abner vinicius"),
                new JogadorTime("Wellington"),
                new JogadorTime("Richard"),
                new JogadorTime("Christian"),
                new JogadorTime("Leo Cittadin"),
                new JogadorTime("Ravanelli"),
                new JogadorTime("Bissoli"),
                new JogadorTime("Carlos Eduardo"),
                new JogadorTime("Nikão"),
                new JogadorTime("Renato Kayser"),
                new JogadorTime("Geuvanio"),
                new JogadorTime("Jandrei"),
                new JogadorTime("Kellven"),
                new JogadorTime("Aguilar"),
                new JogadorTime("João Victor"),
                new JogadorTime("Lucho Gonzales"),
                new JogadorTime("Alvarado"),
                new JogadorTime("Walter"),

            };

            return timeAthelticoParanaense;
            
        }
        public List<Jogador> GeradorDeJogadoresAteticoGoianiense()
        {

            List<Jogador> timeAtleticoGoianiense = new List<Jogador>()
            {

                new JogadorTime("Jean"),
                new JogadorTime("Dudu"),
                new JogadorTime("João Victor"),
                new JogadorTime("Éder"),
                new JogadorTime("Natanael"),
                new JogadorTime("Willian Maranhão"),
                new JogadorTime("Marlon Freitas"),
                new JogadorTime("Arnaldo"),
                new JogadorTime("Zé Roberto"),
                new JogadorTime("Hyuri"),
                new JogadorTime("Matheus Vargas"),
                new JogadorTime("Matheuzinho"),
                new JogadorTime("Mauricio Kozlinski"),
                new JogadorTime("Luan Sales"),
                new JogadorTime("Oliveira"),
                new JogadorTime("Gilvan"),
                new JogadorTime("Everton Felipe"),
                new JogadorTime("Wellington Rato"),
                new JogadorTime("Junior Brandão"),
            

            };
            return timeAtleticoGoianiense;
     
        }  
         public List<Jogador> GeradorDeJogadoresAteticoMineiro()
        {


            List<Jogador> timeAtleticoMineiro= new List<Jogador>() 
            {
              new JogadorTime("Éverson"),
              new JogadorTime("Guga"),
              new JogadorTime("Réver"),
              new JogadorTime("junior Alonso"),
              new JogadorTime("Guilherme Arana"),
              new JogadorTime("Jair"),
              new JogadorTime("Franco"),
              new JogadorTime("Nathan"),
              new JogadorTime("Savarino"),
              new JogadorTime("Eduardo Sasha"),
              new JogadorTime("Keno"),
              new JogadorTime("Victor"),
              new JogadorTime("Rafael"),
              new JogadorTime("Mailton"),
              new JogadorTime("Mariano"),
              new JogadorTime("Igor Rabello"),
              new JogadorTime("Bueno"),
              new JogadorTime("Gabriel"),
              new JogadorTime("Borrero"),
              new JogadorTime("Calebe"),
              new JogadorTime("Savio"),
              new JogadorTime("Marrony"),
              new JogadorTime("Marquinhos"),


            };
            return timeAtleticoMineiro;
        }
        public List<Jogador> GeradorDeJogadoresBahia()
        {


            List<Jogador> timeBahia = new List<Jogador>() 
            {
                new JogadorTime("Douglas Friedrich"),
                new JogadorTime("Ernando"),
                new JogadorTime("Lucas Fonseca"),
                new JogadorTime("Juninho"),
                new JogadorTime("Juninho Capixaba"),
                new JogadorTime("Gregore"),
                new JogadorTime("Elias"),
                new JogadorTime("Edson"),
                new JogadorTime("Ramon"),
                new JogadorTime("Fessin"),
                new JogadorTime("Clayson"),
                new JogadorTime("Mateus Claus"),
                new JogadorTime("Wanderson"),
                new JogadorTime("Nino Paraiba"),
                new JogadorTime("Anderson Martins"),
                new JogadorTime("Matheus Bahia"),
                new JogadorTime("Ronaldo"),
                new JogadorTime("Daniel"),
                new JogadorTime("Ramires"),
                new JogadorTime("Alesson"),
                new JogadorTime("Marco Antonio"),
                new JogadorTime("Gilberto"),
                new JogadorTime("Saldanha"),
            };
            return timeBahia;

        }
          public List<Jogador> GeradorDeJogadoresBotafogo()
        {

            List<Jogador> timeBotafogo = new List<Jogador>() 
            {
              new JogadorTime("Diego Cavalieri"),
              new JogadorTime("Kevin"),
              new JogadorTime("Marcelo Benevenuto"),
              new JogadorTime("Kanu"),
              new JogadorTime("Victor Luis"),
              new JogadorTime("Rafael Forster"),
              new JogadorTime("Caio Alexandre"),
              new JogadorTime("Honda"),
              new JogadorTime("Bruno Nazario"),
              new JogadorTime("Rhuan"),
              new JogadorTime("Pedro Raul"),
              new JogadorTime("Saulo"),
              new JogadorTime("Barrandeguy"),
              new JogadorTime("David Souza"),
              new JogadorTime("Guilherme Santos"),
              new JogadorTime("Renteria"),
              new JogadorTime ("Eder Bessa"),
              new JogadorTime("Luiz Otávio"),
              new JogadorTime("Cicero"),
              new JogadorTime("matheus Babi"),
              new JogadorTime("Lecaros"),
              new JogadorTime("Warley"),
              new JogadorTime("Davi Araujo"),
            };
            return timeBotafogo;
        }
         public List<Jogador> GeradorDeJogadoresBragantino()
        {


            List<Jogador> timeBragantino = new List<Jogador>() 
             {
               new JogadorTime("Cleiton"),
               new JogadorTime("Aderlan"),
               new JogadorTime("Léo Ortiz"),
               new JogadorTime("Ligger"),
               new JogadorTime("Wewerson"),
               new JogadorTime("Edimar"),
               new JogadorTime("Ricardo Ryller"),
               new JogadorTime("Uillian Correia"),
               new JogadorTime ("Raul"),
               new JogadorTime ("Arthur"),
               new JogadorTime("Cuello"),
               new JogadorTime("Bruno Tubarão"),
               new JogadorTime("Ytalo"),
               new JogadorTime("Hurtado"),
               new JogadorTime("Claudinho"),
               new JogadorTime("Luis Phelipe"),
               new JogadorTime("Julio Cesar"),
               new JogadorTime("Haydar"),
               new JogadorTime("Fabricio Brunno"),
               new JogadorTime("Chirgor"),
               new JogadorTime("Morato"),
             };
             return timeBragantino;
        }
         public List<Jogador> GeradorDeJogadoresCeara()
        {



           List<Jogador> timeCeara = new List<Jogador>() 
           {
             new JogadorTime("Fernando Prass"),
             new JogadorTime("Fabinho"),
             new JogadorTime("Tiago"),
             new JogadorTime("Eduardo Brock"),
             new JogadorTime("Bruno Pacheco"),
             new JogadorTime("Pedro Naressi"),
             new JogadorTime("Leandro Carvalho"),
             new JogadorTime("Charles"),
             new JogadorTime("Fernando Sobral"),
             new JogadorTime("Vinicius"),
             new JogadorTime("Lima"),
             new JogadorTime("Leo Chu"),
             new JogadorTime("Kelvyn"),
             new JogadorTime("Rafael Sobis"),
             new JogadorTime("Cleber"),
             new JogadorTime("Diogo Silva"),
             new JogadorTime("Gabriel Lacerda"),
             new JogadorTime("Alyson"),
             new JogadorTime("Marthã"),
             new JogadorTime("Felipe Baixola"),
             new JogadorTime("Wescley"),
             new JogadorTime("Mateus Goncalves"),
            };
            return timeCeara;
        }
          public List<Jogador> GeradorDeJogadoresCorintians()
        {
           List<Jogador> timeCorintians = new List<Jogador>() 
           {
               new JogadorTime("Cassio"),
               new JogadorTime("Fagner"),
               new JogadorTime("Marllon"),
               new JogadorTime("Gil"),
               new JogadorTime("Lucas Piton"),
               new JogadorTime("Xavier"),
               new JogadorTime("Camacho"),
               new JogadorTime("Gabriel"),
               new JogadorTime("Otero"),
               new JogadorTime("Gustavo Mantuan"),
               new JogadorTime("Mateus Vital"),
               new JogadorTime("Cazares"),
               new JogadorTime("Everaldo"),
               new JogadorTime("Gustavo Mosquito"),
               new JogadorTime("Boselli"),
               new JogadorTime("Luan"),
               new JogadorTime("Walter"),
               new JogadorTime("Michel Macedo"),
               new JogadorTime("Sidcley"),
               new JogadorTime("Éderson"),
               new JogadorTime("Cantillo"),
               new JogadorTime("Roni"),
               new JogadorTime("Leo Natel"),
               new JogadorTime("Jô"),
           };
           return timeCorintians;
        }
         public List<Jogador> GeradorDeJogadoresCoritiba()
        {

           List<Jogador> timeCoritiba = new List<Jogador>()
          {
            new JogadorTime("Wilson"),
            new JogadorTime("Natanael"),
            new JogadorTime("Matheus Galdezani"),
            new JogadorTime("Hanrique Vermudt"),
            new JogadorTime("Nathan Silva"),
            new JogadorTime("Willian Mateus"),
            new JogadorTime("Matheus Salles"),
            new JogadorTime("Matheus Bueno"),
            new JogadorTime("Hugo Moura"),
            new JogadorTime("Ricardo Oliveira"),
            new JogadorTime("Yan Sasse"),
            new JogadorTime("Nathan"),
            new JogadorTime("Giovanni Augusto"),
            new JogadorTime("Robson"),
            new JogadorTime("Rodrigo Muniz"),
            new JogadorTime("Neilton"),
            new JogadorTime("Alex Muralha"),
            new JogadorTime("Ramón Martinez"),
            new JogadorTime("Sarrafiore"),
            new JogadorTime("Gabriel"),
            new JogadorTime("Luiz Henrique"),
            new JogadorTime("Mattheus"),
            new JogadorTime("Cerutti"),
          };
          return timeCoritiba;
        }
        public List<Jogador> GeradorDeJogadoresFlamengo()
        {


           List<Jogador> timeFlamengo = new List<Jogador>()
           {
             new JogadorTime("Hugo Souza"),
             new JogadorTime("Diego Alves"),
             new JogadorTime("Mauricio Isla"),
             new JogadorTime("Rodrigo Caio"),
             new JogadorTime("Gustavo Henrique"),
             new JogadorTime("Natan"),
             new JogadorTime("Gabriel Noga"),
             new JogadorTime("Filipe Luis"),
             new JogadorTime("Thiago Maia"),
             new JogadorTime("Willian Arão"),
             new JogadorTime("Gerson"),
             new JogadorTime("Everton Ribeiro"),
             new JogadorTime("Ramon"),
             new JogadorTime("De Arrascaeta"),
             new JogadorTime("Bruno Henrique"),
             new JogadorTime("Pedro"),
             new JogadorTime("Lincoln"),
             new JogadorTime("Vitinho"),
             new JogadorTime("Diego"),
             new JogadorTime("Gabigol"),
             new JogadorTime("Cézar"),
             new JogadorTime("Gabriel Batista"),
             new JogadorTime("Matheuzinho"),
             new JogadorTime("João Lucas"),
             new JogadorTime("Thuler"),
             new JogadorTime("Léo Pereira"),
             new JogadorTime("Renê"),
             new JogadorTime("Gomes"),
             new JogadorTime("Pepe"),
             new JogadorTime("Pedro Rocha"),
             new JogadorTime("Michael"),
           };
           return timeFlamengo;
        }
         public List<Jogador> GeradorDeJogadoresFluminense()
        {



           List<Jogador> timeFluminense = new List<Jogador>()
           {
            new JogadorTime("Muriel Becker"),
            new JogadorTime("Igor Julião"),
            new JogadorTime("Nino"),
            new JogadorTime("Digão"),
            new JogadorTime("Danilo Barcelos"),
            new JogadorTime("Hudson"),
            new JogadorTime("Caio Paulista"),
            new JogadorTime("Dodi"),
            new JogadorTime("Nenê"),
            new JogadorTime("Ganso"),
            new JogadorTime("Yago Felipe"),
            new JogadorTime("André"),
            new JogadorTime("Felippe Cardoso"),
            new JogadorTime("Luiz Henrique"),
            new JogadorTime("Marcos Paulo"),
            new JogadorTime("Fred"),
            new JogadorTime("Marcos Felipe"),
            new JogadorTime("Callegari"),
            new JogadorTime("Matheus Ferraz"),
            new JogadorTime("Luccas Claro"),
            new JogadorTime("Egidio"),
            new JogadorTime("Miguel"),
            new JogadorTime("Lucca"),           
            };
            return timeFluminense;
        }
         public List<Jogador> GeradorDeJogadoresFortaleza()
        {

           List<Jogador> timeFortaleza = new List<Jogador>()
           {
               new JogadorTime("Felipe Alves"),
               new JogadorTime("Max Walef"),
               new JogadorTime("Tinga"),
               new JogadorTime("Gabriel Dias"),
               new JogadorTime("Paulão"),
               new JogadorTime("Roger Carvalho"),
               new JogadorTime("Bruno Melo"),
               new JogadorTime("felipe"),
               new JogadorTime("Marlon"),
               new JogadorTime("Ronald"),
               new JogadorTime("Romarinho"),
               new JogadorTime("Osvaldo"),
               new JogadorTime("Carlinhos"),
               new JogadorTime("David"),
               new JogadorTime("Juninho"),
               new JogadorTime("Yuri Cesar"),
               new JogadorTime("Wellington Paulista"),
               new JogadorTime("Jackson"),
               new JogadorTime("Derley"),
               new JogadorTime("Mariano Vazquez"),
               new JogadorTime("Bergson"),
               new JogadorTime("Igor Torres"),
               new JogadorTime("Éderson"),
           };
           return timeFortaleza;
        }
         public List<Jogador> GeradorDeJogadoresGoias()
        {
           List<Jogador> timeGoias = new List<Jogador>()
           {
             new JogadorTime("Tadeu"),
             new JogadorTime("Edilson"),
             new JogadorTime("David Duarte"),
             new JogadorTime("Fábio Sanches"),
             new JogadorTime("Caju"),
             new JogadorTime("Breno"),
             new JogadorTime("Ariel Cabral"),
             new JogadorTime("Daniel Bessa"),
             new JogadorTime("Douglas Baggio"),
             new JogadorTime("Shaylon"),
             new JogadorTime("Fernandão"),
             new JogadorTime("Keko"),
             new JogadorTime("Ratinho"),
             new JogadorTime("Vinicius"),
             new JogadorTime("Matheus Alves"),
             new JogadorTime("Pintado"),
             new JogadorTime("Iago Mendonça"),
             new JogadorTime("Heron"),
             new JogadorTime("Jefferson"),
             new JogadorTime("Daniel Oliveira"),
             new JogadorTime("Salazar"),
             new JogadorTime("Sandrinho"),
           };
           return timeGoias;
        }
         public List<Jogador> GeradorDeJogadoresGremio()
        {

           List<Jogador> timeGremio = new List<Jogador>()
           {
             new TimeJogador("Vanderlei"),
             new TimeJogador("Orejuela"),
             new TimeJogador("Pedro Geromel"),
             new TimeJogador("Kannemann"),
             new TimeJogador("Bruno Cortez"),
             new TimeJogador("Matheus Henrique"),
             new TimeJogador("Maicon"),
             new TimeJogador("Jean Pyerre"),
             new TimeJogador("Alison"),
             new TimeJogador("Thaciano"),
             new TimeJogador("Isaque"),
             new TimeJogador("lucas Silva"),
             new TimeJogador("Luiz Fernando"),
             new TimeJogador("Ferreira"),
             new TimeJogador("Pepê"),
             new TimeJogador("Paulo Victor"),
             new TimeJogador("Victor Ferraz"),
             new TimeJogador("David Braz"),
             new TimeJogador("Rodrigues"),
             new TimeJogador("Diogo Barbosa"),
             new TimeJogador("Everton"),
             new TimeJogador("Robinho"),
             new TimeJogador("Guilherme Azevedo"),
             new TimeJogador("Diego Souza"),
           };
           return timeGremio;
        }
         public List<Jogador> GeradorDeJogadoresInternacional()
        {
           List<Jogador> timeInternacional = new List<Jogador>()
           {
             new TimeJogador("Marcelo Lomba"),
             new TimeJogador("Rodinei"),
             new TimeJogador("Zé Gabriel"),
             new TimeJogador("Victor Cuesta"),
             new TimeJogador("Uendel"),
             new TimeJogador("Rodrigo Lindoso"),
             new TimeJogador("Praxedes"),
             new TimeJogador("Edenilson"),
             new TimeJogador("Marcos Guilherme"),
             new TimeJogador("Rodrigo Moledo"),
             new TimeJogador("Patrick"),
             new TimeJogador("Rodrigo Dourado"),
             new TimeJogador("Thiago Galhardo"),
             new TimeJogador("D'alessandro"),
             new TimeJogador("Abel Hernandez"),
             new TimeJogador("Yuri Alberto"),
             new TimeJogador("Danilo Fernandes"),
             new TimeJogador("Mazetti"),
             new TimeJogador("Moisés"),
             new TimeJogador("Peglow"),
             new TimeJogador("Nonato"),
             new TimeJogador("William Pottker"),
             new TimeJogador("Leandro Fernandez"), 
           };
           return timeInternacional;
        }
         public List<Jogador> GeradorDeJogadoresPalmeiras()
        {
           List<Jogador> timePalmeiras = new List<Jogador>()
           {
             new TimeJogador("Weverton"),
             new TimeJogador("Mayke"),
             new TimeJogador("Wesley"),
             new TimeJogador("Emerson Santos"),
             new TimeJogador("Gustavo Gomez"),
             new TimeJogador("Vinã"),
             new TimeJogador("Gabriel Menino"),
             new TimeJogador("Patrick de Paula"),
             new TimeJogador("Willian Bigode"),
             new TimeJogador("Zé Rafael"),
             new TimeJogador("Raphael Veiga"),
             new TimeJogador("Danilo"),
             new TimeJogador("Lucas Lima"),
             new TimeJogador("luiz Adriano"),
             new TimeJogador("Gabriel Veron"),
             new TimeJogador("Jailson"),
             new TimeJogador("Vinicius Silvestre"),
             new TimeJogador("Renan"),
             new TimeJogador("Ramires"),
             new TimeJogador("Gustavo Scarpa"),
             new TimeJogador("Rony"),
           };
           return timePalmeiras;
        }
        public List<Jogador> GeradorDeJogadoresSantos()
        {
           List<Jogador> timeSantos = new List<Jogador>()
           {
             new TimeJogador("João Paulo"),
             new TimeJogador("Madson"),
             new TimeJogador("Laércio"),
             new TimeJogador("Luan Peres"),
             new TimeJogador("Felipe Jonatan"),
             new TimeJogador("Jobson"),
             new TimeJogador("Sandry"),
             new TimeJogador("Diego Pituca"),
             new TimeJogador("Jean Mota"),
             new TimeJogador("Lucas Lourenço"),
             new TimeJogador("Lucas Braga"),
             new TimeJogador("Arthur Gomes"),
             new TimeJogador("Kaio Jorge"),
             new TimeJogador("Luiz Felipe"),
             new TimeJogador("Soteldo"),
             new TimeJogador("Wagner Leonardo"),
             new TimeJogador("John Victor"),
             new TimeJogador("Alex"),
             new TimeJogador("Ivonei"),
             new TimeJogador("Anderson Ceará"),
             new TimeJogador("Marcos Leonardo"),
             new TimeJogador("Tailson"),
            }; 
            return timeSantos;
        }
         public List<Jogador> GeradorDeJogadoresSaoPaulo()
        {
           List<Jogador> timeSaoPaulo = new List<Jogador>()
           {
             new TimeJogador("Tiago Volpi"),
             new TimeJogador("Daniel Alves"),
             new TimeJogador("Diego"),
             new TimeJogador("Bruno Alves"),
             new TimeJogador("Reinaldo"),
             new TimeJogador("Luan"),
             new TimeJogador("Tchê Tchê"),
             new TimeJogador("Gabriel Sara"),
             new TimeJogador("Toró"),
             new TimeJogador("Igor Gomes"),
             new TimeJogador("Vitor Bueno"),
             new TimeJogador("Brenner"),
             new TimeJogador("Paulinho Boia"),
             new TimeJogador("Luciano"),
             new TimeJogador("Santiago Tréllez"),
             new TimeJogador("Thiago Couto"),
             new TimeJogador("Júnior"),
             new TimeJogador("Rodrigo Freitas"),
             new TimeJogador("Léo"),
             new TimeJogador("Rodrigo Nestor"),
             new TimeJogador("Pablo"),
             new TimeJogador("Gonzalo Carneiro"),
             new TimeJogador("Hernanes"),
             new TimeJogador("Liziero"),
             new TimeJogador("Juanfran"),
             new TimeJogador("Lucas Perri"),
             new TimeJogador("Igor Vinicius"),
             new TimeJogador("Rojas"),
             new TimeJogador("Walce"),
           };
           return timeSaoPaulo;
        }
         public List<Jogador> GeradorDeJogadoresSport()
        {
           List<Jogador> timeSport = new List<Jogador>()
           {
             new TimeJogador("Luan Polli"),
             new TimeJogador("Patric"),
             new TimeJogador("Iago Maidana"),
             new TimeJogador("Adryelson"),
             new TimeJogador("Luciano Juba"),
             new TimeJogador("Marcão Silva"),
             new TimeJogador("Ronaldo Henrique"),
             new TimeJogador("Ricardinho"),
             new TimeJogador("Mikael"),
             new TimeJogador("Lucas Mugni"),
             new TimeJogador("Thiago Neves"),
             new TimeJogador("Jonathan Gomez"),
             new TimeJogador("Marquinhos"),
             new TimeJogador("Rogério"),
             new TimeJogador("Leandro Barcia"),
             new TimeJogador("Junior Tavares"),
             new TimeJogador("Mailson"),
             new TimeJogador("Ewerthon"),
             new TimeJogador("Rafael Thyere"),
             new TimeJogador("Chico"),
             new TimeJogador("Márcio Araujo"),
             new TimeJogador("Hernane Brocador"),
           };
           return timeSport;
        }
         public List<Jogador> GeradorDeJogadoresVasco()
        {
           List<Jogador> timeVasco = new List<Jogador>()
           {
             new TimeJogador("Fernando Miguel"),
             new TimeJogador("Yago Pikachu"),
             new TimeJogador("Miranda"),
             new TimeJogador("Leandro Castan"),
             new TimeJogador("Henrique"),
             new TimeJogador("Andrey"),
             new TimeJogador("Felipe Bastos"),
             new TimeJogador("Marcos Junior"),
             new TimeJogador("Guilherme Parede"),
             new TimeJogador("Carlinhos"),
             new TimeJogador("Benitez"),
             new TimeJogador("Vinícius"),
             new TimeJogador("Tales Magno"),
             new TimeJogador("German Cano"),
             new TimeJogador("Lucão"),
             new TimeJogador("Ulisses"),
             new TimeJogador("Werley"),
             new TimeJogador("Neto Borges"),
             new TimeJogador("Gabriel Pec"),
             new TimeJogador("Lucas Santos"),
             new TimeJogador("Ribamar"),
             new TimeJogador("Tiago Reis"),
             new TimeJogador("Ygor Catatau"),
             new TimeJogador("Ricardo Graça"),
             new TimeJogador("Bruno Gomes"),
             

           };
        }







    }
}    
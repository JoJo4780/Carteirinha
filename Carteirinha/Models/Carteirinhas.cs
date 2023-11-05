using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carteirinha.Models
{
    public class Carteirinhas
    {
        public int Id {get; set;}
        [Required(ErrorMessage ="Coloque o nome")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "Coloque o nome do seu Idolo")]
        public string Idolo { get; set;}

        [Required(ErrorMessage = "Coloque a sua Idade")]
        public string Idade { get; set;}
        public DateTime Request_time { get; set; } = DateTime.Now;
        public string URL_Image { get; set; }
        public string Code { get; set;}

        [ForeignKey("Usuario")]
        public int Usuario_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public string Token_Basic()
        {
            // Crie uma instância da classe Random
            Random random = new Random();

            // Defina a string de caracteres possíveis
            string caracteresPossiveis = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            // Defina o tamanho da sequência aleatória desejada
            int tamanhoSequencia = 6; // Altere para o tamanho desejado

            // Crie uma variável para armazenar a sequência aleatória
            char[] sequenciaAleatoria = new char[tamanhoSequencia];

            // Gere a sequência aleatória
            for (int i = 0; i < tamanhoSequencia; i++)
            {
                int indiceAleatorio = random.Next(caracteresPossiveis.Length);
                sequenciaAleatoria[i] = caracteresPossiveis[indiceAleatorio];
            }

            // Converta a sequência em uma string
            string Token = new string(sequenciaAleatoria);

            return Token;
        }

    }
}

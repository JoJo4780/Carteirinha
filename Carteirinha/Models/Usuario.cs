using System.ComponentModel;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace  Carteirinha.Models

{
    public class Usuario
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o apelido")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Digite o Gmail")]
        public string Gmail { get; set; }
        [Required(ErrorMessage = "Digite a senha, ela deve possuir mais de 8 digitos")]
        [MinLength(8, ErrorMessage = "Digite a senha, ela deve possuir mais de 8 digitos")]
        public string Senha { get; set; }
        public int Login { get; set; } = 0;

    }
}

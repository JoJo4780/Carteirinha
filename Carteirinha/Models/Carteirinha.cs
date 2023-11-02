using System.ComponentModel.DataAnnotations.Schema;

namespace Carteirinha.Models
{
    public class Carteirinha
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Idade {get; set;}
        public string Idolo { get; set;}
        [ForeignKey("Usuario")]
        public int Usuario_Id { get; set; }
        public Usuario? Usuario { get; set; }
    }
}

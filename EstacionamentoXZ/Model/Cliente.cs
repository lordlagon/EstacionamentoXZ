using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.Model
{
    [Table("Clientes")]
    class Cliente
    {
        //Chave primária auto
        //public int Id { get; set; }
        //public int PessoaId { get; set; }
        //Manual
        [Key] //somente para int 
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "O campo nome deve ser preenchido!")] //Campo requerido
        [MaxLength(70, ErrorMessage = "O Campo deve conter o máximo de 70 caracteres!")] // MaxLength maximo de caracteres, somente para strings
        public string Nome { get; set; }
        [MaxLength(11, ErrorMessage = "O Campo deve conter o apenas 11 caracteres!")] // MaxLength maximo de caracteres, somente para strings
        [MinLength(11, ErrorMessage = "O Campo deve conter o apenas 11 caracteres!")] // MaxLength maximo de caracteres, somente para strings
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        //List<Carro>
    }
}





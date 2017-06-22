using System;
using System.Collections.Generic;
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
        
        [Required(ErrorMessage="O campo nome deve ser preenchido!")] //Campo requerido
        [MaxLength(70, ErrorMessage="O Campo deve conter o máximo de 70 caracteres!")] // MaxLength maximo de caracteres, somente para strings
        [MinLength(5, ErrorMessage = "O Campo deve conter no minimo de 5 caracteres!")] // MinLength minimo de caracteres, somente para strings
        public string Nome { get; set; }
        
        //[Range(typeof(DateTime), "1/2/2004", "3/4/2004", ErrorMessage="Data entre dia 01/02/2004 a 03/04/2004")]
        public DateTime DataDeNascimento { get; set; }
        
        [Required(ErrorMessage = "O campo nome deve ser preenchido!")]
        public string Cpf { get; set; }
        
        [Range(937, 10000, ErrorMessage="O Sálario deve estar entre R$ 937,00 e R$ 10000,00")]
        public double Salario { get; set; }
        
        public string Telefone { get; set; }
    }


    }


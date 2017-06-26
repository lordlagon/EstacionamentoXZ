using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.Model
{
    [Table("Carros")]
    class Carro
    {
     
        [Key] 
        public int CarroId { get; set; }
        [Required(ErrorMessage = "O campo nome deve ser preenchido!")] 
        public string ModeloCarro { get; set; }
        [MaxLength(8, ErrorMessage = "A Placa do Carro deve conter o máximo de 8 caracteres!")] 
        public string PlacaCarro { get; set; }
        //public string status { get; set; }
        public Cliente Cliente { get; set; }
        
    }

}


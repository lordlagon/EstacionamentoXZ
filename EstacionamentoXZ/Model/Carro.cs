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
        [Required(ErrorMessage = "O campo MODELO obrigatório!")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = "O campo PLACA obrigatório!")]
        public string Placa { get; set; }
        public Cliente Cliente { get; set; }
        public bool EstaEstacionado { get; set; }

        public Carro()
        {
            Cliente = new Cliente();
        }



    }

}


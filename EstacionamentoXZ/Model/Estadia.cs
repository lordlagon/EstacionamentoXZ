using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.Model
{
    [Table("Estadias")]
    class Estadia
    {
        [Key]
        public int EstadiaId { get; set; }
        public Carro Carro { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }

        public Estadia()
        {
            Carro = new Carro();
        }
    }
}

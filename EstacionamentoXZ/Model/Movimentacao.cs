using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.Model
{
    [Table("Movimentacoes")]
    class Movimentacao
    {
        [Key]
        public int MovimentacaoId { get; set; }
        public Cliente Cliente { get; set; }
        public Carro Carro { get; set; }
        public string Status { get; set; }
        public DateTime DataDeEntrada { get; set; }
        public DateTime DataDeSaida { get; set; }
    }
}

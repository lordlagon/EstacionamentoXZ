using System;
using System.Collections.Generic;
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
        public Conta Conta { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }
        public DateTime DataDaMovimentacao { get; set; }
    }
}

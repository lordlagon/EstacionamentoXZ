using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.DAL
{
    class Context
    {  /// <summary>
        /// Representar o banco de dados no projeto.
        /// Mapear todas as classes que vão virar tabelas no banco
        /// </summary>
        class Context : DbContext
        {
            //Mapeando a classe pessoa para virar uma tabela no banco
            public DbSet<Pessoa> Pessoas { get; set; }
        }
    }
}

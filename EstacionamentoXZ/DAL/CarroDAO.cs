using EstacionamentoXZ.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.DAL
{
    class CarroDAO
    {
        private static Context ctx = Singleton.Instance.Context;
        /// <summary>
        /// <para>asdasd</para>
        /// <para></para>
        /// </summary>
        /// <param name="carro"></param>
        /// <returns></returns>
        public static bool AdicionarCarro(Carro carro)
        {
            try
            {
                ctx.Carros.Add(carro);
                ctx.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException errosDeValidacao)
            {
                //Percorrer as propriedades do modelo que está sendo salvo
                foreach (DbEntityValidationResult resultadoDaValidacao in errosDeValidacao.EntityValidationErrors)
                {
                    //Percorrer os erros de validação de cada propriedade
                    foreach (DbValidationError erro in resultadoDaValidacao.ValidationErrors)
                    {
                        throw new Exception(erro.ErrorMessage);
                    }
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Banco de dados diferente do modelo!");
            }
        }


        public static List<Carro> RetornarCarros()
        {
            return ctx.Carros.ToList();
        }

        public static Carro BuscarCarroPorPlaca(Carro carro)
        {
            return ctx.Carros.FirstOrDefault(x => x.PlacaCarro.Equals(carro.PlacaCarro));
            //return ctx.Pessoas.SingleOrDefault(x => x.Cpf.Equals(pessoa.Cpf));
        }

        public static List<Carro> BuscarCarrosPorCpfDoCliente(Carro carro)
        {
            return ctx.Carros.Where(x => x.Cliente.Cpf.Contains(carro.Cliente.Cpf)).ToList();
        }

        public static bool AlterarCarro(Carro carro)
        {
            try
            {
                ctx.Entry(carro).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}



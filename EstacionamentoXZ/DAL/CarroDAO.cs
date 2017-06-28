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

        public static bool AdicionarCarro(Carro carro)
        {
            if (BuscarCarroPorPlaca(carro) != null)
            {
                return false;
            }
            try
            {
                ctx.Carros.Add(carro);
                ctx.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException errosDeValidacao)
            {
                //Percorrer as propriedades do modelo que está sendo salvo
                foreach (DbEntityValidationResult resultadoDaValidacao in
                    errosDeValidacao.EntityValidationErrors)
                {
                    //Percorrer os erros de validação de cada propriedade
                    foreach (DbValidationError erro in
                        resultadoDaValidacao.ValidationErrors)
                    {
                        ctx.Entry(carro).State = System.Data.Entity.EntityState.Detached;
                        ctx.SaveChanges();
                        throw new Exception(erro.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public static Carro BuscarCarroPorPlaca(Carro carro)
        {
            return ctx.Carros.Include("Cliente").FirstOrDefault(x => x.Placa.Equals(carro.Placa));
            //return ctx.Pessoas.SingleOrDefault(x => x.Cpf.Equals(pessoa.Cpf));
        }
    }
}



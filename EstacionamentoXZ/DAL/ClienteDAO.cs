using EstacionamentoXZ.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.DAL
{
    class ClienteDAO
    {

        private static Context ctx = Singleton.Instance.Context;

        public static bool AdicionarCliente(Cliente cliente)
        {
            if (BuscarClientePorCPF(cliente) != null)
            {
                return false;
            }
            try
            {
                ctx.Clientes.Add(cliente);
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
                        ctx.Entry(cliente).State = System.Data.Entity.EntityState.Detached;
                        ctx.SaveChanges();
                        throw new Exception(erro.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public static Cliente BuscarClientePorCPF(Cliente cliente)
        {
            return ctx.Clientes.FirstOrDefault(x => x.Cpf.Equals(cliente.Cpf));
        }
    }
}


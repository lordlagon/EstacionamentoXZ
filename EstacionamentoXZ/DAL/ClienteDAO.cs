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
        /// <summary>
        /// <para>asdasd</para>
        /// <para></para>
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public static bool AdicionarCliente(Cliente cliente)
        {
            try
            {
                ctx.Clientes.Add(cliente);
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


        public static List<Cliente> RetornarClientes()
        {
            return ctx.Clientes.ToList();
        }

        public static Cliente BuscarClientePorCPF(Cliente cliente)
        {
            return ctx.Clientes.FirstOrDefault(x => x.Cpf.Equals(cliente.Cpf));
            //return ctx.Pessoas.SingleOrDefault(x => x.Cpf.Equals(pessoa.Cpf));
        }

        public static Cliente BuscarClientePorPK(Cliente cliente)
        {
            return ctx.Clientes.Find(cliente.ClienteId);
        }

        public static List<Cliente> BuscarPessoasPorParteDoNome(Cliente cliente)
        {
            return ctx.Clientes.Where(x => x.NomeCliente.Contains(cliente.NomeCliente)).ToList();
        }

        public static bool RemoverPessoa(Cliente cliente)
        {
            try
            {
                ctx.Clientes.Remove(cliente);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex: " + ex.Message);
                return false;
            }
        }

        public static bool AlterarPessoa(Cliente cliente)
        {
            try
            {
                ctx.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
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


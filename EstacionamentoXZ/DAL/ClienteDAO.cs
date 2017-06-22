using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.DAL
{
    class ClienteDAO
    {
        
        private static Context ctx = new Context();
        /// <summary>
        /// <para>asdasd</para>
        /// <para></para>
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        public static bool AdicionarPessoa(Pessoa pessoa)
        {
            try
            {
                ctx.Pessoas.Add(pessoa);
                ctx.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException errosDeValidacao)
            {   //Percorre as propriedades os erros de validação 
                foreach (DbEntityValidationResult resultadoDaValidacao in errosDeValidacao.EntityValidationErrors)
                {
                    foreach (DbValidationError erro in resultadoDaValidacao.ValidationErrors)
                    {
                        ctx.Entry(pessoa).State = System.Data.Entity.EntityState.Detached;
                        ctx.SaveChanges();
                        throw new Exception(erro.ErrorMessage);
                    }   
                }
                return false;
            }
            catch(InvalidOperationException)
            {
                throw new Exception("Banco de Dados diferente do Modelo");
            }
        }

        public static List<Pessoa> RetornarPessoas()
        {
            return ctx.Pessoas.ToList();
        }

        public static Pessoa BuscarPessoaPorCPF(Pessoa pessoa)
        {
            return ctx.Pessoas.FirstOrDefault(x => x.Cpf.Equals(pessoa.Cpf));
            //return ctx.Pessoas.SingleOrDefault(x => x.Cpf.Equals(pessoa.Cpf));
        }

        public static Pessoa BuscarPessoaPorPK(Pessoa pessoa)
        {
            return ctx.Pessoas.Find(pessoa.PessoaId);
        }

        public static List<Pessoa> BuscarPessoasPorParteDoNome(Pessoa pessoa)
        {
            return ctx.Pessoas.Where(x => x.Nome.Contains(pessoa.Nome)).ToList();
        }

        public static bool RemoverPessoa(Pessoa pessoa)
        {
            try
            {
                ctx.Pessoas.Remove(pessoa);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex: " + ex.Message);
                return false;
            }
        }

        public static bool AlterarPessoa(Pessoa pessoa)
        {
            try
            {
                ctx.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
   
using EstacionamentoXZ.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.DAL
{
    class EstadiaDAO
    {
        private static Context ctx = Singleton.Instance.Context;

        public static bool AdicionarEstadia(Estadia estadia)
        {
            try
            {
                ctx.Estadias.Add(estadia);
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
                        ctx.Entry(estadia).State = System.Data.Entity.EntityState.Detached;
                        ctx.SaveChanges();
                        throw new Exception(erro.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public static Estadia BuscarEstadiaDeCarroEstacionado(Carro carro)
        {
            return ctx.Estadias.Include("Carro").Include("Carro.Cliente").FirstOrDefault(x => x.Carro.Placa.Equals(carro.Placa) && x.Carro.EstaEstacionado == true);
        }

        public static bool AlterarEstadia(Estadia estadia)
        {
            try
            {
                ctx.Entry(estadia).State = System.Data.Entity.EntityState.Modified;
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
                        ctx.Entry(estadia).State = System.Data.Entity.EntityState.Detached;
                        ctx.SaveChanges();
                        throw new Exception(erro.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public static List<Estadia> BuscarEstadiasPorCliente(Cliente cliente)
        {
            return ctx.Estadias.Include("Carro").Include("Carro.Cliente").Where(x => x.Carro.Cliente.Cpf.Equals(cliente.Cpf)).ToList();
        }

        public static List<Estadia> BuscarEstadiasPorData(DateTime data)
        {
            //return ctx.Estadias.Include("Carro").Include("Carro.Dono").Where(x => x.Entrada.Date == data.Date).ToList();
            return ctx.Estadias.Include("Carro").Include("Carro.Cliente").Where(x => x.Entrada.Day == data.Day && x.Entrada.Month == data.Month && x.Entrada.Year == data.Year).ToList();
        }

    }
}

  

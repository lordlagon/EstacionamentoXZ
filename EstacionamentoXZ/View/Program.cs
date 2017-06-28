using EstacionamentoXZ.DAL;
using EstacionamentoXZ.Model;
using EstacionamentoXZ.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.View
{
    class Program
    {
        static void Main(string[] args)
        {
            string op;
            Cliente cliente = new Cliente();
            Carro carro = new Carro();
            Estadia estadia = new Estadia();
            double precoDaEstadia, precoTotal;
            do
            {
                Console.Clear();
                Console.WriteLine("  -- Estacionamento XZPark  --  \n");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 – Cadastrar Carro para um Cliente");
                Console.WriteLine("3 – Entrada de Carro");
                Console.WriteLine("4 – Saída de Carro");
                Console.WriteLine("5 – Histórico de Movimentação do Cliente");
                Console.WriteLine("6 – Histórico de Movimentação do Dia");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("\nEscolha una opção:");
                op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        cliente = new Cliente();
                        Console.Clear();
                        Console.WriteLine(" -- CADASTRO DE CLIENTE -- ");
                        Console.WriteLine("Digite o nome do cliente:");
                        cliente.Nome = Console.ReadLine();
                        Console.WriteLine("Digite o CPF do cliente:");
                        cliente.Cpf = Console.ReadLine();
                        Console.WriteLine("Digite a data de Nascimento do Cliente:");
                        cliente.DataDeNascimento = Convert.ToDateTime(Console.ReadLine());
                        try
                        {
                            if (ClienteDAO.AdicionarCliente(cliente))
                            {
                                Console.WriteLine("Conta adicionada com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Conta já existente!");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "2":
                        carro = new Carro();
                        cliente = new Cliente();
                        Console.Clear();
                        Console.WriteLine(" -- CADASTRAR CARRO -- ");
                        Console.WriteLine("Digite o CPF do Cliente:");
                        cliente.Cpf = Console.ReadLine();
                        cliente = ClienteDAO.BuscarClientePorCPF(cliente);
                        try
                        {

                            if (cliente != null)
                            {
                                carro.Cliente = cliente;
                                Console.WriteLine("Digite a Placa do Carro: ");
                                carro.Placa = Console.ReadLine();
                                Console.WriteLine("Digite o Modelo do Carro: ");
                                carro.Modelo = Console.ReadLine();
                                CarroDAO.AdicionarCarro(carro);
                            }
                            else
                            {
                                Console.WriteLine("Cliente não encontrado!");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case "3":
                        estadia = new Estadia();
                        carro = new Carro();
                        Console.Clear();
                        Console.WriteLine(" -- ENTRADA DO CARRO -- \n");
                        Console.WriteLine("Digite a placa do carro:");
                        carro.Placa = Console.ReadLine();
                        carro = CarroDAO.BuscarCarroPorPlaca(carro);
                        if (carro != null && carro.EstaEstacionado == false)
                        {
                            estadia.Carro = carro;
                            estadia.Entrada = DateTime.Now;
                            //Lembre-se que o SQL Server não trabalha com datas menores que 1753
                            //Por isso defini uma data padrão
                            estadia.Saida = Convert.ToDateTime("01/01/1900");
                            //Modificando o status do carro
                            carro.EstaEstacionado = true;
                            try
                            {

                                if (EstadiaDAO.AdicionarEstadia(estadia))
                                {
                                    Console.WriteLine("Entrada efetuada com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Erro ao registrar entrada do carro!");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Carro não cadastrado ou já se encontra no estacionamento!");
                        }
                        break;
                    case "4":
                        estadia = new Estadia();
                        carro = new Carro();
                        Console.Clear();
                        Console.WriteLine(" -- SAÍDA DO CARRO -- \n");
                        Console.WriteLine("Digite a placa do carro:");
                        carro.Placa = Console.ReadLine();
                        estadia = EstadiaDAO.BuscarEstadiaDeCarroEstacionado(carro);
                        if (estadia != null)
                        {
                            estadia.Saida = DateTime.Now;
                            //Modificando o status do carro
                            estadia.Carro.EstaEstacionado = false;
                            try
                            {
                                if (EstadiaDAO.AlterarEstadia(estadia))
                                {
                                    precoDaEstadia = Calculos.CalcularEstadia(estadia);
                                    Console.WriteLine("Preço total: " + precoDaEstadia.ToString("C2"));
                                    Console.WriteLine("Saída efetuada com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Erro ao registrar a saída do carro!");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Carro não se encontra no estacionamento!");
                        }
                        break;
                    case "5":
                        cliente = new Cliente();
                        precoTotal = 0;
                        Console.Clear();
                        Console.WriteLine(" -- LISTAR ESTADIAS POR CLIENTE -- \n");
                        Console.WriteLine("Digite CPF do cliente:");
                        cliente.Cpf = Console.ReadLine();
                        cliente = ClienteDAO.BuscarClientePorCPF(cliente);
                        if (cliente != null)
                        {
                            Console.WriteLine("\nNome: " + cliente.Nome + "\n");
                            foreach (Estadia estadiaCadastrada
                                in EstadiaDAO.BuscarEstadiasPorCliente(cliente))
                            {
                                Console.WriteLine("\tCarro: " + estadiaCadastrada.Carro.Placa);
                                Console.WriteLine("\tModelo: " + estadiaCadastrada.Carro.Modelo);
                                Console.WriteLine("\tEntrada: " + estadiaCadastrada.Entrada);
                                Console.WriteLine("\tSaída: " + estadiaCadastrada.Saida);
                                precoDaEstadia = Calculos.CalcularEstadia(estadiaCadastrada);
                                Console.WriteLine("\tPreço total: " + precoDaEstadia.ToString("C2") + "\n");
                                precoTotal += precoDaEstadia;
                            }
                            Console.WriteLine("Total: " + precoTotal.ToString("C2"));
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado!");
                        }
                        break;
                    case "6":
                        precoTotal = 0;
                        Console.Clear();
                        Console.WriteLine(" -- LISTAR ESTADIAS POR DATA -- \n");
                        Console.WriteLine("\nDigite uma data: " + cliente.Nome);
                        DateTime data = Convert.ToDateTime(Console.ReadLine());
                        foreach (Estadia estadiaCadastrada in EstadiaDAO.BuscarEstadiasPorData(data))
                        {
                            Console.WriteLine("\tCliente: " + estadiaCadastrada.Carro.Cliente.Nome);
                            Console.WriteLine("\tCarro: " + estadiaCadastrada.Carro.Placa);
                            Console.WriteLine("\tModelo: " + estadiaCadastrada.Carro.Modelo);
                            Console.WriteLine("\tEntrada: " + estadiaCadastrada.Entrada);
                            Console.WriteLine("\tSaída: " + estadiaCadastrada.Saida);
                            precoDaEstadia = Calculos.CalcularEstadia(estadiaCadastrada);
                            Console.WriteLine("\tPreço total: " + precoDaEstadia.ToString("C2") + "\n");
                            precoTotal += precoDaEstadia;
                        }
                        Console.WriteLine("Total: " + precoTotal.ToString("C2"));
                        break;
                    case "0":
                        Console.WriteLine("\nSaindo...");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida!");
                        break;
                }
                Console.WriteLine("\nPressione uma tecla para continuar...");
                Console.ReadKey();
            } while (!op.Equals("0"));
        }
    }
}
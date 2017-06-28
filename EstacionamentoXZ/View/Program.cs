using EstacionamentoXZ.DAL;
using EstacionamentoXZ.Model;
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
            Cliente cliente = new Cliente();
            Carro carro = new Carro();
            string op;
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
                        cliente.NomeCliente = Console.ReadLine();
                        Console.WriteLine("Digite o CPF do cliente:");
                        cliente.Cpf = Console.ReadLine();
                        Console.WriteLine("Digite a data de Nascimento do Cliente:");
                        cliente.DataDeNascimento = Convert.ToDateTime(Console.ReadLine());
                        
                        if (ClienteDAO.AdicionarCliente(cliente))
                        {
                            Console.WriteLine("Conta adicionada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Conta já existente!");
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
                        if (cliente != null)
                        {
                            carro.Cliente(cliente);
                            Console.WriteLine("Digite a Placa do Carro: ");
                            carro.PlacaCarro = Console.ReadLine();
                            Console.WriteLine("Digite o Modelo do Carro: ");
                            carro.ModeloCarro = Console.ReadLine();
                            
                            CarroDAO.AdicionarCarro(carro);
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado!");
                        }

                        break;

                    /*case "3":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- DEPÓSITO -- ");
                        Console.WriteLine("Digite o número da conta:");
                        conta.Numero = Console.ReadLine();
                        conta = ContaDAO.BuscarContaPorNumero(conta);
                        if (conta != null)
                        {
                            Console.WriteLine("Digite o valor do depósito:");
                            double deposito = Convert.ToDouble(Console.ReadLine());
                            MovimentacoesBancarias.Depositar(conta, deposito);
                            Console.WriteLine("Depósito efetuado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }
                        break;
                    case "4":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- SAQUE -- ");
                        Console.WriteLine("Digite o número da conta:");
                        conta.Numero = Console.ReadLine();
                        conta = ContaDAO.BuscarContaPorNumero(conta);
                        if (conta != null)
                        {
                            Console.WriteLine("Digite o valor do saque:");
                            double saque = Convert.ToDouble(Console.ReadLine());
                            if (MovimentacoesBancarias.Sacar(conta, saque))
                            {
                                Console.WriteLine("Saque efetuado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Valor insuficiente!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }
                        break;

                    case "5":
                        Conta contaSaque = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- TRANSFERENCIA -- ");
                        Console.WriteLine("Digite o número da conta:");
                        contaSaque.Numero = Console.ReadLine();
                        contaSaque = ContaDAO.BuscarContaPorNumero(contaSaque);
                        if (contaSaque != null)
                        {
                            Conta contaDeposito = new Conta();
                            Console.WriteLine("Digite o número da conta para a transferencia:");
                            contaDeposito.Numero = Console.ReadLine();
                            contaDeposito = ContaDAO.BuscarContaPorNumero(contaDeposito);
                            if (contaDeposito != null)
                            {
                                if (contaDeposito.Numero != contaSaque.Numero)
                                {
                                    Console.WriteLine("Digite o valor da transferencia:");
                                    double transferencia = Convert.ToDouble(Console.ReadLine());
                                    if (MovimentacoesBancarias.TransferenciaSacar(contaSaque, transferencia))
                                    {
                                        MovimentacoesBancarias.TransferenciaDepositar(contaDeposito, transferencia);
                                        Console.WriteLine("Transferencia efetuada com sucesso!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Valor insuficiente!");
                                    }
                                }
                                else { Console.WriteLine("Não é possível fazer transferencia para a mesma conta"); }
                            }
                            else
                            {
                                Console.WriteLine("Conta não encontrada!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }

                        break;

                    case "6":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- Extrato -- ");
                        Console.WriteLine("Digite o número da conta:");
                        conta.Numero = Console.ReadLine();
                        conta = ContaDAO.BuscarContaPorNumero(conta);
                        if (conta != null)
                        {
                            Console.WriteLine("Número: " + conta.Numero);
                            Console.WriteLine("Cliente: " + conta.Cliente);
                            Console.WriteLine("Saldo: " + conta.Saldo.ToString("C2"));
                            Console.WriteLine("Data de Abertura: " + conta.DataDeAbertura);
                            List<Movimentacao> movimentacoes = MovimentacaoDAO.BuscarMovimentacoesPorConta(conta);
                            Console.WriteLine(" -- MOVIMENTAÇÕES SAQUES EFETUADOS -- ");
                            foreach (Movimentacao movimentacaoCadastrada in movimentacoes)
                            {
                                if (movimentacaoCadastrada.Tipo.Equals("Saque"))
                                {
                                    Console.WriteLine("\nTipo: " + movimentacaoCadastrada.Tipo);
                                    Console.WriteLine("Valor: " + movimentacaoCadastrada.Valor.ToString("C2"));
                                    Console.WriteLine("Data: " + movimentacaoCadastrada.DataDaMovimentacao);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }
                        break;
                    case "7":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- Extrato -- ");
                        Console.WriteLine("Digite o número da conta:");
                        conta.Numero = Console.ReadLine();
                        conta = ContaDAO.BuscarContaPorNumero(conta);
                        if (conta != null)
                        {
                            Console.WriteLine("Número: " + conta.Numero);
                            Console.WriteLine("Cliente: " + conta.Cliente);
                            Console.WriteLine("Saldo: " + conta.Saldo.ToString("C2"));
                            Console.WriteLine("Data de Abertura: " + conta.DataDeAbertura);
                            List<Movimentacao> movimentacoes = MovimentacaoDAO.BuscarMovimentacoesPorConta(conta);
                            Console.WriteLine(" -- MOVIMENTAÇÕES DEPÓSITOS EFETUADOS -- ");
                            foreach (Movimentacao movimentacaoCadastrada in movimentacoes)
                            {
                                if (movimentacaoCadastrada.Tipo.Equals("Depósito"))
                                {
                                    Console.WriteLine("\nTipo: " + movimentacaoCadastrada.Tipo);
                                    Console.WriteLine("Valor: " + movimentacaoCadastrada.Valor.ToString("C2"));
                                    Console.WriteLine("Data: " + movimentacaoCadastrada.DataDaMovimentacao);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }
                        break;
                    case "8":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- Extrato -- ");
                        Console.WriteLine("Digite o número da conta:");
                        conta.Numero = Console.ReadLine();
                        conta = ContaDAO.BuscarContaPorNumero(conta);
                        if (conta != null)
                        {
                            Console.WriteLine("Número: " + conta.Numero);
                            Console.WriteLine("Cliente: " + conta.Cliente);
                            Console.WriteLine("Saldo: " + conta.Saldo.ToString("C2"));
                            Console.WriteLine("Data de Abertura: " + conta.DataDeAbertura);
                            List<Movimentacao> movimentacoes = MovimentacaoDAO.BuscarMovimentacoesPorConta(conta);
                            Console.WriteLine(" -- MOVIMENTAÇÕES TRANSFERENCIA EFETUADAS -- ");
                            foreach (Movimentacao movimentacaoCadastrada in movimentacoes)
                            {
                                if (movimentacaoCadastrada.Tipo.Equals("Transferencia Retirada") || movimentacaoCadastrada.Tipo.Equals("Transferencia Deposito"))
                                {
                                    Console.WriteLine("\nTipo: " + movimentacaoCadastrada.Tipo);
                                    Console.WriteLine("Valor: " + movimentacaoCadastrada.Valor.ToString("C2"));
                                    Console.WriteLine("Data: " + movimentacaoCadastrada.DataDaMovimentacao);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }
                        break;
                    case "9":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- Extrato -- ");
                        Console.WriteLine("Digite o número da conta:");
                        conta.Numero = Console.ReadLine();
                        conta = ContaDAO.BuscarContaPorNumero(conta);
                        if (conta != null)
                        {
                            Console.WriteLine("Número: " + conta.Numero);
                            Console.WriteLine("Cliente: " + conta.Cliente);
                            Console.WriteLine("Saldo: " + conta.Saldo.ToString("C2"));
                            Console.WriteLine("Data de Abertura: " + conta.DataDeAbertura);
                            List<Movimentacao> movimentacoes = MovimentacaoDAO.BuscarMovimentacoesPorConta(conta);
                            Console.WriteLine(" -- TODAS AS MOVIMENTAÇÕES -- ");
                            foreach (Movimentacao movimentacaoCadastrada in movimentacoes)
                            {
                                Console.WriteLine("\nTipo: " + movimentacaoCadastrada.Tipo);
                                Console.WriteLine("Valor: " + movimentacaoCadastrada.Valor.ToString("C2"));
                                Console.WriteLine("Data: " + movimentacaoCadastrada.DataDaMovimentacao);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }
                        break;
            */


                    case "0":
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
                Console.WriteLine("\nPressione uma tecla para continuar...");
                Console.ReadKey();
            } while (!op.Equals("0"));

        }
    }
}

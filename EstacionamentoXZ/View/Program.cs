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
            Conta conta = new Conta();
            string op;
            do
            {
                Console.Clear();
                Console.WriteLine("  --  BANCO XZInvestimentos  --  \n");
                Console.WriteLine("1 - Cadastrar Conta");
                Console.WriteLine("2 - Remover Conta");
                Console.WriteLine("3 - Depósito");
                Console.WriteLine("4 - Saque");
                Console.WriteLine("5 - Transferencia");
                Console.WriteLine("6 - Extrato Saques ");
                Console.WriteLine("7 - Extrato Depositos ");
                Console.WriteLine("8 - Extrato Transferencias");
                Console.WriteLine("9 - Extrato Completo");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("\nEscolha una opção:");
                op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- CADASTRO DE CONTA -- ");
                        Console.WriteLine("Digite o número da conta:");
                        conta.Numero = Console.ReadLine();
                        Console.WriteLine("Digite o nome do cliente:");
                        conta.Cliente = Console.ReadLine();
                        Console.WriteLine("Digite o saldo inicial da conta:");
                        conta.Saldo = Convert.ToDouble(Console.ReadLine());
                        conta.DataDeAbertura = DateTime.Now;
                        if (ContaDAO.AdicionarConta(conta))
                        {
                            Console.WriteLine("Conta adicionada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Conta já existente!");
                        }

                        break;
                    case "2":
                        conta = new Conta();
                        Console.Clear();
                        Console.WriteLine(" -- REMOVER CONTA -- ");
                        Console.WriteLine("Digite o Numero da Conta:");
                        conta.Numero = Console.ReadLine();
                        conta = ContaDAO.BuscarContaPorNumero(conta);
                        if (conta != null)
                        {
                            Console.WriteLine("Numero da Conta: " + conta.Numero);
                            Console.WriteLine("Cliente: " + conta.Cliente);
                            Console.WriteLine("\nDeseja remover esta Conta?");
                            if (Console.ReadLine().ToLower().Equals("s"))
                            {
                                if (ContaDAO.RemoverConta(conta))
                                {
                                    Console.WriteLine("Conta removida com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Erro ao remover esta Conta!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Conta não removida!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Conta não encontrada!");
                        }

                        break;

                    case "3":
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

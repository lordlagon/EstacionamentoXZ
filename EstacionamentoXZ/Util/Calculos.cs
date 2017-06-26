using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoXZ.Util
{
    class Calculos
    {
       /* public static void Depositar(Conta conta, double valor)
        {
            conta.Saldo += valor;
            Movimentacao movimentacao = new Movimentacao();
            movimentacao.Conta = conta;
            movimentacao.Valor = valor;
            movimentacao.Tipo = "Depósito";
            movimentacao.DataDaMovimentacao = DateTime.Now;
            MovimentacaoDAO.AdicionarMovimentacao(movimentacao);
        }

        public static bool Sacar(Conta conta, double valor)
        {
            if (conta.Saldo >= valor)
            {
                conta.Saldo -= valor;
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Conta = conta;
                movimentacao.Valor = valor;
                movimentacao.Tipo = "Saque";
                movimentacao.DataDaMovimentacao = DateTime.Now;
                MovimentacaoDAO.AdicionarMovimentacao(movimentacao);
                return true;
            }
            return false;
        }

        public static bool TransferenciaSacar(Conta conta, double valor)
        {
            if (conta.Saldo >= valor)
            {
                conta.Saldo -= valor;
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Conta = conta;
                movimentacao.Valor = valor;
                movimentacao.Tipo = "Transferencia Retirada";
                movimentacao.DataDaMovimentacao = DateTime.Now;
                MovimentacaoDAO.AdicionarMovimentacao(movimentacao);
                return true;
            }
            return false;
        }

        public static void TransferenciaDepositar(Conta conta, double valor)
        {
            conta.Saldo += valor;
            Movimentacao movimentacao = new Movimentacao();
            movimentacao.Conta = conta;
            movimentacao.Valor = valor;
            movimentacao.Tipo = "Transferencia Deposito";
            movimentacao.DataDaMovimentacao = DateTime.Now;
            MovimentacaoDAO.AdicionarMovimentacao(movimentacao);
        }
        
*/    }
    }


using System;
using System.Text.RegularExpressions;

namespace DioProjeto
{
    public class Conta
    {
        private TipoConta TipoConta { get; set; }
        private double Saldo { get; set; }
        private double Divida { get; set; }
        private string Nome { get; set; }
        private string Senha { get; set; }

        public Conta(TipoConta tipoConta, double saldo, string nome, string senha)
        {

            this.TipoConta = tipoConta;
            this.Saldo = saldo;
            this.Nome = nome;
            this.Divida = 0;
            this.Senha = senha;

        }
 
        public bool Sacar(double ValorSaque)
        {
            if(this.Saldo - ValorSaque < 0 )
            {
                Console.WriteLine("Saldo Insuficiente!");
                return false;
            }

            this.Saldo -= ValorSaque;
            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);
            return true;
        }

        public void Depositar(double valorDeposito)
        {
            if(Divida > 0)
            {

                if(Divida > valorDeposito)
                {   
                    Divida -= valorDeposito;
                }
                else
                {
                    Saldo += (valorDeposito - Divida);
                    Divida = 0;
                }
            }
            else
            {
                this.Saldo += valorDeposito;
                Console.WriteLine("Saldo atual de: {0} é R${1}", this.Nome, this.Saldo);
                Console.WriteLine("Saldo Devedor: R$" + Divida);
            }

        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if (this.Sacar(valorTransferencia)) 
            {
                contaDestino.Depositar(valorTransferencia);
            }
        }

        public void Emprestimo(double valor) 
        {
            this.Depositar(valor);
            if((int)TipoConta == 1)
            {
                this.Divida += (valor + (valor * 0.18));
            }
            else
            {   
                this.Divida += (valor + (valor * 0.10));
            }
        
        }

        public void Extrato()
        {
            Console.WriteLine(
                " -- EXTRATO --" +
                "Titular: "+ Nome +"\n"+
                "Saldo: R$"+Saldo +"\n"+
                "Saldo Devedor: R$"+ Divida +"\n"+
                "Crédito: R$"+ CreditoDisponivel()
            );
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "TipoConta >> " + this.TipoConta + "\n";
            retorno += "Nome >> " + this.Nome + "\n";
            retorno += "Saldo >> R$" + this.Saldo + "\n";
            retorno += "Divida >> R$" + this.Divida + "\n";
            retorno += "Crédito >> R$" + CreditoDisponivel() + "\n";
            retorno += "Senha >>" + this.Senha + "\n";
            return retorno;
        }

        public bool VerificaSenha(string senha)
        {
            bool resposta = false;
            if (this.Senha == senha)
            {
                resposta = true;
            }
            return resposta;
        }

        public double CreditoDisponivel()
        {
            double resposta = 0.0;
            if( !(Divida > 0) )
            {
                resposta = ((int)TipoConta == 1) ? Saldo * 0.15 : Saldo * 0.40;
            }
            return resposta;
        }

    }
}
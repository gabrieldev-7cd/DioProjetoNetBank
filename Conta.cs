using System;

namespace DioProjeto
{
    public class Conta
    {

        private TipoConta TipoConta { get; set; }
        private double Saldo { get; set; }
        private double Credito { get; set; }
        private string Nome { get; set; }

        public Conta(TipoConta tipoConta, double saldo, double credito, string nome) 
        {

            this.TipoConta = tipoConta;
            this.Saldo = saldo;
            this.Credito = credito;
            this.Nome = nome;

        }

        public bool Sacar(double ValorSaque)
        {
            if(this.Saldo - ValorSaque < (this.Credito * -1))
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
            this.Saldo += valorDeposito;
            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);

        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if (this.Sacar(valorTransferencia)) 
            {
                contaDestino.Depositar(valorTransferencia);
            }
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "TipoConta >> " + this.TipoConta + "\n";
            retorno += "Nome >> " + this.Nome + "\n";
            retorno += "Saldo >> " + this.Saldo + "\n";
            retorno += "Crédito >> " + this.Credito + "\n";
            return retorno;
        }

    }
}
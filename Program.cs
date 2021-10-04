using System;

namespace DioProjeto
{
    class Program
    {
        static void Main(string[] args)
        {
            Conta minhaConta = new Conta(TipoConta.PessoaFisica, 0, 0, "Gabriel Alexandre");
          
            Console.WriteLine(minhaConta.ToString());

           //Eliezer
        }
    }
}

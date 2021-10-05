using System;
using System.Collections.Generic;

namespace DioProjeto
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();

        static void Main(string[] args)
        {
            //Conta minhaConta = new Conta(TipoConta.PessoaFisica, 0, 0, "Gabriel Alexandre");
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        //ListarContas();
                        break;
                    case "2":
                        //InserirConta();
                        break;
                    case "3":
                        //Transferir();
                        break;
                    case "4":
                        //Sacar();
                        break;
                    case "5":
                        //Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();

                }
                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();

           //Console.WriteLine(minhaConta.ToString());
           //Eliezer
        }

        private static void InserirConta()
        {
            Console.WriteLine("Inserir nova conta");

            Console.WriteLine("Digite 1 para conta Física ou 2 para Jurídica: ");
            int entradaTipoConta = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Nome do Cliente: ");
            string entradaNome = Console.ReadLine();

            Console.WriteLine("Digite o saldo inicial: ");
            double entradaSalario = double.Parse(Console.ReadLine());

            Console.Write("Digite o crédito ");
            double entradaCredito = double.Parse(Console.ReadLine());

            Conta novaConta = new Conta
            (
                tipoConta: (TipoConta)entradaTipoConta,
                saldo: entradaSalario,
                credito: entradaCredito,
                nome: entradaNome
            );

            listContas.Add(novaConta);
        }

        private static void ListarContas()
        {
            Console.WriteLine("Listar Contas");

            if(listContas.Count == 0) 
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            for(int i = 0; i< listContas.Count; i++) 
            {
                Conta conta = listContas[i];
                Console.Write("#{0} - ", i);
                Console.WriteLine(conta);
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.Clear();
            Console.WriteLine("\n DIO BANK a seu Dispor!!!");
            Console.WriteLine("informe a opção desejada");
            Console.WriteLine("1 - Listar Contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacas");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair \n");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;

        }
    }
}

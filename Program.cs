using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DioProjeto
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
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
        }

        private static void Transferir()
        {
            Console.Write("Digite o número da conta de origem: ");
            int indiceContaOrigem = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da conta de destino: ");
            int indiceContaDestino = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser tranferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());

            listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
        }

        public static void Sacar()
        {
            Console.Write("Digite o número da conta:");
            int indiceConta = int.Parse(Console.ReadLine());
            
            Console.Write("Digite o valor a ser sacado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[indiceConta].Sacar(valorDeposito);

        }

        public static void Depositar()
        {
            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[indiceConta].Depositar(valorDeposito);
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

            string senhaUsuario;

            Console.WriteLine("\nDigite abaixo uma senha com os seguintes requesitos:\n " +
                "Conter 1 ou mais Letra Maiúscula; \n" +
                "Conter 1 ou mais Letra Minúscula; \n" +
                "Ter de 8 a 16 Caracteres; \n"
                );
            Console.Write("\n Digite a senha da Conta: ");
       
            while( ( ValidarSenhaConta(senhaUsuario = Console.ReadLine() )) == false)
            {

                Console.Write("\n A senha anterior não atende aos requesitos, tente novamente:");
            }

            Conta novaConta = new Conta
            (
                tipoConta: (TipoConta)entradaTipoConta,
                saldo: entradaSalario,
                credito: entradaCredito,
                nome: entradaNome,
                senha: senhaUsuario
            );

            listContas.Add(novaConta);
            Console.WriteLine("\n Conta criado com Sucesso!");
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
            //Console.Clear();
            Console.WriteLine("\n DIO BANK a seu Dispor!!!\n");
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

        public static bool ValidarSenhaConta(string senha)
        {
            if (senha != null && senha.Length > 0)
            {
                Regex reg = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$");
                bool resultado = (reg.IsMatch(senha) ? true : false);
                return resultado;
            }
            return false;
        }

    }
}

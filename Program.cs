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
                    case "6":
                        Emprestimo();
                        break;
                    case "7":
                        Extrato();
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

            Console.Write("\n insira a senha da conta de Origem: ");
            string senha = Console.ReadLine();

            if (VerificaSenha(indiceContaOrigem, senha))
            {
                Console.WriteLine("Ok Senha verificada continue com o processo!");

                Console.Write("Digite o número da conta de destino: ");
                int indiceContaDestino = int.Parse(Console.ReadLine());

                Console.Write("Digite o valor a ser tranferido: ");
                double valorTransferencia = double.Parse(Console.ReadLine());

                listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
            }
            else
            {
                Console.WriteLine("Senha Inválida por favor verifique os dados e tente novamente!");
            }
        }

        public static void Sacar()
        {
            Console.Write("Digite o número da conta:");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("\n insira a senha da conta de Origem: ");
            string senha = Console.ReadLine();

            if (VerificaSenha(indiceConta, senha))
            {
                Console.Write("Digite o valor a ser sacado: ");
                double valorDeposito = double.Parse(Console.ReadLine());

                listContas[indiceConta].Sacar(valorDeposito);
            }
            else
            {
                Console.WriteLine("Senha Inválida por favor verifique os dados e tente novamente!");
            }
        }

        public static void Depositar()
        {
            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[indiceConta].Depositar(valorDeposito);
        }
        
        public static void Emprestimo()
        {
            bool resposta = false;
            string message= "Por favor tente o processo novamente!";
            Console.WriteLine(
                "\nInformações sobre o emprestimo: \n \n" +
                "# Nós fornecemos Emprestimos de: \n \n" +
                "# 40% do valor da conta para Pessoas Jurídicas com taxa de 10% sobre o valor de emprestimo \n \n" +
                "# 15% do valor da conta para Pessoas Físicas com taxa de 18% sobre o valor de emprestimo \n \n" +
                "# Quando um Emprestimo for concedido só poderá ser feito um novo \n" +
                "mediante pagamento do saldo devedor. \n \n" +
                "#Todo o dinheiro que for recebido na conta será utilizado \n"+
                "para pagamento da divida \n \n"
            );

            Console.Write("Digite o numero da conta: ");
            int conta = int.Parse(Console.ReadLine());

            Console.Write("\n Entre com a senha da conta:");
            string senha = Console.ReadLine();
            double credito = 0.00, pedido;
            if (VerificaSenha(conta, senha))
            {
                credito = listContas[conta].CreditoDisponivel();
                Console.WriteLine("O valor máximo disponível é: R$" + credito);

                while (true)
                {
                    Console.Write("\n Entre com o valor desejado: ");
                    pedido = double.Parse(Console.ReadLine());

                    if (pedido > 0 && pedido <= credito)
                    {
                        resposta = true;
                        break;
                    }
                }

                if(resposta)
                {
                    listContas[conta].Emprestimo(pedido);
                    message = ("Emprestimo realizado com Sucesso!");
                }
            }
            Console.WriteLine(message);
        }

        public static void Extrato()
        {
            Console.Write("Digite o numero da conta: ");
            int conta = int.Parse(Console.ReadLine());

            Console.Write("\n Entre com a senha da conta:");
            string senha = Console.ReadLine();

            if (VerificaSenha(conta, senha))
            {
                listContas[conta].Extrato();
            }
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

            string senhaUsuario;
            Console.WriteLine("\n" +
                "Digite abaixo uma senha com os seguintes requesitos:\n " +
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
                nome: entradaNome,
                senha: senhaUsuario
            );

            listContas.Add(novaConta);
            Console.WriteLine("\n Conta criada com Sucesso!");
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
                Console.Write("Nª da Conta >> {0} \n ", i);
                Console.WriteLine(conta);
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("\nDIO BANK a seu Dispor!!!\n");
            Console.WriteLine("informe a opção desejada");
            Console.WriteLine("1 - Listar Contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("6 - Emprestimo");
            Console.WriteLine("7 - Extrato");
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

        public static bool VerificaSenha(int conta, string senha)
        {
            bool resposta = false;
            if ( listContas[conta] != null )
            {
                resposta = (listContas[conta].VerificaSenha(senha)) ? true : false;
            }
            else
            {
                Console.WriteLine("Tentou verificar senha e não conseguiu verificar o objeto de conta!");
            }

            return resposta;
        }
    }
}

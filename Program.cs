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
                        Console.WriteLine("Nenhuma opção válida foi selecionada, tente novamente!");
                        break;
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por utilizar nossos serviços.");
        }

        private static void Transferir()
        {
            Console.Write("Digite o número da conta de origem: ");
            int indiceContaOrigem = GetCodigoConta();

            Console.Write("\n insira a senha da conta de Origem: ");
            string senha = GetEntradaString();

            if (VerificaSenha(indiceContaOrigem, senha))
            {
                Console.WriteLine("Ok Senha verificada continue com o processo!");
                Console.Write("\nDigite o número da conta de destino: ");
                int indiceContaDestino = GetCodigoConta();

                Console.Write("\nDigite o valor a ser tranferido: ");
                double valorTransferencia = GetEntradaDouble();

                listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
            }
            else
            {
                Console.WriteLine("\nSenha Inválida por favor verifique os dados e tente novamente!");
            }
        }

        public static void Sacar()
        {
            Console.Write("\nDigite o número da conta:");
            int indiceConta = GetCodigoConta();
         
            Console.Write("\ninsira a senha da conta de Origem: ");
            string senha = GetEntradaString();

            if (VerificaSenha(indiceConta, senha))
            {
                Console.Write("\nDigite o valor a ser sacado: ");
                double valorDeposito = GetEntradaDouble();

                listContas[indiceConta].Sacar(valorDeposito);
            }
            else
            {
                Console.WriteLine("\nSenha Inválida por favor verifique os dados e tente novamente!");
            }
        }

        public static void Depositar()
        {
            Console.Write("\nDigite o código da conta: ");
            int indiceConta = GetCodigoConta();
          
            Console.Write("\nDigite o valor a ser depositado: ");
            double valorDeposito = GetEntradaDouble();

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
            int conta = GetCodigoConta();
           
            Console.Write("\n Entre com a senha da conta:");
            string senha = GetEntradaString();
            double credito = 0.00, pedido;
            if (VerificaSenha(conta, senha))
            {
                credito = listContas[conta].CreditoDisponivel();
                Console.WriteLine("O valor máximo disponível é: R$" + credito.ToString("N2"));

                while (true)
                {
                    Console.Write("\n Entre com o valor desejado: ");
                    pedido = GetEntradaDouble();

                    if (pedido > 0 && pedido <= credito)
                    {
                        resposta = true;
                        break;
                    }
                }

                if (resposta)
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
            int conta = GetCodigoConta();
            
            Console.Write("\n Entre com a senha da conta:");
            string senha = GetEntradaString();

            if (VerificaSenha(conta, senha))
            {
                listContas[conta].Extrato();
            }       
        }

        private static void InserirConta()
        {
            Console.WriteLine("-- Inserir nova conta --");
            Console.WriteLine("Digite 1 para conta Física ou 2 para Jurídica: ");
            int entradaTipoConta = 0;

            while(true)
            {   
                entradaTipoConta = int.Parse(Console.ReadLine());
                if( (entradaTipoConta > 0 && entradaTipoConta <= 2) )
                {
                    break;
                }
                Console.WriteLine("Digite um valor válido para o tipo de conta");
            }     

            Console.WriteLine("Digite o Nome do Cliente: ");
            string entradaNome = GetEntradaString();

            Console.WriteLine("Digite o saldo inicial: ");
            double entradaSalario = GetEntradaDouble();

            string senhaUsuario;
            Console.WriteLine("\n" +
                "Digite abaixo uma senha com os seguintes requesitos: \n" +
                "Conter 1 ou mais Letra Maiúscula; \n" +
                "Conter 1 ou mais Letra Minúscula; \n" +
                "Ter de 8 a 16 Caracteres; \n"
            );

            Console.Write("\n> Digite a senha da Conta: ");

            while ((ValidarSenhaConta(senhaUsuario = Console.ReadLine())) == false)
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
            Console.WriteLine("-- Listar Contas --\n");
            if(listContas.Count == 0) 
            {
                Console.WriteLine("\nNenhuma conta cadastrada.");
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

        public static int GetCodigoConta()
        {
            while (true)
            {
                try
                {
                    int codigo = int.Parse(Console.ReadLine());
                    if (listContas[codigo] != null)
                    {
                        return codigo;
                    }
                }
                catch (Exception )
                {
                    Console.WriteLine("Conta inexistente, por favor insira um Código válido");
                }
            }
        }

        public static string GetEntradaString()
        {
            string entrada;
            while (true)
            {
                try
                {
                    entrada = Console.ReadLine();
                    if ( entrada is string && entrada.Length >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("\nValor inválido por favor tente novamente!: ");
                    }
                }
                catch (Exception )
                {
                    Console.WriteLine("Erro tente novamente!");
                }
            }
            return entrada;
        }

        public static double GetEntradaDouble()
        {
            double entrada;
            while (true)
            {
                try
                {
                    entrada = double.Parse(Console.ReadLine());
                    if (entrada >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("\nValor inválido por favor tente novamente!: ");
                    }
                }catch( Exception )
                {
                    Console.WriteLine("Erro tente novamente");
                }
            }
            return entrada;
        }
    }
}

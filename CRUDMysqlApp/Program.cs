using System;
using System.Linq;

namespace CRUDMysqlApp
{
    class Program
    {
        public static CrudDemo app = new CrudDemo("localhost", "biblioteca", "validador", "12345");
        static void Main(string[] args)
        {
            app.TestConnection(); // Funcionando
            printMenu();
            string opcaoUsuario = Console.ReadLine().ToUpper();
            while (opcaoUsuario != "X")
            {
                executarEscolhaUsuario(opcaoUsuario);
                printMenu();
                opcaoUsuario = Console.ReadLine().ToUpper();
            }


            //CrudDemo app = new CrudDemo("localhost", "biblioteca", "validador", "12345");
            //app.TestConnection(); // Funcionando
            //app.CreateUserSP("Filipe", "Ibura", "Casa"); //Funcionando
            //app.CreateData(); // Funcionando
            //app.ReadData();//Funcionando
            //app.UpdateData(7); // change idproduct based on your data // Funcionando

            //app.ReadData(); //Funcionando
            //app.DeleteUserSP(); // change idproduct based on your data //Funcionando
            //app.ReadData();

            //app.BulkData();//Funcionando
        }

        public static void printMenu()
        {
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Bem Vindo ao Sistema de Gerenciamento de Usuários:");
            Console.WriteLine("Tecle 1 para Cadastrar um Usuário");
            Console.WriteLine("Tecle 2 para Alterar um Usuário");
            Console.WriteLine("Tecle 3 para Excluir um Usuário");
            Console.WriteLine("Tecle 4 para listar todos os Usuários");
            Console.WriteLine("Tecle X para Sair;");

        }

        public static void executarEscolhaUsuario(string escolhaUsuario)
        {
            switch (escolhaUsuario)
            {
                case "1":
                    Console.WriteLine("Digite o nome: ");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Digite o endereco: ");
                    string endereco = Console.ReadLine();
                    Console.WriteLine("Digite o garantia: ");
                    string garantia = Console.ReadLine();
                    app.CreateUserSP(nome, endereco, garantia);
                    break;
                case "2":
                    Console.WriteLine("Digite o id do Usuário a ser editado:");
                    int idUpdate = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Digite o nome: ");
                    string nomeUpdate = Console.ReadLine();
                    Console.WriteLine("Digite o endereco: ");
                    string enderecoUpdate = Console.ReadLine();
                    Console.WriteLine("Digite o garantia: ");
                    string garantiaUpdate = Console.ReadLine();
                    app.UpdateUserSP(idUpdate, nomeUpdate, enderecoUpdate, garantiaUpdate);
                    break;

                case "3":
                    Console.WriteLine("Digite o id do Usuário a ser excluído:");
                    int idDelete = Convert.ToInt32(Console.ReadLine());

                    app.DeleteUserSP(idDelete);
                    break;
                case "4":
                    app.ListarUsuariosSP();
                    break;
                default:
                    break;
            }
        }
    }
}

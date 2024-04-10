using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestaoDeClientesNoCmdProj2
{
    class Program
    {
        // Permite salvar os dados dentro de arquivos
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4}
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - Seja Bem Vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();

            }    

        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do Cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            // Adicionamos a variavel "cliente" para a lista criada "clientes"
            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido. aperte enter para sair.");
            Console.ReadLine();

        }

        static void Listagem()
        {
            if (clientes.Count > 0) // Se tiver pelo menos um cliente
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes) //Para cada "Cliente" dentro da lista de "clientes" eu vou fazer alguma coisa...
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("================================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Não há clientes cadastrados!");
            }

            Console.WriteLine("Aperte enter para sair.");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você deseja remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID invalido, tente novamente!");
                Console.ReadLine();
            }
        }


        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat",FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);

            stream.Close();

        }
        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            // Toda vez que algum erro acontecer o meu código não para e o "catch" irá tratar esse problema
            try
            {
                
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                
            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }

    }
}

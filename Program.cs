using System.Collections.Generic;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            ClientGenerator generator = new ClientGenerator(server);
            List<Client> clientsList = generator.GenerateClients(20);
            foreach(Client client in clientsList)
            {
                client.Work();
            }
        }
    }
}

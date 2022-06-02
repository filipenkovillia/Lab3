using System;
using System.Collections.Generic;

namespace Lab3
{
    public class ClientGenerator
    {
        private Random _rand;
        private int _clientsCount = 0;
        private Server _server;

        public ClientGenerator(Server server)
        {
            _rand = new Random();
            _server = server;
        }

        public List<Client> GenerateClients(int count)
        {
            List<Client> result = new List<Client>();
            for(int i = _clientsCount + 1; i <= _clientsCount + count; i++)
            {
                result.Add(GenerateClient(i));
            }
            return result;
        }

        public Client GenerateClient(int id)
        {
            Client newClient = new Client(_server);
            newClient.ID = id;
            newClient.RequiredResources = _rand.Next(100);
            return newClient;
        }
    }
}

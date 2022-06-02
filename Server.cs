using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lab3
{
    public class Server
    {
        public const int MaxResourceNumber = 100;

        private int _freeResources = 0;
        private object locker = new object();
        private Queue<Client> ClientQueue = new Queue<Client>();
        private List<Client> ReadyClients = new List<Client>();
        
        public Server()
        {

        }

        public ServerResponse Request(Client client, int resourceNumber)
        {
            if (resourceNumber > _freeResources || ClientQueue.Any())
            {
                ClientQueue.Enqueue(client);
                //Monitor
                lock (locker)
                {
                    Client currentClient;
                    ClientQueue.TryPeek(out currentClient);
                    Console.WriteLine($"Client {client.ID} is working with {client.RequiredResources} resources");
                    Thread.Sleep(1000);
                    currentClient = ClientQueue.Dequeue();
                    ReadyClients.Add(currentClient);
                    _freeResources += currentClient.RequiredResources;
                    Console.WriteLine($"Client {client.ID} finished working. {client.RequiredResources} resources released");
                }
                return ServerResponse.OK;
            }
            else
            {
                _freeResources = _freeResources - resourceNumber;
                Free(resourceNumber);
                return ServerResponse.OK;
            }
        }

        public void Free(int resourceNumber)
        {
            _freeResources = _freeResources + resourceNumber;
            int temp = _freeResources;
            while(ClientQueue.Any())
            {
                Client currentClient;
                ClientQueue.TryPeek(out currentClient);
                if (currentClient.RequiredResources <= temp)
                {
                    currentClient = ClientQueue.Dequeue();
                    ReadyClients.Add(currentClient);
                    temp = temp - resourceNumber;
                }
            }
        }
    }
}

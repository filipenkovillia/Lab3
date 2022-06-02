namespace Lab3
{
    public class Client
    {
        private Server _server;

        public int ID { get; set; }
        public int RequiredResources { get; set; }
        
        public Client(Server server)
        {
            _server = server;
        }

        public void Work()
        {
            _server.Request(this, RequiredResources);
        }
    }
}

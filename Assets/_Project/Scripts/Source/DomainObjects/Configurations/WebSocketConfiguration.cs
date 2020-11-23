namespace _Project.Scripts.Source.DomainObjects.Configurations
{
    public struct WebSocketConfiguration
    {
        public string url;

        public WebSocketConfiguration(string url = "ws://localhost:8080/")
        {
            this.url = url;
        }
    }
}
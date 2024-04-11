using Unity.Entities;
using Unity.Networking.Transport;

namespace Code.ManualConnection
{
    public struct ListenForClients : IComponentData
    {
        public NetworkEndpoint Endpoint;
    }
}
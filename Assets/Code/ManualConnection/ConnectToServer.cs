using Unity.Entities;
using Unity.Networking.Transport;

namespace Code.ManualConnection
{
    public struct ConnectToServer : IComponentData
    {
        public NetworkEndpoint Endpoint;
    }
}
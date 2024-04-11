using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Code.SecureDriver
{
    public struct SecureDriverConstructor : INetworkStreamDriverConstructor
    {
        public void CreateClientDriver(World world, ref NetworkDriverStore driverStore, NetDebug netDebug)
        {
            Debug.Log("Creating client driver");
            DefaultDriverBuilder.RegisterClientDriver(
                world, ref driverStore, netDebug,
                caCertificate: ref SecureParameters.GameClientCA,
                serverName: ref SecureParameters.ServerCommonName);
        }

        public void CreateServerDriver(World world, ref NetworkDriverStore driverStore, NetDebug netDebug)
        {
            Debug.Log("Creating server driver");
            DefaultDriverBuilder.RegisterServerDriver(
                world, ref driverStore, netDebug,
                certificate: ref SecureParameters.GameServerCertificate,
                privateKey: ref SecureParameters.GameServerPrivate);
        }
    }
}
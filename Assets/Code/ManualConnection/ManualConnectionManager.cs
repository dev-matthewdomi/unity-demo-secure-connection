using System.Collections;
using Code.Netcode;
using JetBrains.Annotations;
using Unity.Entities;
using Unity.NetCode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UI;

namespace Code.ManualConnection
{
    public class ManualConnectionManager : MonoBehaviour
    {
        [SerializeField] private Button BtnConnect;
        
        public void Start()
        {
            BtnConnect.interactable = NetCodeBootstrap.UseManualConnect;
        }

        public void OnClick_StartClientServer()
        {
            StartCoroutine(StartClientServer());
        }


        private static IEnumerator StartClientServer()
        {
            World serverWorld = ClientServerBootstrap.CreateServerWorld("ServerWorld");
            World clientWorld = ClientServerBootstrap.CreateClientWorld("ClientWorld");
            World.DefaultGameObjectInjectionWorld ??= clientWorld;

            var connectEvent = new ConnectToServer
            {
                Endpoint = NetworkEndpoint.Parse("127.0.0.1", NetCodeBootstrap.Port)
            };
            
            serverWorld.EntityManager.CreateSingleton(connectEvent);
            clientWorld.EntityManager.CreateSingleton(connectEvent);
            
            GetLocalSimulationWorld()?.Dispose();

            yield return null;
        }
        
        [CanBeNull]
        private static World GetLocalSimulationWorld()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (World world in World.All)
                if (world.Flags == WorldFlags.Game)
                    return world;

            return null;
        }
    }
}
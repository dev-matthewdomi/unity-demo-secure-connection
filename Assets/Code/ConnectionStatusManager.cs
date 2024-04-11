using System;
using JetBrains.Annotations;
using TMPro;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Code
{
    public class ConnectionStatusManager : MonoBehaviour
    {
        private enum ConnectionStatus
        {
            Disconnected,
            Connected,
            ConnectedInGame
        }

        [SerializeField] private TextMeshProUGUI TextConnectionStatus;
        
        public void Update()
        {
            World clientWorld = GetClientWorld();
            if (clientWorld == null)
            {
                SetConnectionStatus(ConnectionStatus.Disconnected);
                return;
            }

            EntityQuery connectionQuery = clientWorld.EntityManager.CreateEntityQuery(typeof(NetworkId));
            if (connectionQuery.CalculateEntityCount() == 0)
            {
                SetConnectionStatus(ConnectionStatus.Disconnected);
                return;
            }

            EntityQuery connectionInGameQuery = clientWorld.EntityManager
                .CreateEntityQuery(typeof(NetworkId), typeof(NetworkStreamInGame));
            SetConnectionStatus(connectionInGameQuery.CalculateEntityCount() == 0
                ? ConnectionStatus.Connected
                : ConnectionStatus.ConnectedInGame);
        }


        private void SetConnectionStatus(ConnectionStatus status)
        {
            TextConnectionStatus.text = Enum.GetName(typeof(ConnectionStatus), status);

            TextConnectionStatus.color = status switch
            {
                ConnectionStatus.Disconnected => Color.red,
                ConnectionStatus.Connected => Color.yellow,
                ConnectionStatus.ConnectedInGame => Color.green,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        [CanBeNull]
        private static World GetClientWorld()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (World world in World.All)
                if (world.IsClient())
                    return world;

            return null;
        }
    }
}
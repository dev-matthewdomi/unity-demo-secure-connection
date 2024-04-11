using Code.Netcode;
using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Code.ManualConnection
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation | WorldSystemFilterFlags.ClientSimulation)]
    public partial struct ConnectionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            if (!NetCodeBootstrap.UseManualConnect)
                state.RequireForUpdate<DisableSystem>();
            state.RequireForUpdate<NetworkStreamDriver>();
            state.RequireForUpdate<ConnectToServer>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var connectEvent = SystemAPI.GetSingleton<ConnectToServer>();
            if (NetCodeBootstrap.UseComponentsToConnect)
            {
                Entity driverEntity = SystemAPI.GetSingletonEntity<NetworkStreamDriver>();
                if (state.WorldUnmanaged.IsServer())
                {
                    Debug.Log("ListenWithComponent");
                    state.EntityManager.AddComponentData(driverEntity, new NetworkStreamRequestListen
                    {
                        Endpoint = connectEvent.Endpoint
                    });
                }
                else
                {
                    Debug.Log("ConnectWithComponent");
                    state.EntityManager.AddComponentData(driverEntity, new NetworkStreamRequestConnect
                    {
                        Endpoint = connectEvent.Endpoint
                    });
                }
            }
            else
            {
                var driver = SystemAPI.GetSingleton<NetworkStreamDriver>();
                if (state.WorldUnmanaged.IsServer())
                {
                    Debug.Log("ListenDirect");
                    driver.Listen(connectEvent.Endpoint);
                }
                else
                {
                    Debug.Log("ConnectDirect");
                    driver.Connect(state.EntityManager, connectEvent.Endpoint);
                }
            }

            Entity connectEntity = SystemAPI.GetSingletonEntity<ConnectToServer>();
            state.EntityManager.DestroyEntity(connectEntity);


            // var ecb = new EntityCommandBuffer(Allocator.Temp);
            // foreach ((ConnectToServer connectEvent, Entity entity) in SystemAPI.Query<ConnectToServer>()
            //              .WithEntityAccess())
            // {
            //     if (NetCodeBootstrap.UseComponentsToConnect)
            //     {
            //         Debug.Log("Connecting");
            //         Entity driverEntity = SystemAPI.GetSingletonEntity<NetworkStreamDriver>();
            //         if (isServer)
            //         {
            //             ecb.AddComponent(driverEntity, new NetworkStreamRequestListen
            //             {
            //                 Endpoint = connectEvent.Endpoint
            //             });
            //         }
            //         else
            //         {
            //             ecb.AddComponent(driverEntity, new NetworkStreamRequestConnect
            //             {
            //                 Endpoint = connectEvent.Endpoint
            //             });
            //         }
            //     }
            //     else
            //     {
            //         var driver = SystemAPI.GetSingleton<NetworkStreamDriver>();
            //         if (isServer)
            //         {
            //             driver.Listen(connectEvent.Endpoint);
            //         }
            //         else
            //         {
            //             driver.Connect(state.EntityManager, connectEvent.Endpoint);
            //         }
            //     }
            //         
            //     ecb.DestroyEntity(entity);
            // }
            //
            // ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}
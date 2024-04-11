using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.NetCode;
using UnityEngine;

namespace Code.GoInGame
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation)]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct GoInGameClientSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            EntityQueryBuilder builder = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<NetworkId>()
                .WithNone<NetworkStreamInGame>();
            
            state.RequireForUpdate(state.GetEntityQuery(builder));
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
            JobHandle requestJobHandle = new RequestToGoInGameJob
            {
                EcbParallel = ecb.AsParallelWriter()
            }.ScheduleParallel(state.Dependency);
            requestJobHandle.Complete();
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }


        [BurstCompile]
        [WithNone(typeof(NetworkStreamInGame))]
        private partial struct RequestToGoInGameJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter EcbParallel;

            public void Execute(
                [ChunkIndexInQuery] int sortKey,
                Entity connectionEntity,
                in NetworkId networkId)
            {
                EcbParallel.AddComponent<NetworkStreamInGame>(sortKey, connectionEntity);

                Entity reqEntity = EcbParallel.CreateEntity(sortKey);
                EcbParallel.AddComponent(sortKey, reqEntity, new GoInGameRequest());
                EcbParallel.AddComponent<SendRpcCommandRequest>(sortKey, reqEntity);
                
                Debug.Log($"[Client {networkId.Value}]: Requested to GoInGame");
            }
        }
    }
}
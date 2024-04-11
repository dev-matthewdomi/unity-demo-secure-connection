using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.NetCode;
using UnityEngine;

namespace Code.GoInGame
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct GoInGameServerSystem : ISystem
    {
        [ReadOnly] private ComponentLookup<NetworkId> _networkIdLookup;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            EntityQueryBuilder builder = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<GoInGameRequest>()
                .WithAll<ReceiveRpcCommandRequest>();
            EntityQuery goInGameRequestQuery = builder.Build(ref state);

            _networkIdLookup = state.GetComponentLookup<NetworkId>(true);
            
            state.RequireForUpdate(goInGameRequestQuery);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
            _networkIdLookup.Update(ref state);
            JobHandle jobHandle = new BringNewConnectionsInGameJob
            {
                EcbParallel = ecb.AsParallelWriter(),
                NetworkIdLookup = _networkIdLookup
            }.ScheduleParallel(state.Dependency);
            jobHandle.Complete();
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }


        [BurstCompile]
        private partial struct BringNewConnectionsInGameJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter EcbParallel;
            [ReadOnly] public ComponentLookup<NetworkId> NetworkIdLookup;

            public void Execute(
                [ChunkIndexInQuery] int sortKey,
                Entity entity, 
                in GoInGameRequest req,
                in ReceiveRpcCommandRequest reqSrc)
            {
                EcbParallel.AddComponent<NetworkStreamInGame>(sortKey, reqSrc.SourceConnection);
                EcbParallel.DestroyEntity(sortKey, entity);

                Debug.Log(NetworkIdLookup.TryGetComponent(reqSrc.SourceConnection, out NetworkId networkId)
                    ? $"[Server]: Brought Client '{networkId.Value}' InGame"
                    : "[Server]: Brought Client InGame");
            }
        }
    }
}
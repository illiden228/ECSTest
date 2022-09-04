using Leopotam.EcsLite;
using UnityEngine;

public class PlayerViewInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var gameData = systems.GetShared<GameDataView>();
        gameData.Player = GameObject.FindObjectOfType<Player>();
        
        var movablesPool = world.GetPool<MovableComponent>();
        var animatedPool = world.GetPool<AnimatedUnit>();
        var movableViewPool = world.GetPool<MovableViewComponent>();
        var playerFilter = world.Filter<PlayerTag>().End();
        var playerEntity = playerFilter.GetRawEntities()[0];

        ref MovableComponent playerMovable = ref movablesPool.Get(playerEntity);
        playerMovable.Position = gameData.Player.transform.position;
        playerMovable.TargetPosition = playerMovable.Position;
        
        ref AnimatedUnit animatedUnit = ref animatedPool.Add(playerEntity);
        animatedUnit.Animator = gameData.Player.GetComponentInChildren<Animator>();
        ref MovableViewComponent movableViewComponent = ref movableViewPool.Add(playerEntity);
        movableViewComponent.Object = gameData.Player.gameObject;
    }
}
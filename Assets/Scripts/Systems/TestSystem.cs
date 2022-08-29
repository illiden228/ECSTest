using Leopotam.EcsLite;
using UnityEngine;

public class TestSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var filter = world.Filter<PlayerTag>().End ();
        var movables = world.GetPool<MovableComponent>();
        foreach (var entity in filter)
        {
            ref MovableComponent movableComponent = ref movables.Get(entity);
            Debug.Log(movableComponent.TargetPosition);
            Debug.Log("Позиция - " + movableComponent.Position);
        }
    }
}
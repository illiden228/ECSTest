using System.Reflection;
using Leopotam.EcsLite;
using UnityEngine;

public class MovableToTargetSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var movables = world.GetPool<MovableComponent>();
        EcsFilter player = world.Filter<PlayerTag>().End(); 

        foreach (var entity in player)
        {
            ref MovableComponent movableComponent = ref movables.Get(entity);
            float distanceToTarget = Vector3.Distance(movableComponent.Position, movableComponent.TargetPosition);
            if (distanceToTarget > 0.001f)
            {
                movableComponent.Position = Vector3.MoveTowards(movableComponent.Position, movableComponent.TargetPosition, movableComponent.Speed * Time.deltaTime);
            }
        }
    }
}
using Leopotam.EcsLite;
using UnityEngine;

public class GateTriggerSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var buttonsPool = world.GetPool<ButtonComponent>();
        var buttonsFilter = world.Filter<ButtonComponent>().End();
        var movables = world.GetPool<MovableComponent>();

        foreach (var entity in buttonsFilter)
        {
            ref ButtonComponent button = ref buttonsPool.Get(entity);
            Vector3 clickUnitPosition = movables.Get(button.PlayerEntity).Position;
            button.IsPressed = Vector3.Distance(clickUnitPosition, button.Position) < button.Radius;
        }
    }
}
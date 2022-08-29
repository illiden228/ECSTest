using Leopotam.EcsLite;
using UnityEngine;

public class ButtonSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var buttonsPool = world.GetPool<ButtonTriggerComponent>();
        EcsFilter buttonsFilter = world.Filter<ButtonTriggerComponent>().End();
        EcsPool<MovableComponent> movables = world.GetPool<MovableComponent>();

        foreach (var entity in buttonsFilter)
        {
            ref ButtonTriggerComponent button = ref buttonsPool.Get(entity);
            Vector3 clickUnitPosition = movables.Get(button.PlayerEntity).Position;

            if (Vector3.Distance(clickUnitPosition, button.Position) < button.Radius)
            {
                button.IsPressed = true;
            }
            
            Debug.Log("Нажатие - " + button.IsPressed);
        }
    }
}
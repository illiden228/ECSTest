using Leopotam.EcsLite;
using UnityEngine;

sealed class InputSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world = null;
    
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld ();
    }
    
    public void Run(IEcsSystems systems)
    {
        var filter = _world.Filter<PlayerTag>().End ();
        var inputs = _world.GetPool<MovableComponent>();

        if (Input.GetMouseButtonDown(0))
        {
            foreach (var entity in filter)
            {
                ref MovableComponent inputComponent = ref inputs.Get(entity);
                inputComponent.TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}
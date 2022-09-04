using Leopotam.EcsLite;
using UnityEngine;

sealed class InputSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private Camera _camera;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld ();
        _camera = Camera.main;
        var gameData = systems.GetShared<GameDataView>();
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
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (var hit in hits)
                {
                    if (hit.collider.GetComponent<Floor>())
                    {
                        inputComponent.TargetPosition = hit.point;
                    }
                }
            }
        }
    }
}
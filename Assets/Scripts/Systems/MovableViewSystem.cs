using System.Numerics;
using Leopotam.EcsLite;
using Vector3 = UnityEngine.Vector3;

public class MovableViewSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsWorld _world;
    private EcsPool<MovableComponent> _movables;
    private EcsPool<MovableViewComponent> _movablesView;
    
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _movables = _world.GetPool<MovableComponent>();
        _movablesView = _world.GetPool<MovableViewComponent>();
    }
    
    public void Run(IEcsSystems systems)
    {
        var movableViewFilter = _world.Filter<MovableViewComponent>().End();
        foreach (var entity in movableViewFilter)
        {
            ref MovableComponent movableComponent = ref _movables.Get(entity);
            ref MovableViewComponent movableViewComponent = ref _movablesView.Get(entity);
            if (movableComponent.IsMoving)
            {
                movableViewComponent.Object.transform.position = movableComponent.Position;
                Vector3 direction = movableComponent.TargetPosition - movableComponent.Position;
                if (direction != Vector3.zero)
                    movableViewComponent.Object.transform.forward = direction;
            }
        }
    }
}
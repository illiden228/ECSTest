using Leopotam.EcsLite;

public class AnimationSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsPool<MovableComponent> _movablesPool;
    private EcsPool<AnimatedUnit> _animatedPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _movablesPool = _world.GetPool<MovableComponent>();
        _animatedPool = _world.GetPool<AnimatedUnit>();
    }

    public void Run(IEcsSystems systems)
    {
        var animatedFilter = _world.Filter<AnimatedUnit>().End();
        foreach (var entity in animatedFilter)
        {
            ref MovableComponent movableComponent = ref _movablesPool.Get(entity);
            ref AnimatedUnit animatedComponent = ref _animatedPool.Get(entity);

            if (movableComponent.IsMoving)
            {
                animatedComponent.Animator.Play("Run");
            }
            else
            {
                animatedComponent.Animator.Play("Idle");
            }
        }
    }
}
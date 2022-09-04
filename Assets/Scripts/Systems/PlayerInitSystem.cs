using Leopotam.EcsLite;

public class PlayerInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        int player = world.NewEntity();
        EcsPool<MovableComponent> movables = world.GetPool<MovableComponent>();
        EcsPool<PlayerTag> playerTags = world.GetPool<PlayerTag>();
        ref MovableComponent movableComponent = ref movables.Add (player);
        playerTags.Add(player);

        var gameData = systems.GetShared<GameData>();
        
        movableComponent.Speed = gameData.PlayerMovementSpeed;
    }
}
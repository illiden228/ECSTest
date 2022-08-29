using Leopotam.EcsLite;
using UnityEngine;

public class GameInitSystem : IEcsInitSystem
{
    private EcsWorld _world = null;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        int player = _world.NewEntity();
        EcsPool<MovableComponent> movables = _world.GetPool<MovableComponent>();
        EcsPool<PlayerTag> playerTags = _world.GetPool<PlayerTag>();
        ref MovableComponent movableComponent = ref movables.Add (player);
        playerTags.Add(player);
        
        int buttonGreen = _world.NewEntity();
        EcsPool<ButtonTriggerComponent> triggers = _world.GetPool<ButtonTriggerComponent>();
        ref ButtonTriggerComponent buttonTriggerComponent = ref triggers.Add(buttonGreen);

        int gateGreen = _world.NewEntity();
        EcsPool<OpenGateComponent> gates = _world.GetPool<OpenGateComponent>();
        ref OpenGateComponent gateComponent = ref gates.Add(gateGreen);

        buttonTriggerComponent.Radius = 2f;
        buttonTriggerComponent.Position = new Vector3(-10, 19, -10);
        buttonTriggerComponent.IsPressed = false;
        buttonTriggerComponent.PlayerEntity = player;

        gateComponent.Button = buttonGreen;
        gateComponent.IsOpen = false;

        movableComponent.Speed = 2f;
    }
}
using Leopotam.EcsLite;
using UnityEngine;

public class GatesInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();

        var gameData = systems.GetShared<GameData>();
        var openGatePool = world.GetPool<GateComponent>();
        var buttonTriggerPool = world.GetPool<ButtonComponent>();
        var playerFilter = world.Filter<PlayerTag>().End();
        int playerEntity = playerFilter.GetRawEntities()[0];

        foreach (var buttonToGatePosition in gameData.ButtonToGatePositions)
        {
            int newGateEntity = world.NewEntity();
            ref GateComponent gateComponent = ref openGatePool.Add(newGateEntity);
            ref ButtonComponent buttonComponent = ref buttonTriggerPool.Add(newGateEntity);
            
            gateComponent.CurrentPosition = buttonToGatePosition.Value;
            gateComponent.ClosePosition = gateComponent.CurrentPosition;
            gateComponent.IsOpen = false;
            gateComponent.OpenPosition = gateComponent.ClosePosition + Vector3.up * gameData.OpenUpDistance;
            gateComponent.OpenSpeed = gameData.GateOpenSpeed;

            buttonComponent.Position = buttonToGatePosition.Key;
            buttonComponent.Radius = gameData.ButtonRadius;
            buttonComponent.IsPressed = false;
            buttonComponent.PlayerEntity = playerEntity;
        }
    }
}
using Leopotam.EcsLite;
using UnityEngine;

public class GateOpenSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var buttonsPool = world.GetPool<ButtonComponent>();
        var gatesPool = world.GetPool<GateComponent>();
        var gateFilter = world.Filter<GateComponent>().Inc<ButtonComponent>().End();

        foreach (var entity in gateFilter)
        {
            ref GateComponent gate = ref gatesPool.Get(entity);
            ref ButtonComponent button = ref buttonsPool.Get(entity);

            if (button.IsPressed && !gate.IsOpen)
            {
                float distanceToTarget = Vector3.Distance(gate.CurrentPosition, gate.OpenPosition);
                if (distanceToTarget > 0.001f)
                {
                    gate.CurrentPosition = Vector3.MoveTowards(gate.CurrentPosition, gate.OpenPosition, gate.OpenSpeed * Time.deltaTime);
                }
                else
                {
                    gate.IsOpen = true;
                }
            }
        }
    }
}
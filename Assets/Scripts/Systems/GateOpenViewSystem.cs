using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using UnityEngine;

public class GateOpenViewSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsPool<GateComponent> _openGatePool;
    private EcsPool<ButtonComponent> _buttonPool;
    private EcsFilter _gateFilter;
    private List<Gate> _gates;
    private GameDataView _gameData;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        _openGatePool = world.GetPool<GateComponent>();
        _buttonPool = world.GetPool<ButtonComponent>();
        _gateFilter = world.Filter<GateComponent>().Inc<ButtonComponent>().End();
        _gameData = systems.GetShared<GameDataView>();
        _gates = _gameData.GateGameObjects;
    }
    
    public void Run(IEcsSystems systems)
    {
        foreach (var gate in _gateFilter)
        {
            ref GateComponent gateComponent = ref _openGatePool.Get(gate);
            ref ButtonComponent buttonComponent = ref _buttonPool.Get(gate);
            Transform gateView = null;
            foreach (var goGates in _gates)
            {
                if (goGates.Button.transform.position == buttonComponent.Position)
                    gateView = goGates.Door.transform;
            }
            if(gateView != null)
                gateView.position = gateComponent.CurrentPosition;
        }
    }
}
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

public class EcsGameStartup
{
    private EcsWorld _world;
    private IEcsSystems _initSystems;
    private IEcsSystems _runSystems;
    private IEcsSystems _viewSystems;

    public IEcsSystems InitSystems => _initSystems;
    public IEcsSystems RunSystems => _runSystems;
    public IEcsSystems ViewSystems => _viewSystems;

    public EcsGameStartup(GameData gameData)
    {
        _world = new EcsWorld ();
        
        _initSystems = new EcsSystems (_world, gameData);
        _runSystems = new EcsSystems (_world, gameData);
        _viewSystems = new EcsSystems (_world, gameData);
    }

    public void Init(List<IEcsSystem> viewSystems)
    {
        _initSystems
            .Add(new PlayerInitSystem())
            .Add(new GatesInitSystem());

        _runSystems
            .Add(new MovableSystem())
            .Add(new GateTriggerSystem())
            .Add(new GateOpenSystem());
        
        foreach (var system in viewSystems)
        {
            _viewSystems.Add(system);
        }
        
        _runSystems.Init();
        _initSystems.Init();
        _viewSystems.Init();
    }

    public void Destroy()
    {
        _initSystems?.Destroy();
        _runSystems?.Destroy();
        _viewSystems?.Destroy();

        _initSystems = null;
        _runSystems = null;
        _viewSystems = null;
        
        _world?.Destroy ();
        _world = null;
    }

    public void Run()
    {
        _runSystems?.Run();
        _viewSystems?.Run();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

public class StartupGame : MonoBehaviour
{
    private EcsWorld _world;
    private IEcsSystems _systems;

    private void Start()
    {
        _world = new EcsWorld ();
        _systems = new EcsSystems (_world);
        _systems
            .Add(new GameInitSystem())
            .Add(new InputSystem())
            .Add(new MovableToTargetSystem())
            .Add(new TestSystem())
            .Add(new ButtonSystem())
            .Init();
    }

    private void Update()
    {
        _systems?.Run();
    }

    private void OnDestroy()
    {
        if (_systems != null) {
            _systems.Destroy ();
            _systems = null;
        }
        if (_world != null) {
            _world.Destroy ();
            _world = null;
        }
    }
}

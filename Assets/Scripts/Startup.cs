using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] private Configuration _configuration;
    private EcsGameStartup _gameStartup;
    
    private void Start()
    {
        GameDataView gameData = new GameDataView();
        gameData.PlayerMovementSpeed = _configuration.PlayerMovementSpeed;
        gameData.GateOpenSpeed = _configuration.GateOpenSpeed;
        gameData.ButtonRadius = _configuration.ButtonRadius;
        gameData.OpenUpDistance = _configuration.OpenUpDistance;
        gameData.ButtonToGatePositions = new Dictionary<Vector3, Vector3>();
        gameData.GateGameObjects = FindObjectsOfType<Gate>().ToList();

        foreach (var gate in gameData.GateGameObjects)
        {
            gameData.ButtonToGatePositions.Add(gate.Button.transform.position, gate.Door.transform.position);
        }

        List<IEcsSystem> viewSystems = new List<IEcsSystem>()
        {
            new PlayerViewInitSystem(),
            new MovableViewSystem(),
            new AnimationSystem(),
            new InputSystem(),
            new GateOpenViewSystem()
        };
            
        _gameStartup = new EcsGameStartup(gameData);
        _gameStartup.Init(viewSystems);
    }

    private void Update()
    {
        _gameStartup.Run();
    }

    private void OnDestroy()
    {
        _gameStartup.Destroy();
    }
}

public class GameDataView : GameData
{
    public List<Gate> GateGameObjects;
    public Player Player;
}
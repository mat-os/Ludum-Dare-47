using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
   // public PrefabLevelCanvas PrefabLevelCanvas;

    private GamePrefabLevelController _prefabLevelController;
    public GamePrefabLevelController PrefabLevelController => _prefabLevelController;

    public void Init(GamePrefabLevelController prefabLevelController)
    {
        _prefabLevelController = prefabLevelController;
        
       // PrefabLevelCanvas.Init(this);
        OnInit();
    }

    //------------------------------------------
    /// Порядок вызова - сверху-вниз
    /// OnStartGame - Вызов когда начинаем геймплей
    /// OnEndGame - Вызов когда дошли до финиша и геймпелй закончен
    public abstract void OnAwake();
    public abstract void OnInit();
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnStartGame();
    public abstract void OnEndGame();
    //------------------------------------------
    private void Awake()
    {
        OnAwake();
    }
    private void Start()
    {
        OnStart();
    }
    public void StartGame()
    {
        OnStartGame();
    }
    public void EndGame()
    {
        OnEndGame();
    }
    //------------------------------------------
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePrefabLevelController : MonoBehaviour
{
    public Transform PrefabLevelHolder;
    public PrefabLevelManager PrefabLevelManager;
    
    private Level _currentPrefabLevel;
  //  private MenuPrefabLevel _menuPrefabLevel;

  //  private BattleConfig _currentBattleConfig;
    //Номер битвы, которая у нас сейчас
    public int _currentBattleNumber;
    //Этап битвы, которая у нас сейчас
    public int _currentPlayerStage;
    
    public Level CurrentPrefabLevel => _currentPrefabLevel;

    private void Awake()
    {
        if (PrefabLevelManager.IsUseDebugLevel)
        {
            InitDebugLevel();
        }
        else
        {
          //  InitMenu();
        }
    }
    
    //------------ MENU --------------
    public void InitMenu()
    {
       // _menuPrefabLevel = Instantiate(PrefabLevelManager.MenuLevel.gameObject, PrefabLevelHolder).GetComponent<MenuPrefabLevel>();
       // _menuPrefabLevel.InitMenu();
    }
    public void RemoveMenu()
    {
      //  Destroy(_menuPrefabLevel.gameObject);
    }
    //------------ LEVELS --------------

    //Начинаем новую игру из меню
    public void StartNewGame()
    {
        RemoveMenu();

      //  _currentBattleConfig = PrefabLevelManager.GetBattle(_currentBattleNumber);
     //   InitNewLevel(_currentBattleConfig.PrefabLevels[_currentPlayerStage]);
    }
    public void InitNewLevel(GameObject levelGo)
    {
        _currentPrefabLevel = Instantiate(levelGo, PrefabLevelHolder).GetComponent<Level>();
        _currentPrefabLevel.Init(this);
    }

    public void LoadNextLevel()
    {
        _currentPlayerStage++;

        /*if (_currentPlayerStage > _currentBattleConfig.PrefabLevels.Length)
        {
            Debug.Log("This Is last stage");
        }
        else
        {
            RemoveCurrentLevel();
            InitNewLevel(_currentBattleConfig.PrefabLevels[_currentPlayerStage]);
        }*/
    }
    private void RemoveCurrentLevel()
    {
        Destroy(_currentPrefabLevel.gameObject);
        _currentPrefabLevel = null;
    }
    //------------ DEBUGS --------------
    public void InitDebugLevel()
    {
        _currentPrefabLevel = PrefabLevelManager.DebugLevel;
    }
}

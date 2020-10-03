using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabLevelManager : MonoBehaviour
{
    public Level DebugLevel;
    public bool IsUseDebugLevel;
    
    //Все уровни в игре
    public PrefabLevel[] PrefabLevels = new PrefabLevel[0];
    
    //Уровни в лупе
    public List<Level> LoopPrefabLevels = new List<Level>();
    
    public int GetCountOfPrefabLevels()
    {
        return PrefabLevels.Length;
    }

    public Level GetRandLevelFromLoop(int levelNumber)
    {
        Shuffle();
        //TODO: level shuffle 
        return LoopPrefabLevels[0];
    }
    private void OnValidate()
    {
        for (var i = 0; i < PrefabLevels.Length; i++) PrefabLevels[i].Order = i;

        LoopPrefabLevels.Clear();
        for (var i = 0; i < PrefabLevels.Length; i++)
        {
            if (PrefabLevels[i].IsInLoop)
            {
                LoopPrefabLevels.Add(PrefabLevels[i].Level);
            }
        }
    }
    
    private Level tempGO;
    private void Shuffle() {
        for (int i = 0; i < LoopPrefabLevels.Count; i++) {
            int rnd = Random.Range(0, LoopPrefabLevels.Count);
            tempGO = LoopPrefabLevels[rnd];
            LoopPrefabLevels[rnd] = LoopPrefabLevels[i];
            LoopPrefabLevels[i] = tempGO;
        }
    }
}

[Serializable]
public struct PrefabLevel
{
    public int Order;
    public Level Level;
    public bool IsInLoop;
}
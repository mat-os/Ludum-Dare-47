using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using UnityEngine;

public class AnaliticsController : MonoBehaviour
{
    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void NewLevelStartedEvent(int levelNum)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level-{levelNum}", 0);
    }

    public void NewLevelCompletedEvent(int levelNum)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level-{levelNum}", 0);
    }

    public void NewLevelFailedEvent(int levelNum)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level-{levelNum}", 0);
    }

    public void NewLevelSkippedEvent(int levelNum,int stageNum)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, $"Skipped Level-{levelNum}-{stageNum}", 0);
    }
}

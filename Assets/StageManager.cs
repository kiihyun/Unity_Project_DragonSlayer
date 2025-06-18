using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    int maxClearStage = 1;

    public void SetMaxClearStage(int stage)
    {
        if(stage > maxClearStage)
        {
            maxClearStage = stage;
        }
    }

    public int GetMaxClearStage()
    {
        return maxClearStage;
    }
}

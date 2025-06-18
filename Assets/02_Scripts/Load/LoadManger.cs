using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManger : Singleton<LoadManger>
{
    public bool IsLoadGame;
    protected override void Awake()
    {
        base.Awake();
    }
    

    public void LoadGame()
    {
        IsLoadGame = true;
    }

    public void NewGame()
    {
        IsLoadGame = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadManger : Singleton<LoadManger>
{
    public bool IsLoadGame;
    public PlayerLoad _playerLoad;

    protected override void Awake()
    {
        base.Awake();
    }

    private string GetPath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
    }


    // 객체 불러오기 (Json)
    public T Load<T>(string fileName) where T : new()
    {
        string path = GetPath(fileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning($"[SaveManager] 파일 없음, 기본값 반환: {path}");
            return new T();
        }
        try
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log($"[SaveManager] 불러오기 완료: {path}\n{json}");
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] 불러오기 실패: {path}, 예외: {e.Message}");
            return new T();
        }
    }

    public void LoadPlayer()
    {
        _playerLoad.LoadPlayer();
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

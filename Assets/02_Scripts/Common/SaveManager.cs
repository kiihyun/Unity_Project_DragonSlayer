using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string GetPath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
    }

    // 객체 저장 (Json)
    public void Save<T>(string fileName, T data)
    {
        string path = GetPath(fileName);
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
            Debug.Log($"[SaveManager] 저장 완료: {path}\n{json}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] 저장 실패: {path}, 예외: {e.Message}");
        }
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

    // 저장 파일 삭제
    public void Delete(string fileName)
    {
        string path = GetPath(fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"[SaveManager] 파일 삭제: {path}");
        }
        else
        {
            Debug.LogWarning($"[SaveManager] 삭제 시도 - 파일 없음: {path}");
        }
    }
} 
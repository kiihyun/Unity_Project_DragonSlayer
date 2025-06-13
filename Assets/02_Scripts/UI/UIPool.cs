using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI 재사용 관리
public class UIPool
{
    private readonly Dictionary<UIType, Queue<BaseUI>> _pool = new();

    // type에 해당하는 Queue<BaseUI>가 풀에 존재한다면 Queue에서 꺼내고,
    // 풀에 존재하지 않는다면 새로 생성하여 부모 오브젝트 하위에 생성한다.
    public BaseUI GetUI(UIType type, Transform parent)
    {
        if (_pool.TryGetValue(type, out Queue<BaseUI> q) && q.Count > 0)
        {
            var ui = q.Dequeue();
            ui.transform.SetParent(parent, false);
            return ui;
        }
        
        // 풀에 존재하지 않을 때, UIPath에 저장되어 있는 경로에 따라 프리팹을 불러와 부모 오브젝트 하위에 생성
        string path = UIPath.GetPath(type);
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject go = GameObject.Instantiate(prefab, parent);
        return go.GetComponent<BaseUI>();
    }
    
    // UI를 비활성화하고 다시 _pool에 반환
    public void ReturnUI(UIType type, BaseUI ui)
    {
        // ui 비활성화
        ui.gameObject.SetActive(false);
        // _pool에 비활성화된 ui의 UIType(딕셔너리의 key)이 존재하지 않으면 그 타입의 Queue를 만들고 이미 존재하면 아무것도 하지 않음 
        _pool.TryAdd(type, new Queue<BaseUI>());
        // 해탕 타입의 Queue<BaseUI>(딕셔너리의 value)에 ui를 추가
        _pool[type].Enqueue(ui);
    }
    
}

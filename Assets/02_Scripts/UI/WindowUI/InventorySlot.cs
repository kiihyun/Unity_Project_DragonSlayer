using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
  
    [SerializeField] private Image itemIcon; // 아이템 아이콘 이미지

    public ItemData data;

    // 슬롯 초기화
    public void Set(ItemData newData)
    {
        data = newData;
        
        if(data != null)
        {
            itemIcon.sprite = data.ItemIcon; // 아이콘 이미지 할당
            itemIcon.enabled = true;
        }
        else
        {
            Clear();
        }
    }

    // 슬롯 비우기
    public void Clear()
    {
        data = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }
}

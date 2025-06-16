using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType
{
    NormalSlot,
    QuickSlot
}
public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler

{
    public SlotType slotType;     // 노말슬롯은 오브젝트 풀링으로 자동으로 NormalSlot으로, 퀵슬롯은 수동으로 지정 
    public int quickSlotIndex;    // 퀵슬롯 구분용 int 값
    public Image itemIcon;        // 아이템 아이콘 이미지
    public ItemData data;         // 아이템 데이터
    private GameObject dragIcon;  // 드래그 시 나타나는 이미지
    private Canvas _parentCanvas; // 드래그 시 나타나는 이미지가 존재할 캔버스(최상위 캔버스가 아니면 이동에 제한이 있음)


    private void Awake()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        if (data == null)
        {
            itemIcon.enabled = false;
        }
    }
    
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

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (data == null) return;
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(_parentCanvas.transform, false);
        var image = dragIcon.AddComponent<Image>();
        image.sprite = data.ItemIcon;
        image.raycastTarget = false;
        dragIcon.transform.position = eventData.position;
    }
    
    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        {
            dragIcon.transform.position = eventData.position;
        }
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        {
            Destroy(dragIcon);
        }
    }
    
    // 슬롯에 아이템 데이터 할당 후 아이콘 갱신
    public void SetItem(ItemData newData)
    {
        data = newData;
        itemIcon.sprite = data?.ItemIcon;
        itemIcon.enabled = data != null;
    }
    
    // 다른 슬롯에서 아이템 받아서 교환, 혹은 이동 처리
    public void OnDrop(PointerEventData eventData)
    {
        // draggedSlot은 마우스로 옮기는 아이템
        InventorySlot draggedSlot  = eventData.pointerDrag?.GetComponent<InventorySlot>();
        if (draggedSlot  == null || draggedSlot  == this)
        {
            return;
        }

        Inventory inventory = UIManager.instance.player.inventory;

        // 퀵슬롯에 드롭 (인벤토리 → 퀵슬롯)
        if (slotType == SlotType.QuickSlot && draggedSlot.slotType == SlotType.NormalSlot)
        {
            inventory.SetQuickSlotItem(quickSlotIndex, draggedSlot.data);
            inventory.RemoveItem(draggedSlot.data);
            Set(draggedSlot.data);
            draggedSlot.Clear();
        }
        // 인벤토리 슬롯에 드롭 (퀵슬롯 → 인벤토리)
        else if (slotType == SlotType.NormalSlot && draggedSlot.slotType == SlotType.QuickSlot)
        {
            // 퀵슬롯을 비우고 인벤토리에 추가
            inventory.SetQuickSlotItem(draggedSlot.quickSlotIndex, null);
            inventory.AddItem(draggedSlot.data);
            Set(draggedSlot.data);
            draggedSlot.Clear();
        }
        // 같은 종류끼리 교환 (인벤토리-인벤토리, 퀵슬롯-퀵슬롯)
        else if (slotType == draggedSlot.slotType)
        {
            SwapItems(draggedSlot);
        }
    }

    // 빈 슬롯이 아닐 경우, 두 슬롯의 아이템 교환
    private void SwapItems(InventorySlot targetItem)
    {
        var currentItem = data;
        SetItem(targetItem.data);
        targetItem.SetItem(currentItem);
    }
}

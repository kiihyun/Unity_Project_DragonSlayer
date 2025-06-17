using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType
{
    NormalSlot,
    QuickSlot,
    EquipSlot
}
public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler

{
    public SlotType slotType;      // 노말슬롯은 오브젝트 풀링으로 자동으로 NormalSlot으로, 퀵슬롯은 수동으로 지정 
    public int slotIndex;          // 슬롯 구분용 int 값
    public Image itemIcon;         // 아이템 아이콘 이미지
    public ItemData data;          // 아이템 데이터
    private GameObject _dragIcon;  // 드래그 시 나타나는 이미지
    private Canvas _parentCanvas;  // 드래그 시 나타나는 이미지가 존재할 캔버스(최상위 캔버스가 아니면 이동에 제한이 있음)
    private Inventory _inventory; 
    
    private void Start()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        _inventory = UIManager.instance.player.inventory;
        if (data == null)
        {
            itemIcon.enabled = false;
        }
    }
    
    // 슬롯 초기화
    public void Set(ItemData newData, int index = -1)
    {
        data = newData;
        if(index >= 0) slotIndex = index;
        if(newData != null)
        {
            itemIcon.sprite = newData.ItemIcon; // 아이콘 이미지 할당
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
        _dragIcon = new GameObject("DragIcon");
        _dragIcon.transform.SetParent(_parentCanvas.transform, false);
        var image = _dragIcon.AddComponent<Image>();
        image.sprite = data.ItemIcon;
        image.raycastTarget = false;
        _dragIcon.transform.position = eventData.position;
    }
    
    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (_dragIcon != null)
        {
            _dragIcon.transform.position = eventData.position;
        }
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_dragIcon != null)
        {
            Destroy(_dragIcon);
        }
    }
    
    // 슬롯에 아이템 데이터 할당 후 아이콘 갱신
    public void SetItem(ItemData newData, int index = -1)
    {
        data = newData;
        if(index >= 0) slotIndex = index;
        itemIcon.sprite = data?.ItemIcon;
        itemIcon.enabled = data != null;
    }
    
    // 다른 슬롯에서 아이템 받아서 교환, 혹은 이동 처리
    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot  = eventData.pointerDrag?.GetComponent<InventorySlot>();
        if (draggedSlot  == null || draggedSlot  == this)
        {
            return;
        }
        ItemData prevData = data; // this 슬롯의 기존 아이템 저장
        
        // 퀵슬롯에 드롭 (인벤토리 → 퀵슬롯)
        if (this.slotType == SlotType.QuickSlot && draggedSlot.slotType == SlotType.NormalSlot) 
        {
            // 인벤토리 슬롯 비우기, 퀵슬롯 아이템을 플레이어의 _consumableItems애서 제거
            _inventory.SetQuickSlotItem(slotIndex, draggedSlot.data);
            
            // this 슬롯 데이터 넣어주기
            Set(draggedSlot.data);
            
            // 만약 this 슬롯에 데이터가 없었다면, draggedSlot 초기화
            if (prevData != null)
            {
                // 기존 아이템을 draggedSlot에 넣어 스왑
                draggedSlot.Set(prevData);
            }
            else
            {
                // 기존에 아이템이 없었다면 draggedSlot을 비움
                draggedSlot.Clear();
            }
#if UNITY_EDITOR
            Debug.Log("인벤토리 → 퀵슬롯");
#endif
        }
        // 인벤토리 슬롯에 드롭 (퀵슬롯 → 인벤토리)
        else if (this.slotType == SlotType.NormalSlot && draggedSlot.slotType == SlotType.QuickSlot)
        {
            // 퀵슬롯 비우기, 퀵슬롯 아이템을 플레이어의 _consumableItems 리스트로 복귀
            _inventory.SetQuickSlotItem(draggedSlot.slotIndex, this.data);
            
            // this 슬롯 초기화
            Set(draggedSlot.data);
            
            // 만약 this 슬롯에 데이터가 없었다면, draggedSlot 초기화
            if (prevData != null)
            {
                // 기존 아이템을 draggedSlot에 넣어 스왑
                draggedSlot.Set(prevData);
            }
            else
            {
                // 기존에 아이템이 없었다면 draggedSlot을 비움
                draggedSlot.Clear();
            }
#if UNITY_EDITOR
            Debug.Log("퀵슬롯 → 인벤토리");
#endif
        }
        // 장착슬롯에 드롭 (인벤토리 → 장착슬롯)
        else if (this.slotType == SlotType.EquipSlot && draggedSlot.slotType == SlotType.NormalSlot)
        {
            // 인벤토리 슬롯 비우기, 장비슬롯 아이템을 플레이어의 _consumableItems애서 제거
            _inventory.SetEquipSlotItem(slotIndex, draggedSlot.data);
            
            // this 슬롯 초기화
            Set(draggedSlot.data);
            
            // 만약 this 슬롯에 데이터가 없었다면, draggedSlot 초기화
            if (prevData != null)
            {
                // 기존 아이템을 draggedSlot에 넣어 스왑
                draggedSlot.Set(prevData);
            }
            else
            {
                // 기존에 아이템이 없었다면 draggedSlot을 비움
                draggedSlot.Clear();
            }
#if UNITY_EDITOR
            Debug.Log("인벤토리 → 장착슬롯");
#endif
        }
        // 인벤토리 슬롯에 드롭 (장착슬롯 → 인벤토리)
        else if (this.slotType == SlotType.NormalSlot && draggedSlot.slotType == SlotType.EquipSlot)
        {
            // 장비슬롯 비우기, 장비슬롯 아이템을 플레이어의 _equipableItems 리스트로 복귀
            _inventory.SetEquipSlotItem(draggedSlot.slotIndex, null);
            
            // this 슬롯 초기화
            Set(draggedSlot.data);
           
            // 만약 this 슬롯에 데이터가 없었다면, draggedSlot 초기화
            if (prevData != null)
            {
                // 기존 아이템을 draggedSlot에 넣어 스왑
                draggedSlot.Set(prevData);
            }
            else
            {
                // 기존에 아이템이 없었다면 draggedSlot을 비움
                draggedSlot.Clear();
            }
#if UNITY_EDITOR
            Debug.Log("장착슬롯 → 인벤토리");
#endif
        }
        // 같은 종류끼리 교환 (인벤토리 -> 인벤토리, 퀵슬롯 -> 퀵슬롯)
        else if (this.slotType == draggedSlot.slotType)
        {
            _inventory.swapQuickSlotItem();
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

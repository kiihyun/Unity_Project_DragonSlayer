using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : BaseWindow
{
    [SerializeField] private  GameObject _tooltipUI;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotsParent;

    [Header("Inventory")] 
    [SerializeField] private int minSlotCount = 16; 
    public InventorySlot selectedSlot;
    
    [SerializeField] private List<InventorySlot> _slots = new List<InventorySlot>();
    [SerializeField] private List<ItemData> _itemList = new List<ItemData>();
    private Queue<InventorySlot> _slotPool = new Queue<InventorySlot>();
    public override UIType UIType => UIType.UIInventory;

    private void Start()
    {
        // _itemList에 인벤토리 연결해주기
    }
    public override void OnOpen(OpenParam param)
    {
        // UpdateUI();
    }

    public override void OnClose()
    {
        // UpdateUI();
    }
    
    // 인벤토리에 아이템 추가
    public void AddItem(ItemData item)
    {
        _itemList.Add(item);
    }
    
    //인벤토리에서 아이템 제거
    public void RemoveItem(ItemData item)
    {
        _itemList.Remove(item);
    }

    // Inventory 업데이트
    public void UpdateUI()
    {
        // 이전에 선택된 아이템 기억 ( 존재한다면 )
        ItemData prevSelectedData = selectedSlot != null ? selectedSlot.data : null;
        
        // 기존 슬롯 모두 풀에 반환
        foreach (var slot in _slots)
        {
            slot.gameObject.SetActive(false);
            _slotPool.Enqueue(slot);
        }
        _slots.Clear();
        selectedSlot = null;
        
        // 슬롯 다시 생성, 필요한 만큼 슬롯 재사용 또는 새로 생성
        int slotCount = Mathf.Max(_itemList.Count, minSlotCount);
        for (int i = 0; i < slotCount; i++)
        {
            InventorySlot newSlot;
            // 풀에 아직 slot이 남아 있을 경우 풀에서 꺼내기
            if (_slotPool.Count > 0)
            {
                newSlot = _slotPool.Dequeue();
                newSlot.data = _itemList[i];
            }
            // 풀에 slot이 없다면 새로 생성
            else
            {
                GameObject newSlotObj = Instantiate(_slotPrefab, _slotsParent);
                newSlot = newSlotObj.GetComponent<InventorySlot>();
            }
            
            // 아이템 데이터가 있다면 데이터 할당, 없다면 null
            if (i < _itemList.Count)
            {
                newSlot.data = _itemList[i];
            }
            else
            {
                newSlot.data = null;
            }
            
            // 이전 선택 아이템 복원
            if (prevSelectedData != null && newSlot.data == prevSelectedData)
            {
                selectedSlot = newSlot;
            }
            
            // 리스트에 넣기
            _slots.Add(newSlot);
            
        }


        /*

        // 슬롯 전체 초기화
        foreach (var slot in _slots)
        {
            Destroy(slot.gameObject);
        }
        _slots.Clear();
        selectedSlot = null;

        // 슬롯 다시 생성, 인벤토리 내 아이템 갯수만큼 생성하되 최소 갯수 설정
        int slotCount = Mathf.Max(_itemList.Count, minSlotCount);
        for (int i = 0; i < _itemList.Count; i++)
        {
            GameObject newSlotObj = Instantiate(_slotPrefab, _slotsParent);
            InventorySlot newSlot = newSlotObj.GetComponent<InventorySlot>();
            newSlot.data = _itemList[i];

            // 인벤토리에 아이템을 장착중인지 확인해서 bool값 slot에 넘겨주기
            // 추가 예정


            newSlot.Set();

            // 이전에 선택했던 아이템이면 selectedSlot으로 다시 지정
            if (prevSelectedData != null && newSlot.data == prevSelectedData)
            {
                selectedSlot = newSlot;
            }

            _slots.Add(newSlot);
        }
        */



    }
    
    
    
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIEquipItem : BaseWindow
{
    // [SerializeField] private GameObject _tooltipUI;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotsParent;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    
    [Header("Inventory")]
    [SerializeField] private int minSlotCount = Constants.UI.UIEQUIPMENT_MAXSLOT;
    
    [SerializeField] private List<InventorySlot> _slots = new List<InventorySlot>();
    [SerializeField] private List<ItemData> _itemList = new List<ItemData>();
    [SerializeField] private InventorySlot _equipSlot_0;
    [SerializeField] private InventorySlot _equipSlot_1;
    private Queue<InventorySlot> _slotPool = new Queue<InventorySlot>();
    
    public Inventory inventory { get; private set; }
    
    public override UIType UIType => UIType.UIEquipItem;
    
    // 인벤토리 UI가 열릴 때 UI 갱신
    public override void OnOpen(OpenParam param)
    {
        if (inventory == null)
        {
            return;
        }
        UpdateUI();
    }

    // 인벤토리 UI가 닫힐 때 UI 갱신
    public override void OnClose()
    {
        UpdateUI();
    }
    
    private void OnEnable()
    {
        inventory = UIManager.instance.inventory;
        if (inventory != null)
        {
            inventory.InventoryUpdate += UpdateUI;
        }
        UpdateUI();
    }

    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.InventoryUpdate -= UpdateUI;
        }

        foreach (var slot in _slots)
        {
            slot.gameObject.SetActive(false);
            slot.data = null;
            _slotPool.Enqueue(slot);
        }
        _slots.Clear();
    }

    // UIEquipItem 업데이트
    public void UpdateUI()
    {
        // Inventory의 EquipableItems를 _itemList에 복사하여 저장
        _itemList = inventory.EquipableItems.ToList();
        
        // 슬롯 다시 생성, 필요한 만큼 슬롯 재사용 또는 새로 생성
        int slotCount = Mathf.Max(_itemList.Count, minSlotCount);

        if (_slots.Count == 0 || _itemList.Count > minSlotCount)
        {
            foreach (var slot in _slots)
            {
                slot.gameObject.SetActive(false);
                _slotPool.Enqueue(slot);
            }
            _slots.Clear();

            // 슬롯 생성 또는 풀에서 꺼내기
            for (int i = 0; i < slotCount; i++)
            {
                InventorySlot newSlot;
                if (_slotPool.Count > 0)
                {
                    newSlot = _slotPool.Dequeue();
                }
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

                newSlot.Set(newSlot.data);
                newSlot.gameObject.SetActive(true);
                newSlot.uiEquipItem = this;
                
                // 슬롯이 만들어지면 slotType을 Normalslot으로 지정
                newSlot.slotType = SlotType.NormalSlot;
                
                // 리스트에 넣기
                _slots.Add(newSlot);
            }
        }
        else
        {
            // 슬롯 개수는 충분할 때, 슬롯 데이터만 갱신
            for (int i = 0; i < _slots.Count; i++)
            {
                if (i < _itemList.Count)
                {
                    _slots[i].Set(_itemList[i]);
                }
                else
                {
                    _slots[i].Set(null);
                }
                _slots[i].gameObject.SetActive(true);
                _slots[i].uiEquipItem = this;
            }
        }

        updateSlot();

    }
    
    private void updateSlot()
    {
        // 0번 퀵슬롯
        if (inventory.EquipSlotItems[0] != null)
        {
            _equipSlot_0.data = inventory.EquipSlotItems[0];
            _equipSlot_0.itemIcon.sprite = inventory.EquipSlotItems[0].ItemIcon;
            _equipSlot_0.itemIcon.enabled = true;
        }
        else
        {
            _equipSlot_0.data = null;
            _equipSlot_0.itemIcon.sprite = null;
            _equipSlot_0.itemIcon.enabled = false;
        }

        // 1번 퀵슬롯
        if (inventory.EquipSlotItems[1] != null)
        {
            _equipSlot_1.data = inventory.EquipSlotItems[1];
            _equipSlot_1.itemIcon.sprite = inventory.EquipSlotItems[1].ItemIcon;
            _equipSlot_1.itemIcon.enabled = true;
        }
        else
        {
            _equipSlot_1.data = null;
            _equipSlot_1.itemIcon.sprite = null;
            _equipSlot_1.itemIcon.enabled = false;
        }
    }
}

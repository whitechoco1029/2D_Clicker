using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventory : MonoBehaviour
{
    public Slot[] slots;  // 아이템 슬롯 배열
    public GameObject inventoryWindow;  // 인벤토리 창
    public Transform SlotPanel;  // 슬롯이 배치된 패널
    public Slot equipSlots;

    void Start()
    {
        inventoryWindow.SetActive(true);  // 인벤토리 창을 활성화
        slots = new Slot[SlotPanel.childCount];  // 슬롯 개수에 맞게 배열 크기 설정

        // 각 슬롯에 실제 Slot 오브젝트를 할당
        for (int i = 0; i < SlotPanel.childCount; i++)
        {
            slots[i] = SlotPanel.GetChild(i).GetComponent<Slot>();  // 각 슬롯에 ItemSlot 컴포넌트 연결
        }

        // 인벤토리 UI 초기화
        InitializeInventoryUI();
    }

    // 인벤토리 UI 초기화 작업
    void InitializeInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot(); 
        }
    }

    // 인벤토리에 아이템 추가
    public void AddItemToInventory(WeaponData weapon)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty())  // 빈 슬롯을 찾으면
            {
                slots[i].SetWeapon(weapon); // 아이템을 슬롯에 설정
                break;
            }
        }
    }
}

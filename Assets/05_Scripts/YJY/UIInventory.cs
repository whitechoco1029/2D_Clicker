using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventory : MonoBehaviour
{
    public Slot[] slots;  // ������ ���� �迭
    public GameObject inventoryWindow;  // �κ��丮 â
    public Transform SlotPanel;  // ������ ��ġ�� �г�
    public Slot equipSlots;

    void Start()
    {
        inventoryWindow.SetActive(true);  // �κ��丮 â�� Ȱ��ȭ
        slots = new Slot[SlotPanel.childCount];  // ���� ������ �°� �迭 ũ�� ����

        // �� ���Կ� ���� Slot ������Ʈ�� �Ҵ�
        for (int i = 0; i < SlotPanel.childCount; i++)
        {
            slots[i] = SlotPanel.GetChild(i).GetComponent<Slot>();  // �� ���Կ� ItemSlot ������Ʈ ����
        }

        // �κ��丮 UI �ʱ�ȭ
        InitializeInventoryUI();
    }

    // �κ��丮 UI �ʱ�ȭ �۾�
    void InitializeInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot(); 
        }
    }

    // �κ��丮�� ������ �߰�
    public void AddItemToInventory(WeaponData weapon)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty())  // �� ������ ã����
            {
                slots[i].SetWeapon(weapon); // �������� ���Կ� ����
                break;
            }
        }
    }
}

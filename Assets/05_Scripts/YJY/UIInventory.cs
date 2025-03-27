using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject inventoryWindow;
    public Transform SlotPanel;

    void Start()
    {
        inventoryWindow.SetActive(true);
        slots = new ItemSlot[SlotPanel.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

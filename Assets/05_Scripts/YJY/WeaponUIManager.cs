using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

//인벤토리 시스템이 있어야 무기 아이템을 보관하고 관리에 용이할듯함
//무기아이템을 드래그하여 합성할 수 있는 기능 추가해야됨
public class WeaponUIManager : MonoBehaviour
{
    public GameObject weaponSlotPrefab;   // 무기 슬롯 프리팹
    public Transform inventoryPanel;      // 인벤토리 패널
    public List<Weapon> inventory = new List<Weapon>();  // 무기 목록
    public Button generateWeaponButton; //무기 생성 버튼

    private Weapon firstWeapon = null;    // 첫 번째 선택된 무기
    private Weapon secondWeapon = null;   // 두 번째 선택된 무기

    void Start()
    {
        // 예시로 인벤토리에 무기 추가
        for (int i = 1; i <= 10; i++)
        {
            Weapon weapon = new Weapon(i);
            inventory.Add(weapon);
        }

        // 인벤토리 UI 초기화
        PopulateInventory();

        // 무기 생성 버튼 이벤트 연결
        generateWeaponButton.onClick.AddListener(GenerateWeapon);
    }
    void PopulateInventory() // 인벤토리 UI에 무기 슬롯을 동적으로 생성
    {
        // 기존의 UI 항목을 모두 삭제 (중복 방지)
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // 무기 데이터를 기반으로 슬롯을 생성하여 UI에 표시
        foreach (Weapon weapon in inventory)
        {
            GameObject weaponSlot = Instantiate(weaponSlotPrefab, inventoryPanel);
            WeaponSlot slot = weaponSlot.GetComponent<WeaponSlot>();

            slot.SetWeapon(weapon);  // 슬롯에 무기 설정
            slot.OnWeaponSelected += HandleWeaponSelection; // 무기 선택 이벤트
        }
    }

    private void HandleWeaponSelection(Weapon selectedWeapon)   // 무기 선택 처리
    {
        if (firstWeapon == null)
        {
            firstWeapon = selectedWeapon;
            Debug.Log($"First Weapon Selected: {firstWeapon.weaponName}");
        }
        else if (secondWeapon == null)
        {
            secondWeapon = selectedWeapon;
            Debug.Log($"Second Weapon Selected: {secondWeapon.weaponName}");

            // 두 무기가 선택되면 강화 진행
            UpgradeWeapons();
        }
    }

    // 무기 생성 함수
    private void GenerateWeapon()
    {
        // 무기 ID는 인벤토리 크기 + 1로 설정 (새로운 무기 ID 생성)
        int newWeaponID = inventory.Count + 1;
        Weapon newWeapon = new Weapon(newWeaponID);

        // 생성된 무기를 인벤토리에 추가
        inventory.Add(newWeapon);

        Debug.Log($"Generated New Weapon: {newWeapon.weaponName}");

        // UI 갱신
        RefreshInventoryUI();
    }

    // 무기 강화 함수
    private void UpgradeWeapons()
    {
        if (firstWeapon != null && secondWeapon != null)
        {
            // 두 무기를 결합하여 새로운 무기 생성 (ID + 1 증가)
            Weapon upgradedWeapon = new Weapon(firstWeapon.weaponID + 1);
            upgradedWeapon.WeaponPower = (firstWeapon.WeaponPower + secondWeapon.WeaponPower) / 2;

            Debug.Log($"Upgraded to: {upgradedWeapon.weaponName} with Attack Power: {upgradedWeapon.WeaponPower}");

            // 강화된 무기를 인벤토리에 추가하고, 두 무기는 제거
            inventory.Add(upgradedWeapon);
            inventory.Remove(firstWeapon);
            inventory.Remove(secondWeapon);

            // UI 갱신
            RefreshInventoryUI();

            // 선택된 무기 초기화
            firstWeapon = null;
            secondWeapon = null;
        }
    }

    // 인벤토리 UI 갱신
    void RefreshInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);  // 기존 슬롯 제거
        }

        PopulateInventory();  // 다시 UI 갱신
    }
}


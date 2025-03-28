using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Xml.Linq;

//인벤토리 시스템이 있어야 무기 아이템을 보관하고 관리에 용이할듯함
//무기아이템을 드래그하여 합성할 수 있는 기능 추가해야됨
public class WeaponUIManager : MonoBehaviour
{
    public GameObject slotPrefab; //무기슬롯 프리팹
    public Transform inventoryPanel; //인벤토리 패널
    public Button generateWeaponButton; //무기생성 버튼

    private Weapon firstWeapon = null; //첫번째 선택된 무기
    private Weapon secondWeapon = null; //두번쨰 선택된 무기

    private List<Weapon> inventory = new List<Weapon>();//무기 목록
 

    void Start()
    {
        generateWeaponButton.onClick.AddListener(GenerateWeapon);
        PopulateInventory();
    }

    void GenerateWeapon()
    {
        //무기 ID는 인벤토리 크기 +1로 설정 - 인벤토리 내 갯수+1하여 새로운 id받아오기
        int newWeaponID = inventory.Count + 1;
        Weapon newWeapon = new Weapon(newWeaponID);

        //생성된 무기 인벤토리에 추가하기
        inventory.Add(newWeapon);

        //UI갱신
        AddWeaponToInventoryUI(newWeapon);
    }
    //인벤토리 ui에 무기 슬롯 추가
    void AddWeaponToInventoryUI(Weapon newWeapon)
    {
        GameObject slotObject = Instantiate(slotPrefab, inventoryPanel);  // Slot 생성
        Slot slot = slotObject.GetComponent<Slot>();
        slot.SetWeapon(newWeapon);  // 슬롯에 무기 설정
        slot.OnSlotSelected += HandleWeaponSelection;  // 무기 선택 이벤트 연결
        slot.OnSlotDropped += HandleWeaponDrop;  // 드래그 앤 드롭 이벤트 연결
    }

    // 무기 선택 처리
    private void HandleWeaponSelection(Slot selectedSlot)
    {
        Weapon selectedWeapon = selectedSlot.GetWeapon();

        // 첫 번째 무기 선택
        if (firstWeapon == null)
        {
            firstWeapon = selectedWeapon;
        }
        // 두 번째 무기 선택 후 합성
        else if (secondWeapon == null)
        {
            secondWeapon = selectedWeapon;
            UpgradeWeapons();
        }
    }

    // 무기 합성 처리
    private void UpgradeWeapons()
    {
        if (firstWeapon != null && secondWeapon != null)
        {
            if (firstWeapon.weaponID == secondWeapon.weaponID)  // 동일한 무기인지 확인
            {
                int newWeaponID = firstWeapon.weaponID + 1;
                int newWeaponPower = newWeaponID * 10;

                // 새로운 무기 생성
                Weapon upgradedWeapon = new Weapon(newWeaponID);
                upgradedWeapon.WeaponPower = newWeaponPower;

                // 무기 목록에서 두 무기 제거하고 새로운 무기 추가
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
    }

    // UI 갱신
    void RefreshInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);  // 기존 UI 항목 삭제
        }

        PopulateInventory();  // UI 새로 고침
    }

    // 인벤토리 UI에 있는 모든 무기 추가
    void PopulateInventory()
    {
        foreach (Weapon weapon in inventory)
        {
            AddWeaponToInventoryUI(weapon);  // UI에 무기 슬롯 추가
        }
    }
    // 드래그 앤 드롭 처리
    private void HandleWeaponDrop(Slot droppedSlot)
    {
        // 드롭된 슬롯에서 무기를 가져와서 처리
        Weapon droppedWeapon = droppedSlot.GetWeapon();
        // 추가적인 드래그 앤 드롭 로직 처리(다른기능넣을거면)
        Debug.Log($"드롭된 무기: {droppedWeapon.weaponName}");
    }
}
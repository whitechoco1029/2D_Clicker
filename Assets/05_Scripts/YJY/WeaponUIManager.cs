using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Xml.Linq;

//인벤토리 시스템이 있어야 무기 아이템을 보관하고 관리에 용이할듯함
//무기아이템을 드래그하여 합성할 수 있는 기능 추가해야됨
public class WeaponUIManager : MonoBehaviour
{
    public Button generateWeaponButton; //무기생성 버튼

    public List<Slot> slots = new List <Slot>();
    public WeaponData currentdata; //데이터 불러오기 -1레벨 무기...
    public Slot firstWeapon = null; //첫번째 선택된 무기
    public Slot secondWeapon = null; //두번쨰 선택된 무기

    private List<WeaponData> inventory = new List<WeaponData>();//무기 목록
 

    void Start()
    {
       
        generateWeaponButton.onClick.AddListener(spawnWeapon);

        // 각 슬롯에 실제 Slot 오브젝트를 할당
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            slots.Add (transform.GetChild(0).GetChild(i).GetComponent<Slot>());  // 각 슬롯에 ItemSlot 컴포넌트 연결
        }
    }

    // 무기 합성 처리
    public void UpgradeWeapons()
    {
        if (firstWeapon != null && secondWeapon != null)
        {
            if (firstWeapon.weapondata.weaponID == secondWeapon.weapondata.weaponID)  // 동일한 무기인지 확인
            {
                int newWeaponID = firstWeapon.weapondata.weaponID + 1;

                // 새로운 무기 생성
                WeaponData upgradedWeapon = GetWeaponData(newWeaponID);

                // 선택된 무기 초기화              
                firstWeapon.SetWeapon(null);
                secondWeapon.SetWeapon(upgradedWeapon);
            }
        }
    }
 
    public void spawnWeapon()
    {

        Slot select = GetEmptySlot();
        if (select != null)
        {
            
            select.SetWeapon(currentdata);
        }

    }
    private Slot GetEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].weapondata == null)
            {
                return slots[i];
            }

        }
        return null; //빈칸이 없을때                        
    }
    private WeaponData GetWeaponData(int ID)
    {
        for (int i = 0; i < inventory.Count; i++)

        {
            if (inventory[i].weaponID == ID)
            {
                return inventory[i];
            }
        }
        return null;
    }
}
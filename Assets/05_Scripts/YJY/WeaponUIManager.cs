using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Xml.Linq;
using TMPro;

//인벤토리 시스템이 있어야 무기 아이템을 보관하고 관리에 용이할듯함
//무기아이템을 드래그하여 합성할 수 있는 기능 추가해야됨
public class WeaponUIManager : MonoBehaviour
{
    public Button generateWeaponButton; //무기생성 버튼

    public List<Slot> slots = new List <Slot>();
    public WeaponData currentdata; //데이터 불러오기 -1레벨 무기...
    public Slot firstWeapon = null; //첫번째 선택된 무기
    public Slot secondWeapon = null; //두번쨰 선택된 무기
    public Transform frontSlotTransform;
    public List<WeaponData> inventory = new List<WeaponData>();//무기 목록

    // LJH - ItemCooltime
    [Header("Item Info")]
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI textQuantity;
    [SerializeField] float rechargeTime;
    [SerializeField] int maxQuantity;
    float elapsed;
    int quantity;

    void Start()
    {
       
        generateWeaponButton.onClick.AddListener(spawnWeapon);

        // 각 슬롯에 실제 Slot 오브젝트를 할당
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            slots.Add (transform.GetChild(0).GetChild(i).GetComponent<Slot>());  // 각 슬롯에 ItemSlot 컴포넌트 연결
        }

        UpdateTextQuantity();
    }

    private void Update()
    {
        RechargeWeapon();
    }

    public void HandleDrop()
    {
        if (firstWeapon != null && secondWeapon != null)
        {
            // 1. 드롭 시 시작 슬롯과 같은 슬롯 내부에 있을 때 다시 원래 자리로 돌아올 수 있도록
            if (firstWeapon == secondWeapon)
            {
                Debug.Log("드롭 실패: 같은 슬롯으로 돌아갑니다.");
                // 같은 슬롯이라면 원래 자리로 무기 돌려놓기               
                firstWeapon.FixImagePosition();
                return;
            }

            // 2. 드롭 시 시작 슬롯과 같은 슬롯이 아니고 무기 데이터가 없다면 무기 데이터와 이미지가 옮겨갈 수 있도록
            if (secondWeapon.IsEmpty())
            {
                Debug.Log("무기 이동: 빈 슬롯으로 아이템 이동");
                secondWeapon.SetWeapon(firstWeapon.weapondata);
                firstWeapon.ClearSlot();  // 첫 번째 슬롯 비우기
                firstWeapon.FixImagePosition();
               
                return;
            }

            // 3. 드롭 시 시작 슬롯과 같은 슬롯이 아니고 무기 데이터가 있지만 무기 데이터가 같지 않을 경우 두 무기의 위치를 바꿔준다
            if (secondWeapon.weapondata != firstWeapon.weapondata)
            {
                Debug.Log("무기 위치 변경: 두 무기의 위치를 바꿈");
                WeaponData tempWeapon = secondWeapon.weapondata;
                secondWeapon.SetWeapon(firstWeapon.weapondata);
                firstWeapon.SetWeapon(tempWeapon);
                firstWeapon.FixImagePosition(); 
                
                return;
            }

            // 4. 드롭 시 시작 슬롯과 같은 슬롯이 아니고 무기 데이터가 있고 무기 데이터가 같을 경우 합성이 일어난다
            if (secondWeapon.weapondata.weaponID == firstWeapon.weapondata.weaponID)
            {
                Debug.Log("무기 합성 진행");
                UpgradeWeapons();
                firstWeapon.FixImagePosition();
            }
        }
        else
        {
            if(firstWeapon != null)
            {
                firstWeapon.FixImagePosition();
            }
 
        }
        firstWeapon = null;
        secondWeapon = null;
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
                secondWeapon.SetWeapon(upgradedWeapon);
                firstWeapon.ClearSlot();
            }
        }
    }
 
    public void spawnWeapon()
    {
        if (quantity <= 0)
            return;

        Slot select = GetEmptySlot();
        if (select != null)
        {
            quantity--;
            select.SetWeapon(currentdata);
            UpdateTextQuantity();
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

    private void RechargeWeapon()
    {
        if (quantity >= maxQuantity)
            return;

        elapsed += Time.deltaTime;

        if (rechargeTime <= elapsed)
        {
            quantity++;
            elapsed = rechargeTime;
            UpdateTextQuantity();
        }

        icon.fillAmount = elapsed / rechargeTime;
        if (elapsed == rechargeTime)
            elapsed = 0;
    }

    private void UpdateTextQuantity()
    {
        textQuantity.text = $"{quantity} / {maxQuantity}";
    }
}
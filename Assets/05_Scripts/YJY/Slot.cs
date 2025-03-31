using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image iconImage;
    public WeaponData weapondata;  // 해당 슬롯에 저장된 무기
    public event System.Action<Slot> OnSlotSelected;  // 슬롯 선택 이벤트
    public event System.Action<Slot> OnSlotDropped;   // 슬롯에 드래그 앤 드롭 이벤트
    private WeaponUIManager weaponUIManager;
    private Slot originalSlot; // 아이템이 드래그되기 전의 원래 슬롯

    void Awake()
    {
        weaponUIManager = transform.parent.parent.GetComponent<WeaponUIManager>();
        if (weapondata == null)
        {
            iconImage.gameObject.SetActive(false);  // 이미지 비활성화
        }
    }

    // 슬롯에 무기 설정
    public void SetWeapon(WeaponData newWeapon)
    {
        if (newWeapon == null)
        {
            ClearSlot();
            iconImage.gameObject.SetActive(false);
        }
        else
        {
            iconImage.gameObject.SetActive(true);
            weapondata = newWeapon;
            itemNameText.text = weapondata.displayName;
            iconImage.sprite = newWeapon.icon;
        }
    }

    // 슬롯을 비우는 함수
    public void ClearSlot()
    {
        weapondata = null;
        iconImage.sprite = null; // 아이콘 지우기
    }

    // 슬롯이 비어있는지 여부를 반환
    public bool IsEmpty()
    {
        return weapondata == null;
    }
    // 드래그 시작
    public void OnBeginDrag()
    {
        if (IsEmpty()) return;// 아이템이 없으면 드래그 시작 안 함
        weaponUIManager.firstWeapon = this;
        originalSlot = this;
    }

    // 드래그 중
    public void OnDrag()
    {
        iconImage.transform.position = Input.mousePosition;  // 드래그하는 동안 위치 업데이트
    }
    public void pointerEnter()
    {
        if (weaponUIManager.firstWeapon != null)
        {
            weaponUIManager.secondWeapon = this;
        }

    }
    public void PointerExit()
    {
        weaponUIManager.secondWeapon = null;
    }

    // 드래그 종료
    public void OnDrop()
    {
        // 드롭이 유효한 경우에만 업그레이드
        if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null)
        {
            weaponUIManager.UpgradeWeapons();  // 무기 합성 처리
        }
        else
        {
            // 드롭이 실패했을 때 원래 슬롯으로 돌아감
            if (originalSlot != null)
            {
                originalSlot.SetWeapon(weapondata);  // 원래 슬롯으로 아이템을 되돌림
            }
        }
        //weaponUIManager.UpgradeWeapons();
    }

    // 슬롯에 저장된 무기 반환
    public WeaponData GetWeapon()
    {
        return weapondata;
    }
}
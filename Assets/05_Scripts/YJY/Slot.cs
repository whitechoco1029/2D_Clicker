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
    //private Slot originalSlot; // 아이템이 드래그되기 전의 원래 슬롯
    //private Slot chooseSlot;
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
    }

    // 드래그 중
    public void OnDrag()
    {
        iconImage.transform.position = Input.mousePosition;  // 드래그하는 동안 위치 업데이트
    }
    //public void pointerEnter()
    //{
    //    if (weaponUIManager.firstWeapon != null)
    //    {
    //        weaponUIManager.secondWeapon = this;
    //    }

    //}
    //public void PointerExit()
    //{
    //    weaponUIManager.secondWeapon = null;
    //}

    // 드래그 종료
    public void OnDrop()
    {
         weaponUIManager.secondWeapon = this;  // 드롭할 슬롯을 secondWeapon에 설정
        weaponUIManager.HandleDrop();  // 드롭 후 처리 (WeaponUIManager에서 예외 처리)
        //chooseSlot = weaponUIManager.secondWeapon;
        //// 1.시작 슬롯과 같은 슬롯 내부에 있을 때 다시 원래 자리로 돌아올 수 있도록
        //if (chooseSlot == originalSlot)
        //{
        //    Debug.Log("같은 슬롯으로 돌아감");
        //    originalSlot.SetWeapon(weapondata);  // 원래 슬롯으로 아이템을 되돌림
        //    originalSlot.iconImage.rectTransform.anchoredPosition = Vector2.zero;
        //    originalSlot.iconImage.gameObject.SetActive(true);
        //    return;
        //}

        //// 2.시작 슬롯과 같은 슬롯이 아니고 무기 데이터가 없다면 무기 데이터와 이미지가 옮겨갈 수 있도록
        //if (chooseSlot != originalSlot && chooseSlot.weapondata == null)
        //{
        //    // 무기 데이터와 이미지 옮겨가기
        //    chooseSlot.SetWeapon(weaponUIManager.firstWeapon.weapondata);  // firstWeapon의 데이터를 chooseSlot으로 이동
        //    originalSlot.ClearSlot(); // 원래 슬롯 비우기
        //    originalSlot.iconImage.gameObject.SetActive(false);
        //    Debug.Log("빈 슬롯으로 아이템 이동");
        //    return;
        //}

        //// 3. 드롭 시 시작 슬롯과 같은 슬롯이 아니고 무기 데이터가 있지만 무기 데이터가 같지 않을 경우 두 무기의 위치를 바꿔준다
        //if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null && weaponUIManager.firstWeapon.weapondata != weaponUIManager.secondWeapon.weapondata)
        //{
           
        //    Debug.Log("두 무기의 위치를 바꿈");
        //    return;
        //}

        //// 4. 드롭 시 시작 슬롯과 같은 슬롯이 아니고 무기 데이터가 있고 무기 데이터가 같을 경우 합성이 일어난다
        //if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null && weaponUIManager.firstWeapon.weapondata.weaponID == weaponUIManager.secondWeapon.weapondata.weaponID)
        //{
        //    // 무기 합성 처리
        //    Debug.Log("무기 합성 진행");
        //    weaponUIManager.UpgradeWeapons();
        //    return;
        //}

        //  (드롭 실패)
        //Debug.Log($"1슬롯:{weaponUIManager.firstWeapon},2슬롯:{weaponUIManager.secondWeapon}");
        //// 드롭이 유효한 경우에만 업그레이드
        //if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null)
        //{
        //    Debug.Log("무기합성");
        //    weaponUIManager.UpgradeWeapons();  // 무기 합성 처리
        //}
        //else
        //{
        //    // 드롭이 실패했을 때 원래 슬롯으로 돌아감
        //    if (originalSlot != null)
        //    {
        //        Debug.Log("드롭실패");
        //        originalSlot.SetWeapon(weapondata);  // 원래 슬롯으로 아이템을 되돌림
        //    }
        //}
        //Debug.Log("뭔데");

    }

    //// 슬롯에 저장된 무기 반환
    //public WeaponData GetWeapon()
    //{
    //    return weapondata;
    //}
}
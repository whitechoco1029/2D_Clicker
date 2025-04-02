using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image iconImage;
    public WeaponData weapondata = null;  // 해당 슬롯에 저장된 무기
  //  public event System.Action<Slot> OnSlotSelected;  // 슬롯 선택 이벤트
  //  public event System.Action<Slot> OnSlotDropped;   // 슬롯에 드래그 앤 드롭 이벤트
    private WeaponUIManager weaponUIManager;
    private Vector3 originalScale;
    public Transform frontSlot;

    void Awake()
    {
        // weaponUIManager = transform.parent.parent.GetComponent<WeaponUIManager>();
        //frontSlot = weaponUIManager.frontSlotTransform;
        // LJH
        if (transform.parent.parent.TryGetComponent(out WeaponUIManager manager))
            weaponUIManager = manager;
        else
            weaponUIManager = transform.parent.GetComponent<WeaponUIManager>();

        frontSlot = weaponUIManager.frontSlotTransform;

        if (frontSlot == null)
        {
            Debug.LogError("frontSlot을 찾을 수 없음");
        }
        if (weapondata == null)
        {
            iconImage.gameObject.SetActive(false);  // 이미지 비활성화
            itemNameText.gameObject.SetActive(false);
        }
        else
        {
            UpdateSlotWithWeaponData();  // 무기 데이터가 있으면 업데이트
        }
        originalScale = iconImage.rectTransform.localScale;
    }
    private void UpdateSlotWithWeaponData()
    {
        if (weapondata != null)
        {
            itemNameText.text = $"{weapondata.weaponID}";  // 무기 ID 텍스트 업데이트
            iconImage.sprite = weapondata.icon;  // 무기 아이콘 설정
        }
    }

    // 슬롯에 무기 설정
    public void SetWeapon(WeaponData newWeapon)
    {
        weapondata = newWeapon;

        if (newWeapon == null)
        {
            ClearSlot();
          
        }
        else
        {
            iconImage.gameObject.SetActive(true);
            itemNameText.gameObject.SetActive(true);
            UpdateSlotWithWeaponData();
        }
    }

    // 슬롯을 비우는 함수
    public void ClearSlot()
    {
        weapondata = null;
        iconImage.sprite = null; // 아이콘 지우기
        itemNameText.text = "";  // 텍스트도 지우기
        iconImage.gameObject.SetActive(false);  // 이미지 비활성화
        itemNameText.gameObject.SetActive(false);  // 텍스트 비활성화
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
        iconImage.rectTransform.localScale = originalScale * 1.4f; //크기키우기
        if (frontSlot != null)
        {
            iconImage.transform.SetParent(frontSlot.transform);  // frontSlot의 자식으로 설정
            iconImage.rectTransform.localPosition = Vector3.zero;  // frontSlot 내에서 위치 초기화
        }
        else
        {
            Debug.LogError("frontSlot이 할당되지 않음");
        }
    }

    // 드래그 중
    public void OnDrag()
    {
        iconImage.transform.position = Input.mousePosition;  // 드래그하는 동안 위치 업데이트
    }

    public void PointEnter()
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
        //weaponUIManager.secondWeapon = this;  // 드롭할 슬롯을 secondWeapon에 설정
        weaponUIManager.HandleDrop();  // 드롭 후 처리 (WeaponUIManager에서 예외 처리)
       
        
       
    }
    public void FixImagePosition()
    {
        iconImage.transform.SetParent(transform);
        iconImage.rectTransform.anchoredPosition = Vector2.zero; // 이미지의 RectTransform 위치를 고정
        iconImage.rectTransform.localPosition = Vector3.zero;
        iconImage.rectTransform.localScale = originalScale;
    }
}
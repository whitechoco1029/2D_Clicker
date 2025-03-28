using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI attackPowerText;
    public Button slotButton;

    public Weapon weapon;  // 해당 슬롯에 저장된 무기
    public event System.Action<Slot> OnSlotSelected;  // 슬롯 선택 이벤트
    public event System.Action<Slot> OnSlotDropped;   // 슬롯에 드래그 앤 드롭 이벤트

    private CanvasGroup canvasGroup;  // 드래그할 때 사용할 캔버스 그룹

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // 슬롯에 무기 설정
    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        itemNameText.text = weapon.weaponName;
        attackPowerText.text = $"Power: {weapon.WeaponPower}";

        // 버튼 클릭 시 이벤트 발생
        slotButton.onClick.AddListener(() => OnSlotSelected?.Invoke(this));
    }

    // 슬롯을 비우는 함수
    public void ClearSlot()
    {
        weapon = null;
        itemNameText.text = "";  // UI에서 텍스트 지우기
        attackPowerText.text = "";
    }

    // 슬롯이 비어있는지 여부를 반환
    public bool IsEmpty()
    {
        return weapon == null;
    }
    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;  // 드래그 시작 시 Raycast 차단
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;  // 드래그하는 동안 위치 업데이트
    }

    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;  // 드래그 종료 후 Raycast 활성화
    }

    // 드롭된 아이템 처리
    public void OnDrop(PointerEventData eventData)
    {
        OnSlotDropped?.Invoke(this);  // 슬롯에 아이템이 드롭되었을 때 이벤트 발생
    }

    // 슬롯에 저장된 무기 반환
    public Weapon GetWeapon()
    {
        return weapon;
    }
}

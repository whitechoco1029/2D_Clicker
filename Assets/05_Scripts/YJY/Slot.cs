using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI attackPowerText;
    public Button slotButton;

    public Weapon weapon;  // �ش� ���Կ� ����� ����
    public event System.Action<Slot> OnSlotSelected;  // ���� ���� �̺�Ʈ
    public event System.Action<Slot> OnSlotDropped;   // ���Կ� �巡�� �� ��� �̺�Ʈ

    private CanvasGroup canvasGroup;  // �巡���� �� ����� ĵ���� �׷�

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // ���Կ� ���� ����
    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        itemNameText.text = weapon.weaponName;
        attackPowerText.text = $"Power: {weapon.WeaponPower}";

        // ��ư Ŭ�� �� �̺�Ʈ �߻�
        slotButton.onClick.AddListener(() => OnSlotSelected?.Invoke(this));
    }

    // ������ ���� �Լ�
    public void ClearSlot()
    {
        weapon = null;
        itemNameText.text = "";  // UI���� �ؽ�Ʈ �����
        attackPowerText.text = "";
    }

    // ������ ����ִ��� ���θ� ��ȯ
    public bool IsEmpty()
    {
        return weapon == null;
    }
    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;  // �巡�� ���� �� Raycast ����
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;  // �巡���ϴ� ���� ��ġ ������Ʈ
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;  // �巡�� ���� �� Raycast Ȱ��ȭ
    }

    // ��ӵ� ������ ó��
    public void OnDrop(PointerEventData eventData)
    {
        OnSlotDropped?.Invoke(this);  // ���Կ� �������� ��ӵǾ��� �� �̺�Ʈ �߻�
    }

    // ���Կ� ����� ���� ��ȯ
    public Weapon GetWeapon()
    {
        return weapon;
    }
}

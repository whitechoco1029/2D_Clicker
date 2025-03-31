using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image iconImage;
    public WeaponData weapondata;  // �ش� ���Կ� ����� ����
    public event System.Action<Slot> OnSlotSelected;  // ���� ���� �̺�Ʈ
    public event System.Action<Slot> OnSlotDropped;   // ���Կ� �巡�� �� ��� �̺�Ʈ
    private WeaponUIManager weaponUIManager;
    private Slot originalSlot; // �������� �巡�׵Ǳ� ���� ���� ����

    void Awake()
    {
        weaponUIManager = transform.parent.parent.GetComponent<WeaponUIManager>();
        if (weapondata == null)
        {
            iconImage.gameObject.SetActive(false);  // �̹��� ��Ȱ��ȭ
        }
    }

    // ���Կ� ���� ����
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

    // ������ ���� �Լ�
    public void ClearSlot()
    {
        weapondata = null;
        iconImage.sprite = null; // ������ �����
    }

    // ������ ����ִ��� ���θ� ��ȯ
    public bool IsEmpty()
    {
        return weapondata == null;
    }
    // �巡�� ����
    public void OnBeginDrag()
    {
        if (IsEmpty()) return;// �������� ������ �巡�� ���� �� ��
        weaponUIManager.firstWeapon = this;
        originalSlot = this;
    }

    // �巡�� ��
    public void OnDrag()
    {
        iconImage.transform.position = Input.mousePosition;  // �巡���ϴ� ���� ��ġ ������Ʈ
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

    // �巡�� ����
    public void OnDrop()
    {
        // ����� ��ȿ�� ��쿡�� ���׷��̵�
        if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null)
        {
            weaponUIManager.UpgradeWeapons();  // ���� �ռ� ó��
        }
        else
        {
            // ����� �������� �� ���� �������� ���ư�
            if (originalSlot != null)
            {
                originalSlot.SetWeapon(weapondata);  // ���� �������� �������� �ǵ���
            }
        }
        //weaponUIManager.UpgradeWeapons();
    }

    // ���Կ� ����� ���� ��ȯ
    public WeaponData GetWeapon()
    {
        return weapondata;
    }
}
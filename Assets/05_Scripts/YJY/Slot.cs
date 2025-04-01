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
    //private Slot originalSlot; // �������� �巡�׵Ǳ� ���� ���� ����
    //private Slot chooseSlot;
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
    }

    // �巡�� ��
    public void OnDrag()
    {
        iconImage.transform.position = Input.mousePosition;  // �巡���ϴ� ���� ��ġ ������Ʈ
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

    // �巡�� ����
    public void OnDrop()
    {
         weaponUIManager.secondWeapon = this;  // ����� ������ secondWeapon�� ����
        weaponUIManager.HandleDrop();  // ��� �� ó�� (WeaponUIManager���� ���� ó��)
        //chooseSlot = weaponUIManager.secondWeapon;
        //// 1.���� ���԰� ���� ���� ���ο� ���� �� �ٽ� ���� �ڸ��� ���ƿ� �� �ֵ���
        //if (chooseSlot == originalSlot)
        //{
        //    Debug.Log("���� �������� ���ư�");
        //    originalSlot.SetWeapon(weapondata);  // ���� �������� �������� �ǵ���
        //    originalSlot.iconImage.rectTransform.anchoredPosition = Vector2.zero;
        //    originalSlot.iconImage.gameObject.SetActive(true);
        //    return;
        //}

        //// 2.���� ���԰� ���� ������ �ƴϰ� ���� �����Ͱ� ���ٸ� ���� �����Ϳ� �̹����� �Űܰ� �� �ֵ���
        //if (chooseSlot != originalSlot && chooseSlot.weapondata == null)
        //{
        //    // ���� �����Ϳ� �̹��� �Űܰ���
        //    chooseSlot.SetWeapon(weaponUIManager.firstWeapon.weapondata);  // firstWeapon�� �����͸� chooseSlot���� �̵�
        //    originalSlot.ClearSlot(); // ���� ���� ����
        //    originalSlot.iconImage.gameObject.SetActive(false);
        //    Debug.Log("�� �������� ������ �̵�");
        //    return;
        //}

        //// 3. ��� �� ���� ���԰� ���� ������ �ƴϰ� ���� �����Ͱ� ������ ���� �����Ͱ� ���� ���� ��� �� ������ ��ġ�� �ٲ��ش�
        //if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null && weaponUIManager.firstWeapon.weapondata != weaponUIManager.secondWeapon.weapondata)
        //{
           
        //    Debug.Log("�� ������ ��ġ�� �ٲ�");
        //    return;
        //}

        //// 4. ��� �� ���� ���԰� ���� ������ �ƴϰ� ���� �����Ͱ� �ְ� ���� �����Ͱ� ���� ��� �ռ��� �Ͼ��
        //if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null && weaponUIManager.firstWeapon.weapondata.weaponID == weaponUIManager.secondWeapon.weapondata.weaponID)
        //{
        //    // ���� �ռ� ó��
        //    Debug.Log("���� �ռ� ����");
        //    weaponUIManager.UpgradeWeapons();
        //    return;
        //}

        //  (��� ����)
        //Debug.Log($"1����:{weaponUIManager.firstWeapon},2����:{weaponUIManager.secondWeapon}");
        //// ����� ��ȿ�� ��쿡�� ���׷��̵�
        //if (weaponUIManager.firstWeapon != null && weaponUIManager.secondWeapon != null)
        //{
        //    Debug.Log("�����ռ�");
        //    weaponUIManager.UpgradeWeapons();  // ���� �ռ� ó��
        //}
        //else
        //{
        //    // ����� �������� �� ���� �������� ���ư�
        //    if (originalSlot != null)
        //    {
        //        Debug.Log("��ӽ���");
        //        originalSlot.SetWeapon(weapondata);  // ���� �������� �������� �ǵ���
        //    }
        //}
        //Debug.Log("����");

    }

    //// ���Կ� ����� ���� ��ȯ
    //public WeaponData GetWeapon()
    //{
    //    return weapondata;
    //}
}
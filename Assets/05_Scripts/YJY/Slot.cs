using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image iconImage;
    public WeaponData weapondata = null;  // �ش� ���Կ� ����� ����
  //  public event System.Action<Slot> OnSlotSelected;  // ���� ���� �̺�Ʈ
  //  public event System.Action<Slot> OnSlotDropped;   // ���Կ� �巡�� �� ��� �̺�Ʈ
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
            Debug.LogError("frontSlot�� ã�� �� ����");
        }
        if (weapondata == null)
        {
            iconImage.gameObject.SetActive(false);  // �̹��� ��Ȱ��ȭ
            itemNameText.gameObject.SetActive(false);
        }
        else
        {
            UpdateSlotWithWeaponData();  // ���� �����Ͱ� ������ ������Ʈ
        }
        originalScale = iconImage.rectTransform.localScale;
    }
    private void UpdateSlotWithWeaponData()
    {
        if (weapondata != null)
        {
            itemNameText.text = $"{weapondata.weaponID}";  // ���� ID �ؽ�Ʈ ������Ʈ
            iconImage.sprite = weapondata.icon;  // ���� ������ ����
        }
    }

    // ���Կ� ���� ����
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

    // ������ ���� �Լ�
    public void ClearSlot()
    {
        weapondata = null;
        iconImage.sprite = null; // ������ �����
        itemNameText.text = "";  // �ؽ�Ʈ�� �����
        iconImage.gameObject.SetActive(false);  // �̹��� ��Ȱ��ȭ
        itemNameText.gameObject.SetActive(false);  // �ؽ�Ʈ ��Ȱ��ȭ
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
        iconImage.rectTransform.localScale = originalScale * 1.4f; //ũ��Ű���
        if (frontSlot != null)
        {
            iconImage.transform.SetParent(frontSlot.transform);  // frontSlot�� �ڽ����� ����
            iconImage.rectTransform.localPosition = Vector3.zero;  // frontSlot ������ ��ġ �ʱ�ȭ
        }
        else
        {
            Debug.LogError("frontSlot�� �Ҵ���� ����");
        }
    }

    // �巡�� ��
    public void OnDrag()
    {
        iconImage.transform.position = Input.mousePosition;  // �巡���ϴ� ���� ��ġ ������Ʈ
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
    // �巡�� ����
    public void OnDrop()
    {
        //weaponUIManager.secondWeapon = this;  // ����� ������ secondWeapon�� ����
        weaponUIManager.HandleDrop();  // ��� �� ó�� (WeaponUIManager���� ���� ó��)
       
        
       
    }
    public void FixImagePosition()
    {
        iconImage.transform.SetParent(transform);
        iconImage.rectTransform.anchoredPosition = Vector2.zero; // �̹����� RectTransform ��ġ�� ����
        iconImage.rectTransform.localPosition = Vector3.zero;
        iconImage.rectTransform.localScale = originalScale;
    }
}
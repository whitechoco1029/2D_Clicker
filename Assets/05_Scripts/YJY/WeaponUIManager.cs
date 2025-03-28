using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Xml.Linq;

//�κ��丮 �ý����� �־�� ���� �������� �����ϰ� ������ �����ҵ���
//����������� �巡���Ͽ� �ռ��� �� �ִ� ��� �߰��ؾߵ�
public class WeaponUIManager : MonoBehaviour
{
    public GameObject slotPrefab; //���⽽�� ������
    public Transform inventoryPanel; //�κ��丮 �г�
    public Button generateWeaponButton; //������� ��ư

    private Weapon firstWeapon = null; //ù��° ���õ� ����
    private Weapon secondWeapon = null; //�ι��� ���õ� ����

    private List<Weapon> inventory = new List<Weapon>();//���� ���
 

    void Start()
    {
        generateWeaponButton.onClick.AddListener(GenerateWeapon);
        PopulateInventory();
    }

    void GenerateWeapon()
    {
        //���� ID�� �κ��丮 ũ�� +1�� ���� - �κ��丮 �� ����+1�Ͽ� ���ο� id�޾ƿ���
        int newWeaponID = inventory.Count + 1;
        Weapon newWeapon = new Weapon(newWeaponID);

        //������ ���� �κ��丮�� �߰��ϱ�
        inventory.Add(newWeapon);

        //UI����
        AddWeaponToInventoryUI(newWeapon);
    }
    //�κ��丮 ui�� ���� ���� �߰�
    void AddWeaponToInventoryUI(Weapon newWeapon)
    {
        GameObject slotObject = Instantiate(slotPrefab, inventoryPanel);  // Slot ����
        Slot slot = slotObject.GetComponent<Slot>();
        slot.SetWeapon(newWeapon);  // ���Կ� ���� ����
        slot.OnSlotSelected += HandleWeaponSelection;  // ���� ���� �̺�Ʈ ����
        slot.OnSlotDropped += HandleWeaponDrop;  // �巡�� �� ��� �̺�Ʈ ����
    }

    // ���� ���� ó��
    private void HandleWeaponSelection(Slot selectedSlot)
    {
        Weapon selectedWeapon = selectedSlot.GetWeapon();

        // ù ��° ���� ����
        if (firstWeapon == null)
        {
            firstWeapon = selectedWeapon;
        }
        // �� ��° ���� ���� �� �ռ�
        else if (secondWeapon == null)
        {
            secondWeapon = selectedWeapon;
            UpgradeWeapons();
        }
    }

    // ���� �ռ� ó��
    private void UpgradeWeapons()
    {
        if (firstWeapon != null && secondWeapon != null)
        {
            if (firstWeapon.weaponID == secondWeapon.weaponID)  // ������ �������� Ȯ��
            {
                int newWeaponID = firstWeapon.weaponID + 1;
                int newWeaponPower = newWeaponID * 10;

                // ���ο� ���� ����
                Weapon upgradedWeapon = new Weapon(newWeaponID);
                upgradedWeapon.WeaponPower = newWeaponPower;

                // ���� ��Ͽ��� �� ���� �����ϰ� ���ο� ���� �߰�
                inventory.Add(upgradedWeapon);
                inventory.Remove(firstWeapon);
                inventory.Remove(secondWeapon);

                // UI ����
                RefreshInventoryUI();

                // ���õ� ���� �ʱ�ȭ
                firstWeapon = null;
                secondWeapon = null;
            }
        }
    }

    // UI ����
    void RefreshInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);  // ���� UI �׸� ����
        }

        PopulateInventory();  // UI ���� ��ħ
    }

    // �κ��丮 UI�� �ִ� ��� ���� �߰�
    void PopulateInventory()
    {
        foreach (Weapon weapon in inventory)
        {
            AddWeaponToInventoryUI(weapon);  // UI�� ���� ���� �߰�
        }
    }
    // �巡�� �� ��� ó��
    private void HandleWeaponDrop(Slot droppedSlot)
    {
        // ��ӵ� ���Կ��� ���⸦ �����ͼ� ó��
        Weapon droppedWeapon = droppedSlot.GetWeapon();
        // �߰����� �巡�� �� ��� ���� ó��(�ٸ���ɳ����Ÿ�)
        Debug.Log($"��ӵ� ����: {droppedWeapon.weaponName}");
    }
}